using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using MyLib;

namespace MyLib.Mvc
{
    public class SignInHelper : Controller
    {
        private readonly IOptionsMonitor<CookieAuthenticationOptions> _cookieAuthOptionsMonitor;
        public SignInHelper(IOptionsMonitor<CookieAuthenticationOptions> cookieAuthOptions)
        {
            _cookieAuthOptionsMonitor = cookieAuthOptions;
        }
        public async void SignInByCookie(SignInItem sign)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, sign.Principal, sign.Properties);
        }
        public void SignInByCookie(dynamic userId)
        {
            SignInItem sign = new(userId);
            SignInByCookie(sign);
        }
    }
    public class SignInItem
    {
        public ClaimsIdentity? Identity;
        public ClaimsPrincipal? Principal { get; set; }
        public AuthenticationProperties? Properties { get; set; }
        public SignInItem()
        {
        }
        public SignInItem(dynamic id)
        {
            SetProperties(true, true, 20);
        }
        public void SetUserId(dynamic uid)
        {
            Identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            var name = new Claim(ClaimTypes.Name, uid);
            var nameId = new Claim(ClaimTypes.NameIdentifier, uid);
            Identity.AddClaim(name);
            Identity.AddClaim(nameId);
            Principal = new ClaimsPrincipal(Identity);
        }
        public void SetProperties(bool isPersistent, bool allowRefresh, double? expireTime)
        {
            Properties = new AuthenticationProperties();
            Properties.IsPersistent = isPersistent;
            Properties.AllowRefresh = allowRefresh;
            double exTime = expireTime ?? 20;
            Properties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(exTime);
        }
    }
}
