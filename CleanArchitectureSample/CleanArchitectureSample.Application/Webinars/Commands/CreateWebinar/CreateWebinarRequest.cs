﻿namespace CleanArchitectureSample.Application.Webinars.Commands.CreateWebinar;

public sealed record CreateWebinarRequest(string Name, DateTime ScheduledOn);