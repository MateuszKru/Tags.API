using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tags.Domain.Entities;

namespace Tags.Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Tag> Tags { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DatabaseFacade Database { get; }
    }
}