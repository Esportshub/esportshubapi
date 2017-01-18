namespace Data.App.Models.Builders
{
    public interface IBuilder<TEntity>
    {
        TEntity Build();
    }
}