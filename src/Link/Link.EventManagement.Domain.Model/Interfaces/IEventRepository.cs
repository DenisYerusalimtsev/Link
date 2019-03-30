﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Link.EventManagement.Domain.Model.Entities;

namespace Link.EventManagement.Domain.Model.Interfaces
{
    public interface IEventRepository
    {
        List<Event> Get();

        Event Get(EventId id);

        Task<Event> Create(Event ev);

        void Update(EventId id, Event ev);

        void Remove(EventId eventId);
    }
}
