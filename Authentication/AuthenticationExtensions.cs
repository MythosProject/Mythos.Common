using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Mythos.Common.Authentication;

public static class AuthenticationExtensions
{
    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        var authenticationBuilder = builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = "cookie";
            options.DefaultChallengeScheme = "oidc";
            options.DefaultSignOutScheme = "oidc";
        })
        .AddCookie("cookie", options =>
        {
            // set session lifetime
            options.ExpireTimeSpan = TimeSpan.FromHours(8);

            // sliding or absolute
            options.SlidingExpiration = false;

            // host prefixed cookie name
            options.Cookie.Name = "__Mythos-spa";

            // strict SameSite handling
            options.Cookie.SameSite = SameSiteMode.Strict;
        })
        .AddOpenIdConnect("oidc", options =>
        {
            options.Authority = "https://localhost:7286";

            options.ClientId = "web";
            options.ClientSecret = "secret";
            options.ResponseType = "code";

            options.ResponseMode = "query";

            options.MapInboundClaims = false;
            options.GetClaimsFromUserInfoEndpoint = true;

            options.SaveTokens = true;

            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("offline_access");
            options.Scope.Add("api1");

            options.GetClaimsFromUserInfoEndpoint = true;
        });

        return builder;
    }
}
