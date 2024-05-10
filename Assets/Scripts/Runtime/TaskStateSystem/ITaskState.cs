using Runtime.Managers;

namespace Runtime.TaskStateSystem
{
    public interface ITaskState
    {
        void EnterState(TaskStateManager stateManager);
        void UpdateState(TaskStateManager stateManager);
        void ExitState(TaskStateManager stateManager);
    }
}