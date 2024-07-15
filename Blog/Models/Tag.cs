namespace Blog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
