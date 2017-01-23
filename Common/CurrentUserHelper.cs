using Microsoft.AspNet.Identity;
using System.Web;

namespace Common
{
    public class CurrentUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }

    public class CurrentUserHelper
    {
        public static CurrentUser Get {
            get {
                var user = HttpContext.Current.User;

                if (user == null)
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(user.Identity.GetUserId()))
                {
                    return null;
                }

                return new CurrentUser {
                    UserId = user.Identity.GetUserId(),
                    UserName = user.Identity.GetUserName(),
                    Name = user.Identity.Name
                };
            }
        }
    }
}
