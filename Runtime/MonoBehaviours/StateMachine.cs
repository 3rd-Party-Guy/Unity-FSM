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
        public Blackboard Blackboard { get; } = new();

        IEnumerable<ConditionalTransition> checks;
        IEnumerable<ConditionalTransition> updateChecks;
        IEnumerable<ConditionalTransition> fixedUpdateChecks;
        IEnumerable<ConditionalTransition> lateUpdateChecks;

        private void Awake()
        {
            if (blackboardInitializationData != null)
            {
                blackboardInitializationData.SetValuesOnBlackboard(Blackboard);
            }
        }

        private void OnDestroy()
        {
            State.Behaviours.OnExit(Blackboard);
            Blackboard.Clear();
        }

        public void ChangeState(State newState)
        {
            if (newState == null)
            {
                Debug.LogError("3rd-Party-Guy.FSM: Attempting to change to a null State");
                return;
            }

            State.Behaviours.OnExit(Blackboard);
            State = newState;
            State.Behaviours.OnEnter(Blackboard);

            UpdateCheckLists();
        }

        void Start()
        {
            if (startState == null)
            {
                Debug.LogError("3rd-Party-Guy.FSM: Start State not defined");
                return;
            }

            State = startState;
            UpdateCheckLists();
        }

        void Update()
        {
            State.Behaviours.Update(Blackboard);

            foreach (var check in updateChecks)
            {
                check.Check(Blackboard);
            }
        }

        void FixedUpdate()
        {
            State.Behaviours.FixedUpdate(Blackboard);

            foreach (var check in fixedUpdateChecks)
            {
                check.Check(Blackboard);
            }
        }

        private void LateUpdate()
        {
            State.Behaviours.LateUpdate(Blackboard);

            foreach (var check in lateUpdateChecks)
            {
                check.Check(Blackboard);
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
                check.Initialize(Blackboard);
                check.OnTransitionTrigger += OnTransitionTriggered;
            }
        }

        void OnTransitionTriggered(object sender, State targetState)
        {
            ChangeState(targetState);
        }
    }
}
