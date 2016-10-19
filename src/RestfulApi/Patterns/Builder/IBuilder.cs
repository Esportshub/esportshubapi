using RestfulApi.Models.Validator;

namespace Patterns.Builder 
{
    public interface IBuilder<TEntity> 
    {
        TEntity Build(IValidator validator);
        TEntity Build();
        
    }
}