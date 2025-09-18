using System;
using System.Linq;
using System.Web;
using Project.Entity;

namespace Project.Utils
{
    public static class AuthenticationUtil
    {
        public static int GetUserId(HttpRequestBase request, HttpSessionStateBase session)
        {
            var userIdStr = request.Cookies[Constant.USER_ID]?.Value;
            return string.IsNullOrEmpty(userIdStr) ? -1 : Convert.ToInt32(userIdStr);
        }

        public static int GetUserId(HttpRequest request, HttpSessionStateBase session)
        {
            return GetUserId(new HttpRequestWrapper(request), session);
        }

        public static user GetCurrentUser(HttpRequestBase request, HttpSessionStateBase session)
        {
            var userId = GetUserId(request, session);
            if (userId == -1) return null;

            using (var ctx = new DBConnection())
            {
                return ctx.users.FirstOrDefault(it => it.user_id == userId);
            }
        }
    }
}