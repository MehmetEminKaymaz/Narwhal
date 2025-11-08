namespace narwhalLang.Domain
{
    public class NarwhalReadOnlyProperty : BaseDomain
    {
        public Guid NarwhalNodeId { get; set; }
        public Guid? ParentReadOnlyPropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyRuntimeValueType { get; set; }
        public string PropertyDefinition { get; set; }
        public bool IsNestedProperty { get; set; }
    }
}
