﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Source.Scripts.Core.StateMachine.States.Base
{
    public abstract class BaseState<TTrigger> : IState<TTrigger>
        where TTrigger : Enum
    {
        private readonly Dictionary<TTrigger, Func<Task>> _internalTriggers;
        
        protected BaseState()
        {
            _internalTriggers = new Dictionary<TTrigger, Func<Task>>();
        }

        public bool HasInternal(TTrigger trigger) => 
            _internalTriggers.ContainsKey(trigger);

        public async Task Internal(TTrigger trigger)
        {
            var action = _internalTriggers[trigger];

            await action();
        }

        public abstract Task OnEntry();

        public abstract Task OnExit();

        public virtual void Configure() {}

        protected void InternalTransition(TTrigger trigger, Func<Task> action) => 
            _internalTriggers.Add(trigger, action);
    }
}