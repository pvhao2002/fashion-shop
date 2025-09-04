function hidePost(productId) {
    const formData = new FormData();
    formData.append("productId", productId);
    showLoading();
    fetch('/User/Profile/HidePost', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            hideLoading();
            if (data.success) {
                showAlert('Ẩn bài đăng thành công.', 'success');
                setTimeout(() => {
                    location.reload();
                }, 1000);
            } else {
                showAlert(data.message, 'danger');
            }
        })
        .catch(_ => {
            showAlert('Có lỗi xảy ra, vui lòng thử lại sau.', 'danger');
        });
}

function showPost(productId) {
    showLoading();
    const formData = new FormData();
    formData.append("productId", productId);
    fetch('/User/Profile/ShowPost', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            hideLoading();
            if (data.success) {
                showAlert('Đăng lại bài đăng thành công.', 'success');
                setTimeout(() => {
                    location.reload();
                }, 1000);
            } else {
                showAlert('Có lỗi xảy ra, vui lòng thử lại sau.', 'danger');
            }
        })
        .catch(_ => {
            showAlert('Có lỗi xảy ra, vui lòng thử lại sau.', 'danger');
        });
}