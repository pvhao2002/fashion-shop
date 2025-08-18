setTimeout(function () {
  // Your code to execute after 1 second
  $.getJSON("../json/products.json", function (data) {
    let html = "";
    data.forEach(item => {
      html += `
          <div class="col pb-sm-2">
              <article class="position-relative">
                <div class="position-relative mb-3">
                  <img class="rounded-3" src="${item.image}" alt="Article img"
                    style="width: 430px; height: 195px; object-fit: contain;">
                </div>
                <h3 class="mb-2 fs-lg"><a class="nav-link stretched-link" href="single.html">
                    ${item.name}
                  </a></h3>
                <ul class="list-inline mb-0 fs-xs">
                  <li class="list-inline-item pe-1">
                    <i class="fi-star-filled mt-n1 me-1 fs-base text-warning align-middle"></i><b>5.0</b><span
                      class="text-muted">&nbsp;(48)</span>
                  </li>
                  <li class="list-inline-item pe-1">
                    <i class="fi-credit-card mt-n1 me-1 fs-base text-muted align-middle"></i>$$
                  </li>
                  <li class="list-inline-item pe-1">
                    <i class="fi-map-pin mt-n1 me-1 fs-base text-muted align-middle"></i>Freeship
                  </li>
                </ul>
              </article>
            </div>
        `;
    });
    $("#catalog-products").html(html);
  });
}, 1500);