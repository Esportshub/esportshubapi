namespace EsportshubApi.Extensions
{
    public static class Extensions
    {
        public static bool CompareEntities(this object @this, object compareObject)
        {
            return @this != null && @this.GetType() == compareObject.GetType();
        }
    }

}