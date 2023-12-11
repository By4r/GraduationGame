using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public byte StageValue;
        public new GameObject light;
        public new GameObject playerEyes;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;

        #endregion

        #region Private Variables

        private PlayerData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPlayerData();
            SendDataToControllers();
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void SendDataToControllers()
        {
            movementController.SetData(_data.MovementData);
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            PlayerSignals.Instance.onMovePlayer += OnPlayerMove;
            PlayerSignals.Instance.onRunPlayer += OnPlayerRun;
            PlayerSignals.Instance.onRunOrSprint += OnRunOrSprint;
            
            PlayerSignals.Instance.onDecreaseStamina += OnDecreaseStamina;
            PlayerSignals.Instance.onIncreaseStamina += OnIncreaseStamina;
        }


        private void OnPlayerMove()
        {
            PlayerSignals.Instance.onMovePlayer?.Invoke();
        }
        private void OnPlayerRun()
        {
            PlayerSignals.Instance.onRunPlayer?.Invoke();
        }
        private void OnRunOrSprint()
        {
            PlayerSignals.Instance.onRunOrSprint?.Invoke();
        }
        private void OnReset()
        {
            StageValue = 0;
            //movementController.OnReset();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            PlayerSignals.Instance.onMovePlayer -= OnPlayerMove;
            PlayerSignals.Instance.onRunPlayer -= OnPlayerRun;
            PlayerSignals.Instance.onRunOrSprint -= OnRunOrSprint;
            
            PlayerSignals.Instance.onDecreaseStamina -= OnDecreaseStamina;
            PlayerSignals.Instance.onIncreaseStamina -= OnIncreaseStamina;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void OnDecreaseStamina()
        {
            PlayerSignals.Instance.onDecreaseStamina?.Invoke();
        }
        private void OnIncreaseStamina()
        {
            PlayerSignals.Instance.onIncreaseStamina?.Invoke();
        }
        
    }
}