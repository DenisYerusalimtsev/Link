﻿using Link.Common.Domain.Framework.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Link.EventManagement.Application.Frameworks
{
    public abstract class CommandHandler<T, TReply> : ICommandHandler
        where T : class, ICommand<TReply>
        where TReply : class, ICommandReply
    {
        private readonly ICommandValidator<T, TReply> _validator;
        protected readonly IList<IDomainEvent> DomainEvents;

        protected CommandHandler(ICommandValidator<T, TReply> validator)
        {
            _validator = validator;
            DomainEvents = new List<IDomainEvent>();
        }

        public virtual async Task<TR> Handle<TR>(ICommand<TR> command)
            where TR : class, ICommandReply
        {
            T typedCommand = command as T;
            _validator.Validate(typedCommand);

            var result = await Handle(typedCommand);

            return result as TR;
        }

        protected abstract Task<TReply> Handle(T command);

        protected void RaiseEvent(IDomainEvent @event)
        {
            DomainEvents.Add(@event);
        }
    }
}
