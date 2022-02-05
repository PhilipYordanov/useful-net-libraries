using MiniProfilerDemo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProfilerDemo.Services
{
    public interface IPostService
    {
        Task<IReadOnlyList<Post>> GetAll();

        Task<Post> GetById(int id);

        Task<Post> Create(Post post);
    }
}
