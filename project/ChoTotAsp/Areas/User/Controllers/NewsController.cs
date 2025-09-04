//using System;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using System.Web.WebPages;
//using ChoTotAsp.Areas.User.Payload;
//using ChoTotAsp.Areas.User.ViewModel;
//using ChoTotAsp.Entity;
//using ChoTotAsp.Utils;

//namespace ChoTotAsp.Areas.User.Controllers
//{
//    public class NewsController : System.Web.Mvc.Controller
//    {
//        // GET
//        public async Task<ActionResult> Index(NewsRequest request)
//        {
//            var listProvinces = await DBContext.Instance.Provinces.ToListAsync();
//            var listCategory = await DBContext.Instance.Categories.ToListAsync();
//            var currentUserId = AuthenticationUtil.GetUserId(Request, Session);
//            var listNews = DBContext.Instance.Products
//                .Include(p => p.Account)
//                .Include(p => p.Category)
//                .Include(p => p.Province)
//                .Include(p => p.Images)
//                .Where(p => p.AccountId != currentUserId && Constant.APPROVED.Equals(p.Status));
//            listNews = ApplyFilters(listNews, request);
//            var totalNews = await listNews.CountAsync();
//            var totalPage = (int)Math.Ceiling((double)totalNews / request.Pz);
//            listNews = ApplySorting(listNews, request.Sb, request.St);

//            listNews = listNews
//                .Skip((request.P - 1) * request.Pz)
//                .Take(request.Pz);

//            var model = new NewViewModel
//            {
//                ListProvinces = listProvinces,
//                ListCategory = listCategory,
//                ListNews = await listNews.ToListAsync(),
//                TotalNews = totalNews,
//                ViewMode = request.View,
//                TotalPage = totalPage,
//                Page = request.P,
//                CategoryId = request.Cid,
//                ProvinceId = request.Pid,
//                YearFrom = request.Yf,
//                YearTo = request.Yt,
//                PriceFrom = request.Pf,
//                PriceTo = request.Pt,
//                SortBy = request.Sb,
//                PageSize = request.Pz,
//                NextPageLink = GetNextPageLink(request, totalPage),
//                PrevPageLink = GetPrevPageLink(request),
//                Sort = $"{request.St}-{request.Sb}",
//                SortType = request.St,
//            };
//            return View(model);
//        }

//        public async Task<ActionResult> Detail(int id)
//        {
//            var userId = AuthenticationUtil.GetUserId(Request, Session);
//            var post = await DBContext.Instance.Products
//                .Include(p => p.Account)
//                .Include(p => p.Category)
//                .Include(p => p.Province)
//                .Include(p => p.Images)
//                .FirstOrDefaultAsync(p => p.ProductId == id);
//            if (post == null) return RedirectToAction("Index");
//            var relatedPost = await DBContext.Instance.Products
//                .Include(p => p.Account)
//                .Include(p => p.Category)
//                .Include(p => p.Province)
//                .Include(p => p.Images)
//                .Where(p => p.CategoryId == post.CategoryId && p.ProductId != post.ProductId)
//                .OrderByDescending(p => p.CreatedDate)
//                .Take(5)
//                .ToListAsync();
//            var model = new DetailNewsViewModel
//            {
//                CurrentProduct = post,
//                RelatedProduct = relatedPost
//            };
//            return View(model);
//        }


//        private string GetNextPageLink(NewsRequest request, int totalPage)
//        {
//            return request.P >= totalPage
//                ? "javascript:void(0)"
//                : Url.Action("Index", new
//                {
//                    request.Cid,
//                    request.Pid,
//                    request.Yf,
//                    request.Yt,
//                    request.Pf,
//                    request.Pt,
//                    P = request.P + 1,
//                    request.Pz,
//                    request.Sb,
//                    request.St
//                });
//        }

//        private string GetPrevPageLink(NewsRequest request)
//        {
//            return request.P <= 1
//                ? "javascript:void(0)"
//                : Url.Action("Index", new
//                {
//                    request.Cid,
//                    request.Pid,
//                    request.Yf,
//                    request.Yt,
//                    request.Pf,
//                    request.Pt,
//                    P = request.P - 1,
//                    request.Pz,
//                    request.Sb,
//                    request.St
//                });
//        }

//        private IQueryable<Product> ApplySorting(IQueryable<Product> query, string sortBy, string sortType)
//        {
//            switch (sortBy.ToLower())
//            {
//                case "created_date":
//                    query = sortType.ToLower() == "asc"
//                        ? query.OrderBy(p => p.CreatedDate)
//                        : query.OrderByDescending(p => p.CreatedDate);
//                    break;
//                case "price":
//                    query = sortType.ToLower() == "asc"
//                        ? query.OrderBy(p => p.Price)
//                        : query.OrderByDescending(p => p.Price);
//                    break;
//                default:
//                    query = query.OrderByDescending(p => p.CreatedDate); // Default sorting
//                    break;
//            }

//            return query;
//        }

//        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, NewsRequest request)
//        {
//            query = query
//                .Where(p => request.Cid <= 0 || p.CategoryId == request.Cid)
//                .Where(p => request.Pid <= 0 || p.ProvinceId == request.Pid)
//                .Where(p => request.Yf <= 0 || p.CreatedDate.Year >= request.Yf)
//                .Where(p => request.Yt <= 0 || p.CreatedDate.Year <= request.Yt)
//                .Where(p => request.Pf <= 0 || p.Price >= request.Pf)
//                .Where(p => request.Keyword == null
//                            || request.Keyword.Trim().Equals("")
//                            || p.Title.ToLower().Contains(request.Keyword.ToLower()))
//                .Where(p => request.Pt <= 0 || p.Price <= request.Pt);

//            return query;
//        }
//    }
//}