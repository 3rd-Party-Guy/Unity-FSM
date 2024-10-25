using System;
using System.Collections.Generic;
using ThirdPartyGuy.Collections;
using UnityEngine;

namespace ThirdPartyGuy.FSM
{
    [CreateAssetMenu(fileName = "ConditionalTransition", menuName = "3rd-Party-Guy/FSM/Conditional Transition", order = 3)]
    public class ConditionalTransition : ScriptableObject
    {
        public event EventHandler<State> OnTransitionTrigger;


        public bool IsUpdateCheck => isUpdateCheck;
        public bool IsFixedUpdateCheck => isFixedUpdateCheck;
        public bool IsLateUpdateCheck => isLateUpdateCheck;

        [SerializeField] protected State targetState;
        [SerializeField] bool isUpdateCheck;
        [SerializeField] bool isFixedUpdateCheck;
        [SerializeField] bool isLateUpdateCheck;

        public virtual void Initialize(Blackboard context) { }
        public virtual void Check(Blackboard context) { }

        protected void TriggerTransition()
        {
            OnTransitionTrigger?.Invoke(this, targetState);
        }
    }

    public static class ConditionalTransitionExtensions
    {
        public static void Check(this IEnumerable<ConditionalTransition> self, Blackboard context)
        {
            foreach (var transition in self)
            {
                transition.Check(context);
            }
        }
    }
}
