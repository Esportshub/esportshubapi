using RestfulApi.Models.Validator;
using System.ComponentModel.DataAnnotations;

namespace Patterns.Builder 
{
    public interface IBuilder<TEntity,TValidator> 
    {
        TEntity Build(IValidator<TEntity> validator);
        TEntity Build();      
    }
}