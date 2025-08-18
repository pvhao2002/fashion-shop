$(document).ready(function () {
    $.getJSON("./json/products.json", function (data) {
        let html = "";
        data.forEach(item => {
            html += `
          <div class="product-item-index">
            <div class="position-relative">
              <div class="position-relative mb-3">
                <button
                  class="btn btn-icon btn-light-primary btn-xs text-primary rounded-circle position-absolute top-0 end-0 m-3 zindex-5"
                  type="button" data-bs-toggle="tooltip" data-bs-placement="left" title="Thêm vào yêu thích">
                  <i class="fi-heart"></i>
                </button>
                <img class="rounded-3" src="${item.image}" alt="${item.name}" />
              </div>
              <h3 class="mb-2 fs-lg">
                <a class="nav-link stretched-link" href="single.html?id=${item.id}">
                  ${item.name}
                </a>
              </h3>
              <ul class="list-inline mb-0 fs-xs">
                <li class="list-inline-item pe-1">
                  <i class="fi-star-filled mt-n1 me-1 fs-base text-warning align-middle"></i>
                  <b>${item.rating}</b>
                  <span class="text-muted">&nbsp;(${item.reviews} đánh giá)</span>
                </li>
                <li class="list-inline-item pe-1">
                  <i class="fi-credit-card mt-n1 me-1 fs-base text-muted align-middle"></i>
                  ${item.price}
                </li>
                <li class="list-inline-item pe-1">
                  <i class="fi-shopping-bag mt-n1 me-1 fs-base text-muted align-middle"></i>
                  Đã bán: ${item.sold}
                </li>
              </ul>
            </div>
          </div>
        `;
        });
        $("#popular-products").html(html);
        // Lấy options từ attribute
        const options = JSON.parse($("#popular-products").attr("data-carousel-options"));

        // Khởi tạo Tiny Slider
        const slider = tns(Object.assign({}, {
            container: "#popular-products",
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
});