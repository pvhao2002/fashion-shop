function addToWishList(productId) {
    showLoading();
    fetch(`/User/WishList/Add/${productId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            hideLoading();
            showAlert(data.message, data.type);
        })
        .catch(error => {
            hideLoading();
            showAlert('Vui lòng đăng nhập', 'warning');
        });
}

function removeFromWishList(productId) {
    showLoading();
    fetch(`/User/WishList/Remove/${productId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            hideLoading();
            showAlert(data.message, data.type);
            setTimeout(() => {
                location.reload();
            }, 500);
        })
        .catch(error => {
            hideLoading();
            showAlert('Vui lòng đăng nhập', 'warning');
        });
}