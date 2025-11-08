namespace narwhalLang.Domain
{
    public class NarwhalProperty : BaseDomain
    {
        public Guid NarwhalNodeId { get; set; }
        public Guid? ParentPropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyRuntimeValueType { get; set; }
        public string PropertyDefinition { get; set; }
        public bool IsRequired { get; set; }
        public bool IsNestedProperty { get; set; }
        public bool IsConfigForDockerProvider {  get; set; }
    }
}
