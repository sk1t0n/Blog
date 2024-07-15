using Blog.Helpers;
using Blog.Models;

namespace Blog.ViewModels
{
    public class HomePageViewModel
    {
        public PaginatedList<Post> Posts { get; set; } = default!;
        public List<Tag> Tags { get; set; } = default!;
    }
}
