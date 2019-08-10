using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class UrlHelperExtensions1
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                //action: nameof(AccountController.ConfirmEmail),
                action: "ConfirmEmail",
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                //action: nameof(AccountController.ResetPassword),
                action: "ResetPassword",
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }
    }
}
