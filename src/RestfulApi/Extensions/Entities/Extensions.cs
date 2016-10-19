namespace EsportshubApi.Extensions
{
    public static class Extensions
    {
        public static bool CompareEntities(this object @this, object compareObject)
        {
            if (@this == null || @this.GetType() != compareObject.GetType())
                return false;

            return true;
        }
    }

}