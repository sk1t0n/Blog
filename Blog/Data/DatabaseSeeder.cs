using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class DatabaseSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
            using var context = new ApplicationDbContext(dbContextOptions);

            if (context.Tags.Count() > 0)
            {
                return;
            }

            Tag tag1 = new Tag() { Name = "tag1" };
            Tag tag2 = new Tag() { Name = "tag2" };
            Tag tag3 = new Tag() { Name = "tag3" };

            context.Tags.AddRange(tag1, tag2, tag3);

            Post post1 = new Post()
            {
                Title = "Post1",
                Content = "Post_Content_1",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Tags = new List<Tag> { tag1, tag2 }
            };
            Post post2 = new Post()
            {
                Title = "Post2",
                Content = "Post_Content_2",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Tags = new List<Tag> { tag3 }
            };

            context.Posts.AddRange(post1, post2);

            Comment comment1 = new Comment()
            {
                Content = "Comment1",
                Post = post2,
            };

            context.Comments.Add(comment1);

            context.SaveChanges();
        }
    }
}
