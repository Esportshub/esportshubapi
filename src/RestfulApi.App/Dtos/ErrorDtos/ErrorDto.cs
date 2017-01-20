namespace RestfulApi.App.Dtos.ErrorDtos
{
    public abstract class ErrorDto
    {
        protected string ErrorMessage { get; set; }

        public ErrorDto(string message)
        {
            ErrorMessage = message;
        }
    }
}