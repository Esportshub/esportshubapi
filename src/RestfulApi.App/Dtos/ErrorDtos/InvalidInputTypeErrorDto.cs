using System.Collections.Generic;

namespace RestfulApi.App.Dtos.ErrorDtos
{
    public sealed class InvalidInputTypeErrorDto : ErrorDto
    {

        public InvalidInputTypeErrorDto() : base("Invalid input type(s)") { }
    }
}