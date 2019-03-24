﻿using Link.Common.Domain.Framework.Models;

namespace Link.EventManagement.Domain.Model.Entities
{
    public class EventId : ValueObject<EventId>
    {
        public static EventId NewEventId => new EventId(0);

        public EventId(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public bool IsValid => Id > 0;

        protected override bool EqualsCore(EventId other)
        {
            return Id == other.Id;
        }

        protected override int GetHashCodeCore()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
