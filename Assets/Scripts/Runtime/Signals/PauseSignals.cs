using System;
using Enums;
using Runtime.Extensions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.Signals
{
    public class PauseSignals : MonoSingleton<PauseSignals>
    {
        public UnityAction onPauseGame = delegate { };
        public UnityAction onResumeGame = delegate { };
        public UnityAction onMainMenuGame = delegate { };
        public UnityAction<bool> onPhotoPanelState = delegate { };
        public UnityAction<bool> onPauseState = delegate { };
        public UnityAction<bool> onCanPause = delegate { };
    }
}