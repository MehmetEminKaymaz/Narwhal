using narwhalLang.Domain;

namespace narwhalLang.CompilerEngine
{
    public class AnalyzerModel
    {
        public Guid NarwhalNodeId { get; set; }
        public List<Guid> UsedNarwhalPropertyList { get; set; }
        public List<Guid> NeededReadOnlyPropertyValues { get; set; }
        public int ExecutionOrder { get; set; }
    }

    public class AnalyzedItem
    {
        public Guid VisualNodeId { get; set; }
        public NarwhalNode ReferencedRealNarwhalNode { get; set; }
        public List<NarwhalProperty> UsedNarwhalPropertyList { get; set; }
        public List<NarwhalReadOnlyProperty> NeededReadOnlyPropertyValues { get; set; }
        public int ExecutionOrder { get; set; }

    }

    public class Analyzer
    {
        public void AnalyzeUIModel(string rawModel)
        {

        }
    }
}
