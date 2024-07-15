using Blog.Data;
using Blog.Data.Repositories;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PostsRepository _postsRepository;
        private readonly TagsRepository _tagsRepository;

        public PostsController(
            ApplicationDbContext context,
            PostsRepository postsRepository,
            TagsRepository tagsRepository)
        {
            _context = context;
            _postsRepository = postsRepository;
            _tagsRepository = tagsRepository;
        }

        // GET: Posts
        public async Task<IActionResult> Index(int? tagId)
        {
            List<Post> posts = default!;

            if (tagId is not null)
            {
                posts = await _postsRepository.FindPostsByTagId(tagId.Value);
            }
            else
            {
                posts = await _postsRepository.FindPosts();
            }

            var viewModel = new HomePageViewModel()
            {
                Posts = posts,
                Tags = await _tagsRepository.FindTags()
            };
            return View(viewModel);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postsRepository.FindPostWithTagsAndComments(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            PostDetailsPageViewModel viewModel = new PostDetailsPageViewModel()
            {
                Post = post,
                Tags = string.Join(" ", post.Tags.Select(t => t.Name)),
                Comments = post.Comments
            };

            return View(viewModel);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();
                post.CreatedAt = DateTime.Now;
                post.UpdatedAt = post.CreatedAt;
                await _postsRepository.AddPost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postsRepository.FindPostById(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Content,CreatedAt")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.UpdatedAt = DateTime.Now;
                    await _postsRepository.UpdatePost(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_postsRepository.PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postsRepository.FindPostById(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _postsRepository.FindPostById(id);
            if (post != null)
            {
               await _postsRepository.RemovePost(post);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
