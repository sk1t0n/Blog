using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class TagsRepository
    {
        private ApplicationDbContext _context;

        public TagsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> FindTags()
        {
            return await _context.Tags.ToListAsync();
        }
    }
}
