namespace Code.Scripts.StateMachine
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();

        void FixedUpdate();
    }
}