using System.Collections.Generic;
using UnityEngine;

namespace ThirdPartyGuy.FSM
{
    [CreateAssetMenu(fileName = "State", menuName = "3rdPartyGuy/FSM/State", order = 1)]
    public sealed class State : ScriptableObject
    {
        [SerializeReference] List<ConditionalTransition> transitions;
        [SerializeReference] List<Behaviour> behaviours;

        public IReadOnlyList<ConditionalTransition> Transitions => transitions;
        public IReadOnlyList<Behaviour> Behaviours => behaviours;
    }
}