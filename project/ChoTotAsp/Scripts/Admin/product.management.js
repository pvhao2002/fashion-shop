function changeStatus(productId, status) {
    const formData = new FormData();
    formData.append('productId', productId);
    formData.append('status', status);
    fetch('/Admin/ProductManagement/ChangeStatus', {
        method: 'POST',
        body: formData
    }).then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload();
            } else {
                alert(data.message);
            }
        })
        .catch(error => {
            location.reload();
        });
}
