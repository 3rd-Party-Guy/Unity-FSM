using System.Collections.Generic;

namespace ThirdPartyGuy.FSM
{
    [System.Serializable]
    public abstract class Behaviour
    {
        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnExit() { }
    }

    public static class BheaviourExtensions
    {
        public static void OnEnter(this IEnumerable<Behaviour> behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnEnter();
            }
        }

        public static void Update(this IEnumerable<Behaviour> behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Update();
            }
        }

        public static void FixedUpdate(this IEnumerable<Behaviour> behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.FixedUpdate();
            }
        }

        public static void OnExit(this IEnumerable<Behaviour> behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.OnExit();
            }
        }
    }
}
