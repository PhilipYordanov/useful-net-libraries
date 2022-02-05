using MiniProfilerDemo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProfilerDemo.Services
{
    public interface ITagService
    {
        Task<IReadOnlyList<Tag>> GetAll();
    }
}
