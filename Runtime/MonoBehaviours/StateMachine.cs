using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ThirdPartyGuy.FSM
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField, SerializeReference] State startState;
        [SerializeField, SerializeReference] State state;

        public State State => state;

        IEnumerable<ConditionalTransition> checks;
        IEnumerable<ConditionalTransition> updateChecks;
        IEnumerable<ConditionalTransition> fixedUpdateChecks;

        public void ChangeState(State newState)
        {
            if (newState == null)
            {
                Debug.LogError("3rd-Party-Guy.FSM: Attempting to change to a null state");
                return;
            }

            state.Behaviours.OnExit();
            state = newState;
            state.Behaviours.OnEnter();

            UpdateCheckLists();
        }

        void Start()
        {
            if (startState == null)
            {
                Debug.LogError("3rd-Party-Guy.FSM: Start State not defined");
                return;
            }

            ChangeState(startState);
        }

        void Update()
        {
            state.Behaviours.Update();

            foreach (var check in updateChecks)
            {
                check.Check();
            }
        }

        void FixedUpdate()
        {
            state.Behaviours.FixedUpdate();

            foreach (var check in fixedUpdateChecks)
            {
                check.Check();
            }
        }

        void UpdateCheckLists()
        {
            if (checks != null)
            {
                foreach (var check in checks)
                {
                    check.OnTransitionTrigger -= OnTransitionTriggered;
                }
            }

            checks = state.Transitions;
            updateChecks = checks.Where(e => e.IsUpdateCheck);
            fixedUpdateChecks = checks.Where(e => e.IsFixedUpdateCheck);

            foreach (var check in checks)
            {
                check.OnTransitionTrigger += OnTransitionTriggered;
            }
        }

        void OnTransitionTriggered(object sender, ConditionalTransition transition)
        {
            ChangeState(transition.TargetState);
        }
    }
}
