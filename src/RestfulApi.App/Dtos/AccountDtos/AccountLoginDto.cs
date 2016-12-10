using System.ComponentModel.DataAnnotations;

namespace RestfulApi.App.Dtos.AccountDtos
{
    public class AccountLoginDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}