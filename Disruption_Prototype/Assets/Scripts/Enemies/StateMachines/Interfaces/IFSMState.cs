namespace StateMachines
{
    public interface IFSMState
    {
        void OnEnter();
        void Update();
        void ChangeState();
        void OnExit();
    }
}
