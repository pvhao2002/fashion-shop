setTimeout(function () {
    // Your code to execute after 1 second
    $.getJSON("../json/products.json", function (data) {
        let html = "";
        data.forEach(item => {
            html += `
          <div>
            <div class="position-relative">
              <div class="position-relative mb-3">
                <button
                  class="btn btn-icon btn-light-primary btn-xs text-primary rounded-circle position-absolute top-0 end-0 m-3 zindex-5"
                  type="button" data-bs-toggle="tooltip" data-bs-placement="left" title="Add to Wishlist">
                  <i class="fi-heart"></i>
                </button>
                <img class="rounded-3" src="img/fashion/anh1.png" alt="Article img"
                  style="width: 423px; height: 193px; object-fit: cover;">
              </div>
              <h3 class="mb-2 fs-lg"><a class="nav-link stretched-link" href="#">Tên sản phẩm</a></h3>
              <ul class="list-inline mb-0 fs-xs">
                <li class="list-inline-item pe-1">
                  <i class="fi-star-filled mt-n1 me-1 fs-base text-warning align-middle"></i>
                  <b>5.0</b>
                  <span class="text-muted">&nbsp;(48)</span>
                </li>
                <li class="list-inline-item pe-1">
                  <i class="fi-credit-card mt-n1 me-1 fs-base text-muted align-middle"></i>300.000 đ
                </li>
                <li class="list-inline-item pe-1">
                  <i class="fi-shopping-bag mt-n1 me-1 fs-base text-muted align-middle"></i>3k4 đã bán 
                </li>
              </ul>
            </div>
          </div>
        `;
        });
        $("#related-products").html(html);
        // Lấy options từ attribute
        const options = JSON.parse($("#related-products").attr("data-carousel-options"));

        // Khởi tạo Tiny Slider
        const slider = tns(Object.assign({}, {
            container: "#related-products",
            controlsText: [
                '<i class="fi-chevron-left"></i>',
                '<i class="fi-chevron-right"></i>',
            ],
            nav: true,
            navPosition: "bottom",
            mouseDrag: !0,
            speed: 500,
            autoplayHoverPause: !0,
            autoplayButtonOutput: !1
        }, options));
    });
}, 1500);