using Microsoft.AspNetCore.Identity;
using Mobify.BLL.ModelVM.AccountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobify.BLL.Services.Abstraction
{
    public interface IAccountServices
    {
        public Task<bool> Register(UserRegisterVM user);
        public Task<bool> SignOut();
        public Task<bool> SignIn(UserLogInVM user);
    }
}
