using System.Data;
using System.Net.Http.Headers;
using CleanArchitectureSample.Application.Abstractions;
using CleanArchitectureSample.Domain.Abstractions;
using CleanArchitectureSample.Domain.Entities;
using Dapper;

namespace CleanArchitectureSample.Application.Webinars.Queries.GetWebinarById;

public sealed class GetWebinarQueryHandler : IQueryHandler<GetWebinarByIdQuery, WebinarResponse>
{
    private readonly IWebinarRepository _webinarRepository;

    public GetWebinarQueryHandler(IWebinarRepository webinarRepository) => _webinarRepository = webinarRepository;

    public async Task<WebinarResponse> Handle(GetWebinarByIdQuery request, CancellationToken cancellationToken = default)
    {
        var webinar = await _webinarRepository.GetWebinar(request.WebinarId);
        if (webinar is null)
        {
            throw new Exception();
        }
        return new WebinarResponse(webinar.Name, webinar.ScheduledOn);
    }
}