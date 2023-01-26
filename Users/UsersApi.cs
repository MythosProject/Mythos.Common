//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Routing;
//using Mythos.Common.Authentication;

//namespace Mythos.Common.Users;

//public static class UsersApi
//{
//    public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes)
//    {
//        var group = routes.MapGroup("/users");

//        group.WithTags("Users");

//        group.MapPost("/", async Task<Results<Ok, ValidationProblem>> (UserInfo newUser, UserManager<MythosUser> userManager) =>
//        {
//            var result = await userManager.CreateAsync(new() { UserName = newUser.Username }, newUser.Password);

//            if (result.Succeeded)
//            {
//                return TypedResults.Ok();
//            }

//            return TypedResults.ValidationProblem(result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description }));
//        });

//        group.MapPost("/token", async Task<Results<BadRequest, Ok<AuthToken>>> (UserInfo userInfo, UserManager<MythosUser> userManager, ITokenService tokenService) =>
//        {
//            var user = await userManager.FindByNameAsync(userInfo.Username);

//            if (user is null || !await userManager.CheckPasswordAsync(user, userInfo.Password))
//            {
//                return TypedResults.BadRequest();
//            }

//            return TypedResults.Ok(new AuthToken(tokenService.GenerateToken(user.UserName!)));
//        });

//        group.MapPost("/token/{provider}", async Task<Results<Ok<AuthToken>, ValidationProblem>> (string provider, ExternalUserInfo userInfo, UserManager<MythosUser> userManager, ITokenService tokenService) =>
//        {
//            var user = await userManager.FindByLoginAsync(provider, userInfo.ProviderKey);

//            var result = IdentityResult.Success;

//            if (user is null)
//            {
//                user = new MythosUser() { UserName = userInfo.Username };

//                result = await userManager.CreateAsync(user);

//                if (result.Succeeded)
//                {
//                    result = await userManager.AddLoginAsync(user, new UserLoginInfo(provider, userInfo.ProviderKey, displayName: null));
//                }
//            }

//            if (result.Succeeded)
//            {
//                return TypedResults.Ok(new AuthToken(tokenService.GenerateToken(user.UserName!)));
//            }

//            return TypedResults.ValidationProblem(result.Errors.ToDictionary(e => e.Code, e => new[] { e.Description }));
//        });

//        return group;
//    }
//}
