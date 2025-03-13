using CleanArchitectureSample.Application.Abstractions;
using CleanArchitectureSample.Domain.Entities;

namespace CleanArchitectureSample.Application.Webinars.Queries.GetWebinarById;

public sealed record GetWebinarByIdQuery(Guid WebinarId) : IQuery<WebinarResponse>;