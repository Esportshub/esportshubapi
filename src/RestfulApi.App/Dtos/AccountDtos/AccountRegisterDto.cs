using System.ComponentModel.DataAnnotations;

namespace RestfulApi.App.Dtos.AccountDtos
{
    public class AccountRegisterDto
    {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
}