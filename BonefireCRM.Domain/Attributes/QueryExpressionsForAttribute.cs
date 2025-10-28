namespace BonefireCRM.SourceGenerator
{
    // This attribute can be used to mark classes that require filtering and sorting logic to be applied
    // a source generator will create the required code.
    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class QueryExpressionsForAttribute : Attribute
    {
        public Type EntityType { get; set; }

        public QueryExpressionsForAttribute(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
