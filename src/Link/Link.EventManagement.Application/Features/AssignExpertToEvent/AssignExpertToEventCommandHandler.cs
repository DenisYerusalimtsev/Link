﻿using Link.Common.Domain.Framework.Frameworks;
using Link.EventManagement.Domain.Services.Interfaces;
using System.Threading.Tasks;

namespace Link.EventManagement.Application.Features.AssignExpertToEvent
{
    public sealed class AssignExpertToEventCommandHandler
        : CommandHandler<AssignExpertToEventCommand, AssignExpertToEventCommand.Reply>
    {
        private readonly IEventRepository _events;

        public AssignExpertToEventCommandHandler(
            ICommandValidator<AssignExpertToEventCommand, 
            AssignExpertToEventCommand.Reply> validator, IEventRepository events) 
            : base(validator)
        {
            _events = events;
        }

        protected override async Task<AssignExpertToEventCommand.Reply> Handle(AssignExpertToEventCommand command)
        {
            var existedEvent = await _events.Get(command.EventId);
            foreach (var expertId in command.ExpertsId)
            {
                existedEvent.Experts.Add(expertId);
            }

            return new AssignExpertToEventCommand.Reply(command.EventId, existedEvent.Experts);
        }
    }
}
