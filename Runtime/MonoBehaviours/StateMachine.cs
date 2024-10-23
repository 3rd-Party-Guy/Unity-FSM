using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using ThirdPartyGuy.Collections;

namespace ThirdPartyGuy.FSM
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] State startState;
        [SerializeField] BlackboardData blackboardInitializationData;

        public State State { get; private set; }
        Blackboard blackboard = new();

        IEnumerable<ConditionalTransition> checks;
        IEnumerable<ConditionalTransition> updateChecks;
        IEnumerable<ConditionalTransition> fixedUpdateChecks;
        IEnumerable<ConditionalTransition> lateUpdateChecks;

        private void Awake()
        {
            blackboardInitializationData.SetValuesOnBlackboard(blackboard);
        }

        public void ChangeState(State newState)
        {
            if (newState == null)
            {
                Debug.LogError("3rd-Party-Guy.FSM: Attempting to change to a null State");
                return;
            }

            State.Behaviours.OnExit(blackboard);
            State = newState;
            State.Behaviours.OnEnter(blackboard);

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
            State.Behaviours.Update(blackboard);

            foreach (var check in updateChecks)
            {
                check.Check(blackboard);
            }
        }

        void FixedUpdate()
        {
            State.Behaviours.FixedUpdate(blackboard);

            foreach (var check in fixedUpdateChecks)
            {
                check.Check(blackboard);
            }
        }

        private void LateUpdate()
        {
            State.Behaviours.LateUpdate(blackboard);

            foreach (var check in lateUpdateChecks)
            {
                check.Check(blackboard);
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

            checks = State.Transitions;
            updateChecks = checks.Where(e => e.IsUpdateCheck);
            fixedUpdateChecks = checks.Where(e => e.IsFixedUpdateCheck);
            lateUpdateChecks = checks.Where(e => e.IsLateUpdateCheck);

            foreach (var check in checks)
            {
                check.Initialize(blackboard);
                check.OnTransitionTrigger += OnTransitionTriggered;
            }
        }

        void OnTransitionTriggered(object sender, ConditionalTransition transition)
        {
            ChangeState(transition.TargetState);
        }
    }
}
