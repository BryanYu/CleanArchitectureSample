using CleanArchitectureSample.Domain.Abstractions;
using CleanArchitectureSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureSample.Infrastructure.Repositories;

public sealed class WebinarRepository : IWebinarRepository
{
    private readonly ApplicationDbContext _dbContext;

    public WebinarRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public void Insert(Webinar webinar) => _dbContext.Set<Webinar>().Add(webinar);

    public async Task<Webinar?> GetWebinar(Guid webinarId)
    {
        return await _dbContext.Set<Webinar>().FirstOrDefaultAsync(item => item.Id == webinarId);
    }
}