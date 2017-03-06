using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using System.Security.Claims;
using System.Configuration;
using System.DirectoryServices;

using System.Net.Mime;
using System.DirectoryServices.AccountManagement;

namespace GalopApi.Providers
{
    public class ADAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "" });
            string connection = ConfigurationManager.ConnectionStrings["ADConnectionString"].ToString();
            DirectorySearcher dssearch = new DirectorySearcher(connection);

            String username = context.Request.Headers.GetValues("username").ElementAt(0);
            String password = context.Request.Headers.GetValues("password").ElementAt(0);


            dssearch.Filter = "(sAMAccountName=" + username + ")";
            SearchResult sresult = dssearch.FindOne();
            if (sresult != null)
            {
                DirectoryEntry dsresult = sresult.GetDirectoryEntry();
                UserPrincipal userPrincipal = null;
                bool loginSucceed = false;
                using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain))
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalContext, IdentityType.SamAccountName, username);

                    loginSucceed = principalContext.ValidateCredentials(username, password);

                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                context.Validated(identity);
            }
        }
    }
}