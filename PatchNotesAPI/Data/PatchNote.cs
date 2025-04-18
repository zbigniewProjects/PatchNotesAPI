namespace DestructorsNetApi.Data
{
    public class PatchNote
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedAt { get; set; }
    }
}