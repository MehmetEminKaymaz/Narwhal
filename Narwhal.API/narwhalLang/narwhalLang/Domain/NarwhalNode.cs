namespace narwhalLang.Domain
{
    public class NarwhalNode : BaseDomain
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsResource { get; set; }
        public bool IsDataSource { get; set; }
        public string CompilerModel { get; set; }
        public string VisualName { get; set; }
        public string VisualType { get; set; }
        public string GlobalSupportedLibraryName { get; set; }
        // public string ModelTemplate { get; set; }

    }
}
