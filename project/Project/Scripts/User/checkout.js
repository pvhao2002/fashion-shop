function applyCoupon(originalTotal) {
    let code = document.getElementById("promoCode").value.trim();
    let discountElem = document.getElementById("discountAmount");
    let finalTotalElem = document.getElementById("finalTotal");
    let successMsg = document.getElementById("couponMessage");
    let errorMsg = document.getElementById("couponError");

    // reset message
    successMsg.classList.add("d-none");
    errorMsg.classList.add("d-none");

    // Demo: chỉ chấp nhận mã "SALE10" giảm 10%
    if (code === "SALE10") {
        let discount = originalTotal * 0.1;
        discountElem.innerText = discount.toLocaleString() + " đ";
        finalTotalElem.innerText = (originalTotal - discount).toLocaleString() + " đ";
        successMsg.classList.remove("d-none");
    } else {
        discountElem.innerText = "0 đ";
        finalTotalElem.innerText = originalTotal.toLocaleString() + " đ";
        errorMsg.classList.remove("d-none");
    }
}

$("#checkoutForm").submit(function (e) {
    e.preventDefault();
    let phoneNumber = $("input[name=phoneNumber]").val().trim();
    let shippingAddress = $("textarea[name=shippingAddress]").val().trim();
    let paymentMethod = $("select[name=paymentMethod]").val();
    let note = $("textarea[name=note]").val().trim();
    let couponCode = $("#promoCode").val().trim();
    if (phoneNumber === "") {
        alert("Vui lòng nhập số điện thoại.");
        return;
    }
    let phoneRegex = /^(0[0-9]{9,10})$/;
    if (!phoneRegex.test(phoneNumber)) {
        alert("Số điện thoại không hợp lệ.");
        return;
    }
    if (shippingAddress === "") {
        alert("Vui lòng nhập địa chỉ giao hàng.");
        return;
    }

    if (!paymentMethod) {
        alert("Vui lòng chọn phương thức thanh toán.");
        return;
    }
    let data = {
        phoneNumber: phoneNumber,
        shippingAddress: shippingAddress,
        paymentMethod: paymentMethod,
        note: note,
        couponCode: couponCode
    };

    $.ajax({
        url: '/Cart/PlaceOrder',
        type: 'POST',
        data: data,
        success: function (res) {
            if (res.success) {
                alert(res.message);
                if (res.paymentUrl) {
                    window.location.href = res.paymentUrl;
                } else {
                    window.location.href = "/User/Product/";
                }
            } else {
                alert(res.message);
                if (res.needAuth) {
                    window.location.href = "/User/Home/Index?act=login";
                }
            }
        },
        error: function () {
            alert("Có lỗi xảy ra khi đặt hàng!");
        }
    });
});
