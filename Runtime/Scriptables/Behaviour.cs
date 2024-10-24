using System.Collections.Generic;
using ThirdPartyGuy.Collections;
using UnityEngine;

namespace ThirdPartyGuy.FSM
{
    [CreateAssetMenu(fileName = "Behaviour", menuName = "3rd-Party-Guy/FSM/Behaviour", order = 2)]
    public class Behaviour : ScriptableObject
    {
        public virtual void OnEnter(Blackboard context) { }
        public virtual void Tick(Blackboard context) { }
        public virtual void FixedTick(Blackboard context) { }
        public virtual void LateTick(Blackboard context) { }
        public virtual void OnExit(Blackboard context) { }
    }

    public static class BehaviourExtensions
    {
        public static void OnEnter(this IEnumerable<Behaviour> behaviours, Blackboard blackboard)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnEnter(blackboard);
            }
        }

        public static void Update(this IEnumerable<Behaviour> behaviours, Blackboard blackboard)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Tick(blackboard);
            }
        }

        public static void FixedUpdate(this IEnumerable<Behaviour> behaviours, Blackboard blackboard)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.FixedTick(blackboard);
            }
        }

        public static void LateUpdate(this IEnumerable<Behaviour> behaviours, Blackboard blackboard)
        {
            foreach (var behavior in behaviours)
            {
                behavior.LateTick(blackboard);
            }
        }

        public static void OnExit(this IEnumerable<Behaviour> behaviours, Blackboard blackboard)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnExit(blackboard);
            }
        }
    }
}
