namespace StateMachines
{
    public interface IFSMState
    {
        void Init();
        void OnEnter();
        void Update();
        void ChangeState();
        void OnExit();
    }
}
