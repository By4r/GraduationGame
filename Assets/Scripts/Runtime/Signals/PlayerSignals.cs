using Runtime.Enums;
using Runtime.Extensions;

using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction onMovePlayer = delegate { };
        public UnityAction onRunPlayer = delegate { };
        public UnityAction onRunOrSprint = delegate { };
        public UnityAction onDecreaseStamina = delegate { };
        public UnityAction onIncreaseStamina = delegate { };
        public UnityAction onIncreaseMentalHealth = delegate { };
        public UnityAction onDecreaseMentalHealth = delegate { };
        
    }
}