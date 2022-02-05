using Microsoft.EntityFrameworkCore;
using MiniProfilerDemo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProfilerDemo.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;

        public PostService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Post> Create(Post post)
        {
            await this.context.Set<Post>().AddAsync(post);
            await this.context.SaveChangesAsync();

            return post;
        }

        public async Task<IReadOnlyList<Post>> GetAll()
        {
            return await this.context.Set<Post>().ToListAsync();
        }

        public async Task<Post> GetById(int id)
        {
             var result =await this.context.Posts
                .Include(x => x.PostTags)
                .ThenInclude(y => y.Tag)
                .FirstOrDefaultAsync(z => z.PostId == id);

            return result;
        }
    }
}
