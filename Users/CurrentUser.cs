//using System.Security.Claims;

//namespace Mythos.Common.Users;

//public class CurrentUser
//{
//    public MythosUser User { get; set; } = default!;
//    public ClaimsPrincipal Principal { get; set; } = default!;

//    public string UserId => User.Id;
//    public string Id => Principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
//    public bool IsAdmin => Principal.IsInRole("admin");
//}