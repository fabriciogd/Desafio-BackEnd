﻿using Moto.Application.Base;
using Moto.Domain.Entities;
using Newtonsoft.Json;

namespace Moto.Application.Extensions;

public static class IntegrationEventExtensions
{
    public static Event ToJsonEventData(this IIntegrationEvent @event)
    {
        return Event.Create(
            @event.GetGenericTypeName(),
            JsonConvert.SerializeObject(@event)
        );
    }
}
