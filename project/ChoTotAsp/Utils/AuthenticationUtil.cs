using System;
using System.Web;

namespace ChoTotAsp.Utils
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
    }
}