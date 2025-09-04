function openMessage(productId) {
    fetch(`/User/Message/GetRoom/${productId}`)
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                window.location.href = `/User/Profile/Message/${data.roomId}`;
            } else {
                showAlert('Vui lòng đăng nhập để nhắn tin', 'warning');
            }
        })
        .catch(error => console.error(error));
}