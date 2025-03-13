using CleanArchitectureSample.Application.Abstractions;
using CleanArchitectureSample.Domain.Abstractions;
using CleanArchitectureSample.Domain.Entities;

namespace CleanArchitectureSample.Application.Webinars.Commands.CreateWebinar;

public class CreateWebinarCommandHandler : ICommandHandler<CreateWebinarCommand, Guid>
{
    private readonly IWebinarRepository _webinarRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWebinarCommandHandler(IWebinarRepository webinarRepository, IUnitOfWork unitOfWork)
    {
        _webinarRepository = webinarRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(CreateWebinarCommand request, CancellationToken cancellationToken)
    {
        var webinar = new Webinar(Guid.NewGuid(), request.Name, request.ScheduledOn);
        _webinarRepository.Insert(webinar);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return webinar.Id;
    }
}