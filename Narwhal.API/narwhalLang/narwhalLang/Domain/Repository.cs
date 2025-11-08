namespace narwhalLang.Domain
{
    public class Repository : BaseDomain
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public int StarCount { get; set; }
        public int ForkCount { get; set; }
        public string ByteCode { get; set; }
    }
}
