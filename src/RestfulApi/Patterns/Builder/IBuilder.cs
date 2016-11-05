using RestfulApi.Models.Validator;
using System.ComponentModel.DataAnnotations;

namespace Patterns.Builder 
{
    public interface IBuilder<TEntity,TValidator> 
    {
        TEntity Build(TValidator validator);
        TEntity Build();      
    }
}