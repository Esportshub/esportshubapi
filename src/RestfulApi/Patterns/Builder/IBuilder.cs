namespace Patterns.Builder 
{
    public interface IBuilder<TEntity> 
    {
        TEntity Build();
    }
}