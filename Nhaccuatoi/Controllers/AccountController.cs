using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhaccuatoi.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Nhaccuatoi.Controllers;
// namespace Nhaccuatoi.Models.ApplicationUser;

public class AccountController : Controller
{
  private readonly ILogger<AccountController> _logger;
  private readonly UserManager<IdentityUser> _userManager;
  private readonly SignInManager<IdentityUser> _signInManager;
  // private readonly IOptions<AppSettings> _appSettings;

  public AccountController(ILogger<AccountController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
  // IOptions<AppSettings> appSettings
  {
    _logger = logger;
    _signInManager = signInManager;
    _userManager = userManager;
    // _appSettings = appSettings;
  }

  [HttpGet]
  public IActionResult SignIn()
  {
    return View("SignIn");
  }

  [HttpGet]
  public async Task<IActionResult> Logout()
  {
      await _signInManager.SignOutAsync();
      
      HttpContext.Session.Remove("UserId");
      HttpContext.Session.Remove("UserName");

      return RedirectToAction("Index", "Home");
  }

  [HttpPost]
  public async Task<IActionResult> SignIn(SignInModel signInModel)
  {
    _logger.LogInformation($"Username: {signInModel.Username}, Password length: {signInModel.Password}");
    if (ModelState.IsValid)
    {
      // Console.WriteLine(signInModel.Username, signInModel.Password);
      Console.WriteLine("Username: {0}, Password: {1}", signInModel.Username, signInModel.Password);
        var user = await _userManager.FindByNameAsync(signInModel.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, signInModel.Password))
        {

          var roles = await _userManager.GetRolesAsync(user);
          var role = roles.FirstOrDefault();

          await _signInManager.SignInAsync(user, isPersistent: false);

          HttpContext.Session.SetString("UserId", user.Id);
          HttpContext.Session.SetString("UserName", user.UserName);
          
          if (role == "Admin") {
            return RedirectToAction("Dashboard", "Admin");
          }

          return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    }
    return View(signInModel);
  }

  [HttpGet]
  public IActionResult SignUp()
  {
    return View("SignUp");
  }

  [HttpPost]
  public async Task<IActionResult> SignUp(SignUpModel signUpModel)
  {
    if (ModelState.IsValid)
    {
      Console.WriteLine("Username: {0}, Password: {1}", signUpModel.Username, signUpModel.Password);
      var user = new IdentityUser { UserName = signUpModel.Username };
      // Console.WriteLine(JsonSerializer.Serialize(user), user.Id);
      var result = await _userManager.CreateAsync(user, signUpModel.Password);
      string json = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);

      if (result.Succeeded)
      {
        if (!string.IsNullOrEmpty("User"))
        {
            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to add role.");
                return View("User");
            }
        }

        HttpContext.Session.SetString("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.UserName);
        return RedirectToAction("Index", "Home");
      }

      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
    }
    return View(signUpModel);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
