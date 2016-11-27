using RestfulApi.Models.Validator;

namespace RestfulApi.Patterns.Builder
{
    public interface IBuilder<TEntity,TValidator> 
    {
        TEntity Build(IValidator<TEntity> validator);
        TEntity Build();      
    }
}