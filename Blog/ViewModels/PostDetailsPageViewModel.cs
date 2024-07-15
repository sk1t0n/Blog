using Blog.Models;

namespace Blog.ViewModels
{
    public class PostDetailsPageViewModel
    {
        public Post Post { get; set; } = default!;
        public string Tags { get; set; } = default!;
        public List<Comment> Comments { get; set; } = default!;
    }
}
