using Blog.Models;

namespace Blog.ViewModels
{
    public class HomePageViewModel
    {
        public List<Post> Posts { get; set; } = default!;
        public List<Tag> Tags { get; set; } = default!;
    }
}
