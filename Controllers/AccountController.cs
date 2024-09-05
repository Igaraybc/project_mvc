using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel() { ReturnUrl = returnUrl});
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if(!ModelState.IsValid){
            return View(loginVM);
        }

        //Verifica se o user já foi registrado na tabela do identity
        var user = await _userManager.FindByNameAsync(loginVM.UserName);

        if (user != null){
            //Se usuário existe tentamos fazer o login
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

            if (result.Succeeded){
                if(string.IsNullOrEmpty(loginVM.ReturnUrl)){
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginVM.ReturnUrl);
            }
        }
        ModelState.AddModelError("", "Falha ao realizar o login!");
        return View(loginVM);
    }

    public IActionResult Register(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken] //Usado para evitar falsificação de requisições entre sites (Ataque CSRF)
    //Cria um campo oculto no formulário para validar a sessão
    public async Task<IActionResult> Register(LoginViewModel registerVM){
        if(ModelState.IsValid){
            var user = new IdentityUser() {UserName = registerVM.UserName};
            //Cria o usuário na tabela
            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if(result.Succeeded){
                await _userManager.AddToRoleAsync(user, "Member");
                return RedirectToAction("Login", "Account");
            }
            else{
                this.ModelState.AddModelError("Registro", "Falha ao realizar registro!");
            }
        }
        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Logout(){
        HttpContext.Session.Clear();
        HttpContext.User = null;
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied(){
        return View();
    }
}

