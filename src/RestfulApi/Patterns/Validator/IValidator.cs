namespace RestfulApi.Models.Validator
{
    public interface IValidator<TEntity>
    {
         void Validate(TEntity entity);
    }
}