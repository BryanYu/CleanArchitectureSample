using CleanArchitectureSample.Application.Abstractions;

namespace CleanArchitectureSample.Application.Webinars.Commands.CreateWebinar;

public sealed record CreateWebinarCommand(string Name, DateTime ScheduledOn) : ICommand<Guid>;