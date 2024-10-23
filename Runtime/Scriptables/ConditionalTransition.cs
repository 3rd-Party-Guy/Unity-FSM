using System;
using System.Collections.Generic;
using ThirdPartyGuy.Collections;
using UnityEngine;

namespace ThirdPartyGuy.FSM
{
    [CreateAssetMenu(fileName = "ConditionalTransition", menuName = "3rd-Party-Guy/FSM/Conditional Transition", order = 3)]
    public class ConditionalTransition : ScriptableObject
    {
        public event EventHandler<ConditionalTransition> OnTransitionTrigger;

        public State TargetState { get; private set; }

        public bool IsUpdateCheck => isUpdateCheck;
        public bool IsFixedUpdateCheck => isFixedUpdateCheck;
        public bool IsLateUpdateCheck => isLateUpdateCheck;

        [SerializeField] bool isUpdateCheck;
        [SerializeField] bool isFixedUpdateCheck;
        [SerializeField] bool isLateUpdateCheck;

        public virtual void Initialize(Blackboard context) { }
        public virtual bool Check(Blackboard context) { return false; }

        protected void TriggerTransition()
        {
            OnTransitionTrigger?.Invoke(this, this);
        }
    }
}
