using MediatRFluentDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRFluentDemo.Context
{
    public interface IApplicationContext
    {
        DbSet<Milestone> Milestones{ get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
