using Enums;
using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine.Events;


namespace Runtime.Signals
{
    public class StateSignals : MonoSingleton<StateSignals>
    {
        public UnityAction<GameStates> onSetGameState = delegate { };
    }
}