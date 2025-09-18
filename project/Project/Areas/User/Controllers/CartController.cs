using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Project.Areas.User.DTO;
using Project.Areas.User.ViewModel;
using Project.Entity;
using Project.Utils;

namespace Project.Areas.User.Controllers
{
    [CustomAuthorize(Constant.ROLE_USER)]
    public class CartController : System.Web.Mvc.Controller
    {
        [HttpPost]
        public async Task<JsonResult> PlaceOrder(
            string phoneNumber
            , string shippingAddress
            , string paymentMethod
            , string note
            , string couponCode = null)
        {
            using (var ctx = new DBConnection())
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    var user = AuthenticationUtil.GetCurrentUser(Request, Session);
                    var cart = await ctx.carts.Include("cart_items.product")
                        .FirstOrDefaultAsync(c => c.user_id == user.user_id);

                    if (cart == null || cart.cart_items == null || !cart.cart_items.Any())
                    {
                        return Json(new { success = false, message = "Giỏ hàng của bạn đang trống." });
                    }

                    decimal totalAmount = cart.cart_items.Sum(ci => ci.total_item_price ?? 0);
                    decimal discount = 0;
                    if (!string.IsNullOrEmpty(couponCode))
                    {
                        if (couponCode == "SALE10")
                        {
                            discount = totalAmount * 0.1m;
                        }
                        else
                        {
                            return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ." });
                        }
                    }

                    totalAmount -= discount;

                    // Tạo order
                    var order = new orders
                    {
                        user_id = user.user_id,
                        order_status = Constant.PENDING,
                        payment_status = Constant.UNPAID,
                        total_amount = totalAmount,
                        phone_number = phoneNumber,
                        shipping_address = shippingAddress,
                        note = note,
                        payment_method = paymentMethod,
                        discount_code = couponCode,
                        discount_amount = discount.ToString("F2"),
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };

                    order = ctx.orders.Add(order);
                    await ctx.SaveChangesAsync();

                    // Thêm order_items
                    foreach (var ci in cart.cart_items)
                    {
                        var orderItem = new order_items
                        {
                            order_id = order.order_id,
                            product_id = ci.product_id,
                            quantity = ci.quantity ?? 0,
                            price = ci.product?.price ?? 0
                        };
                        ctx.order_items.Add(orderItem);
                    }

                    ctx.cart_items.RemoveRange(cart.cart_items);
                    ctx.carts.Remove(cart);
                    await ctx.SaveChangesAsync();
                    transaction.Commit();
                    string paymentUrl = null;
                    if (Constant.VNPAY.Equals(paymentMethod))
                    {
                        paymentUrl = doPaymentVNPAY(order);
                    }

                    return Json(new
                    {
                        success = true, message = "Đặt hàng thành công!", orderId = order.order_id,
                        paymentUrl = paymentUrl
                    });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Có lỗi xảy ra khi đặt hàng: " + ex.Message });
                }
            }
        }

        private string doPaymentVNPAY(orders o)
        {
            string vnp_Returnurl = "http://localhost:61833/User/Product";
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            var gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var gmtPlus7CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(o.created_at.Value.ToUniversalTime(), gmtPlus7);


            string vnp_TmnCode = "GLE8YXG4";
            string vnp_HashSecret = "ZCVPMHAELZKRPGTFLWJDPLQVPHBWEKXG";
            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (Convert.ToInt64(o.total_amount) * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", gmtPlus7CreatedAt.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang: {o.order_id}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", $"{DateTime.Now.Ticks}{o.order_id}");
            var gmtPlus7ExpireDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(15), gmtPlus7);
            vnpay.AddRequestData("vnp_ExpireDate", gmtPlus7ExpireDate.ToString("yyyyMMddHHmmss"));
            return vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
        }

        public async Task<ActionResult> CheckOut()
        {
            using (var ctx = new DBConnection())
            {
                var user = AuthenticationUtil.GetCurrentUser(Request, Session);
                var cart = await ctx.carts.FirstOrDefaultAsync(it => it.user_id == user.user_id);
                var cartDto = new CartDTO(cart);
                return View(new CartViewModel
                {
                    Cart = cartDto,
                    User = new UserDTO(user)
                });
            }
        }

        // GET
        public async Task<ActionResult> Index()
        {
            using (var ctx = new DBConnection())
            {
                var userId = AuthenticationUtil.GetUserId(Request, Session);
                var cart = await ctx.carts.FirstOrDefaultAsync(it => it.user_id == userId);
                var cartDto = new CartDTO(cart);
                return View(new CartViewModel
                {
                    Cart = cartDto
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> RemoveItem(int cartItemId)
        {
            using (var ctx = new DBConnection())
            {
                var cartItem = await ctx.cart_items.Include(it => it.product)
                    .FirstOrDefaultAsync(it => it.cart_item_id == cartItemId);
                if (cartItem == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại trong giỏ hàng" });
                }

                var cart = await ctx.carts.FirstOrDefaultAsync(it => it.cart_id == cartItem.cart_id);
                if (cart == null)
                {
                    return Json(new { success = false, message = "Giỏ hàng không tồn tại" });
                }

                cart.total_items -= Convert.ToInt32(cartItem.quantity);
                cart.total_price -= Convert.ToDecimal(cartItem.total_item_price ?? 0);
                ctx.cart_items.Remove(cartItem);
                await ctx.SaveChangesAsync();
                return Json(new
                {
                    success = true,
                    totalPrice = cart.total_price,
                    totalItems = cart.total_items
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateQuantity(int cartItemId, int change)
        {
            using (var ctx = new DBConnection())
            {
                var cartItem = await ctx.cart_items.Include(it => it.product)
                    .FirstOrDefaultAsync(it => it.cart_item_id == cartItemId);
                if (cartItem == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại trong giỏ hàng" });
                }

                var cart = await ctx.carts.FirstOrDefaultAsync(it => it.cart_id == cartItem.cart_id);
                if (cart == null)
                {
                    return Json(new { success = false, message = "Giỏ hàng không tồn tại" });
                }

                if (cartItem.quantity + change <= 0)
                {
                    // Remove item from cart
                    cart.total_items -= Convert.ToInt32(cartItem.quantity);
                    cart.total_price -= Convert.ToDecimal(cartItem.total_item_price ?? 0);
                    ctx.cart_items.Remove(cartItem);
                    await ctx.SaveChangesAsync();
                    return Json(new
                    {
                        success = true,
                        totalItemPrice = 0,
                        totalPrice = cart.total_price,
                        totalItems = cart.total_items
                    });
                }

                cartItem.quantity += change;
                cartItem.total_item_price = cartItem.product.price * cartItem.quantity;
                cart.total_items += change;
                cart.total_price += Convert.ToDecimal(cartItem.product.price * change);
                await ctx.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    totalItemPrice = cartItem.total_item_price,
                    totalPrice = cart.total_price,
                    totalItems = cart.total_items
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Add(int productId, int quantity)
        {
            using (var ctx = new DBConnection())
            {
                if (quantity <= 0)
                {
                    return Json(new { success = false, message = "Số lượng không hợp lệ" });
                }

                var product = await ctx.products.FirstOrDefaultAsync(it => it.product_id == productId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại" });
                }

                var userId = AuthenticationUtil.GetUserId(Request, Session);
                var cart = await ctx.carts.FirstOrDefaultAsync(it => it.user_id == userId);
                if (cart == null)
                {
                    cart = new cart
                    {
                        user_id = userId,
                        total_price = 0,
                        total_items = 0,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    cart = ctx.carts.Add(cart);
                    await ctx.SaveChangesAsync();

                    var cartItem = new cart_items
                    {
                        cart_id = cart.cart_id,
                        product_id = productId,
                        quantity = quantity,
                        total_item_price = product.price * quantity
                    };
                    ctx.cart_items.Add(cartItem);
                    cart.total_items += quantity;
                    cart.total_price += cartItem.total_item_price ?? 0;
                    await ctx.SaveChangesAsync();
                }
                else
                {
                    var cartItem = await ctx.cart_items.FirstOrDefaultAsync(it =>
                        it.cart_id == cart.cart_id && it.product_id == productId);
                    if (cartItem == null)
                    {
                        cartItem = new cart_items
                        {
                            cart_id = cart.cart_id,
                            product_id = productId,
                            quantity = quantity,
                            total_item_price = product.price * quantity,
                        };
                        ctx.cart_items.Add(cartItem);
                        cart.total_items += quantity;
                        cart.total_price += cartItem.total_item_price ?? 0;
                        await ctx.SaveChangesAsync();
                    }
                    else
                    {
                        cartItem.quantity += quantity;
                        cartItem.total_item_price += product.price * quantity;
                        cart.total_items += quantity;
                        cart.total_price += Convert.ToDecimal(product.price * quantity);
                        await ctx.SaveChangesAsync();
                    }
                }
            }

            return Json(new { success = true });
        }
    }
}