using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class PostsRepository
    {
        private ApplicationDbContext _context;

        public PostsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Post> FindPosts()
        {
            return from post in _context.Posts
                   orderby post.Title
                   select post;
        }

        public IQueryable<Post> FindPostsByTagId(int tagId)
        {
            return _context.Posts
                .Where(p => p.Tags.Any(t => t.Id == tagId))
                .OrderBy(p => p.Title);
        }

        public async Task<Post?> FindPostById(Guid id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post?> FindPostWithTagsAndComments(Guid id)
        {
            return await _context.Posts
               .Include("Tags")
               .Include("Comments")
               .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePost(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
