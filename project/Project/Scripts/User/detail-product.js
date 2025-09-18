function decreaseQty() {
    let input = document.getElementById("quantityInput");
    let value = parseInt(input.value);
    if (value > 1) input.value = value - 1;
}

function increaseQty() {
    let input = document.getElementById("quantityInput");
    let value = parseInt(input.value);
    input.value = value + 1;
}

function addToCart(productId) {
    let qty;
    try {
        qty = parseInt(document.getElementById("quantityInput").value) ?? 1;
    } catch (err) {
        qty = 1;
    }
    if (qty < 1) qty = 1;

    $.ajax({
        url: '/Cart/Add',
        type: 'POST',
        data: {productId: productId, quantity: qty},
        success: function (response) {
            if (response.success) {
                alert("Đã thêm sản phẩm vào giỏ hàng!");
            } else {
                alert(response.message);
                if (response.needAuth) {
                    document.cookie.split(";").forEach(function (c) {
                        document.cookie = c
                            .replace(/^ +/, "")
                            .replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/");
                    });
                    if ($("#signin-modal").length) {
                        $("#signin-modal").modal("show");
                    } else {
                        // Hoặc redirect sang trang login
                        window.location.href = "/User/Home/Index?act=login";
                    }
                }
            }
        },
        error: function () {
            alert("Có lỗi xảy ra, vui lòng thử lại.");
        }
    });
}