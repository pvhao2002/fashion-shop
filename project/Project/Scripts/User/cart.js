function updateQuantity(cartItemId, change) {
    $.ajax({
        url: '/Cart/UpdateQuantity',
        type: 'POST',
        data: { cartItemId: cartItemId, change: change },
        success: function (res) {
            if (res.success) {
                location.reload();
            } else {
                alert(res.message);
            }
        }
    });
}

function removeItem(cartItemId) {
    if (confirm("Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?")) {
        $.ajax({
            url: '/Cart/RemoveItem',
            type: 'POST',
            data: { cartItemId: cartItemId },
            success: function (res) {
                if (res.success) {
                    location.reload();
                } else {
                    alert(res.message);
                }
            }
        });
    }
}
