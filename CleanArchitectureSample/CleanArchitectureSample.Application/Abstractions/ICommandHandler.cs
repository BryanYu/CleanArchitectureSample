﻿using MediatR;

namespace CleanArchitectureSample.Application.Abstractions;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where  TCommand : ICommand<TResponse>
{
}