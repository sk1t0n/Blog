using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Post Post { get; set; } = default!;
        public IdentityUser Author { get; set; } = default!;
    }
}
