namespace StateMachines
{
    public interface IFSMState
    {
        void Enter();

        void Update();

        void Exit();
    }
}
