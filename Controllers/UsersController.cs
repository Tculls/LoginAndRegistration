using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegistration.Models;

public class UsersController : Controller
{
    private ORMContext _context;
    public UsersController(ORMContext context)
    {
        _context = context;
    }

    [HttpGet("/")]
    public IActionResult LoginAndReg()
    {
        return View("LoginRegistration");
    }

    [HttpPost("/register")]
    public IActionResult Register(User NewUser)
    {
        if (ModelState.IsValid)
        {
            if(_context.Users.Any(use => use.Email == NewUser.Email))
            {
                ModelState.AddModelError("Email", "is in use");
            }
        }
        if (ModelState.IsValid == false)
        {
            return LoginAndReg();
        }
        PasswordHasher<User> HashedPass = new PasswordHasher<User>();
        NewUser.Password = HashedPass.HashPassword(NewUser, NewUser.Password);
        _context.Users.Add(NewUser);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("UUID", NewUser.UserId);
        return RedirectToAction("Success");
        

    }
    [HttpPost("/login")]
    public IActionResult Login(LoginUser loginUser)
    {
        if(ModelState.IsValid == false)
        {
            return LoginAndReg();
        }

        User? dbUser = _context.Users.FirstOrDefault(use => use.Email == loginUser.Email);
        if(dbUser == null)
        {
            ModelState.AddModelError("Email", " and password do not match");
            return LoginAndReg();
        }

        PasswordHasher<LoginUser> HashedPass = new PasswordHasher<LoginUser>();
        PasswordVerificationResult PassCompare = HashedPass.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.Password);

        if (PassCompare == 0)
        {
            ModelState.AddModelError("Password", "does not match this email");
            return LoginAndReg();
        }
        HttpContext.Session.SetInt32("UUID", dbUser.UserId);
        return RedirectToAction("Success");
    }
    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("LoginAndReg");
    }
    [HttpGet("/Users/Success")]
    public IActionResult Success()
    {
        return View("Success");
    }
}