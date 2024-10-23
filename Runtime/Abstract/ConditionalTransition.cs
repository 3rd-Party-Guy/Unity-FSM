using System;
using System.Collections.Generic;

namespace ThirdPartyGuy.FSM
{
    [System.Serializable]
    public abstract class ConditionalTransition
    {
        public event EventHandler<ConditionalTransition> OnTransitionTrigger;

        public State TargetState { get; private set; }

        public bool IsUpdateCheck { get; private set; }
        public bool IsFixedUpdateCheck { get; private set; }

        public virtual void OnEnter() { }
        public virtual void Check() { }
        public virtual void OnExit() { }

        protected void TriggerTransition()
        {
            OnTransitionTrigger?.Invoke(this, this);
        }
    }

    public static class ConditionalTransitionExtensions
    {
        public static void OnEnter(this IEnumerable<ConditionalTransition> transitions)
        {
            foreach (var transition in transitions)
            {
                transition.OnEnter();
            }
        }
        
        public static void Check(this IEnumerable<ConditionalTransition> transitions)
        {
            foreach (var transition in transitions)
            {
                transition.Check();
            }
        }

        public static void OnExit(this IEnumerable<ConditionalTransition> transitions)
        {
            foreach (var transition in transitions)
            {
                transition.OnExit();
            }
        }
    }
}
