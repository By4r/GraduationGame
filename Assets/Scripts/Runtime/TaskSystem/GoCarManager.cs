using UnityEngine;

namespace Runtime.TaskSystem
{
    public class GoCarManager:MonoBehaviour
    {
        private bool _isReadyToSearch;
        private bool _isFullTank;
        
        internal void TankAmount(bool state)
        {
            _isFullTank = state;
        }
        
    }
}