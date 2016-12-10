using System.ComponentModel.DataAnnotations;

namespace RestfulApi.App.Dtos.AccountDtos
{
    public class AccountRegisterDto
    {
            public string Email { get; set; }

            public string Password { get; set; }
        }
}