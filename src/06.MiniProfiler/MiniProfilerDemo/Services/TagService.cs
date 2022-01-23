using Microsoft.EntityFrameworkCore;
using MiniProfilerDemo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProfilerDemo.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext context;

        public TagService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<Tag>> GetAll()
        {
            return await this.context.Set<Tag>().ToListAsync();
        }
    }
}
