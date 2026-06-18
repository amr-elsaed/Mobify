namespace Mobify.BLL.Services.Implmentation
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountServices(UserManager<ApplicationUser> user, SignInManager<ApplicationUser> signIn , RoleManager<IdentityRole> roleManager)
        {
            userManager = user;
            signInManager = signIn;
            this.roleManager = roleManager;
        }
        public async Task<bool> Register(UserRegisterVM user)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Email = user.Email,
                UserName = user.Name,
                Address = user.Address,
            };
            var res = await userManager.CreateAsync(applicationUser, user.Password);
            if (res.Succeeded)
            {
                await signInManager.SignInAsync(applicationUser, false);
                return true;
            }
            return false;
        }
        public async Task<bool> SignOut()
        {
            await signInManager.SignOutAsync();
            return true;
        }
        public async Task<bool> SignIn(UserLogInVM user)
        {
            var res = await userManager.FindByEmailAsync(user.Email);
            if (res != null)
            {
                bool found = await userManager.CheckPasswordAsync(res, user.Password);
                if (found)
                {
                    await signInManager.SignInAsync(res, user.RememberMe);
                    return true;
                }
            }
            return false;
        }
    
    }
}
