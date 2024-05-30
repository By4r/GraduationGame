using Runtime.Controllers;
using Runtime.Controllers.Camera;
using Runtime.Controllers.Player;
using Runtime.Controllers.Security_Room;
using Runtime.Controllers.Subtitle;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Managers;
using Runtime.StateManagers;
using Runtime.TaskStateSystem.TaskStates;
using Runtime.TaskStateSystem.TaskUI;
using Runtime.TaskSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.TaskStateSystem
{
    public class TaskStateManager : MonoBehaviour
    {
        private ITaskState _currentState;

        [SerializeField] private TaskController taskController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private SleepController sleepController;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private PlayerPickUpController playerPickUpController;
        [SerializeField] private CheckHouseManager checkHouseManager;
        [SerializeField] private CheckOfficeManager checkOfficeManager;
        [SerializeField] private LetterManager letterManager;
        [SerializeField] private GoCarManager goCarManager;
        [SerializeField] private PlaySubtitle playSubtitle;
        [SerializeField] private TaskInfoManager taskInfoManager;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private CamScareManager camScareManager;
        [SerializeField] private CheckCameraManager checkCameraManager;
        [SerializeField] private SecurityRoomController securityRoomController;
        [SerializeField] private CheckAtticManager checkAtticManager;
        [SerializeField] private HeatHouseManager heatHouseManager;

        [ShowInInspector] private WorkData _workData;

        private void Awake()
        {
            _workData = GetData();
        }

        private void Start()
        {
            // Initialize the state machine with an initial state
            SetState(new PickUpPhoneState());
            //SetState(new CollectGarbageState());
            //SetState(new GoSleepState());
            //SetState(new SweepFloorState());
            //SetState(new WateringFlowerState());
            //SetState(new CheckHouseState());
            //SetState(new CheckOfficeState());
            //SetState(new AtticState());
            //SetState(new CheckCameraState());
            //SetState(new GoCarState());
            //SetState(new HeatHouseState());
        }

        private void Update()
        {
            // Update the current state
            _currentState?.UpdateState(this);
        }

        public void SetState(ITaskState newState)
        {
            // Exit the current state
            _currentState?.ExitState(this);

            // Set the new state
            _currentState = newState;

            // Enter the new state
            _currentState?.EnterState(this);
        }

        public TaskController GetTaskController() => taskController;
        public PlayerPhysicsController GetPlayerPhysicsController() => playerPhysicsController;
        public SleepController GetSleepController() => sleepController;
        public AudioSource GetAudioSource() => audioSource;
        public PlayerPickUpController GetPlayerPickUpController() => playerPickUpController;
        public CheckHouseManager GetCheckHouseManager() => checkHouseManager;
        public CheckOfficeManager GetCheckOfficeManager() => checkOfficeManager;
        public LetterManager GetLetterManager() => letterManager;
        public GoCarManager GetGoCarManager() => goCarManager;
        public WorkData GetWorkData() => _workData;

        public PlaySubtitle GetPlaySubtitle() => playSubtitle;

        public TaskInfoManager GetTaskInfoManager() => taskInfoManager;

        public PlayerMovementController GetPlayerMovementController() => playerMovementController;

        public CameraController GetCameraController() => cameraController;

        public CamScareManager GetCamScareManager() => camScareManager;
        

        public PlayerManager GetPlayerManager() => playerManager;

        public CheckCameraManager GetCheckCameraManager() => checkCameraManager;

        public SecurityRoomController GetSecurityRoomController() => securityRoomController;

        public CheckAtticManager GetCheckAtticManager() => checkAtticManager;

        public HeatHouseManager GetHeatHouseManager() => heatHouseManager;
        
        private WorkData GetData()
        {
            return Resources.Load<CD_Work>("Data/CD_Work").workData;
        }
    }
}