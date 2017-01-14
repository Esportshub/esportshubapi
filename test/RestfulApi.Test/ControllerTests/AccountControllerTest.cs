using System;
using Moq;
using Xunit;

namespace RestfullApi.Test.ControllerTests
{
    public class AccountControllerTest : IDisposable
    {

        //mock _signInManager.SignInAsync
        //mock _userManager.CreateAsync
        [Fact]
        public async void Register()
        {
            Mock<UserManager<ApplicationUser>> userManager = new Mock<UserManager<ApplicationUser>>();
            Mock<SignInManager<ApplicationUser>> signInManager = new Mock<SignInManager<ApplicationUser>>();

            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            signInManager.Setup(x => x.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), It.IsAny<string>())).Returns();

            AccountController accountController = new AccountController(userManager.Object, signInManager.Object, null, null );
        }

        public void Dispose()
        {

        }
    }
}