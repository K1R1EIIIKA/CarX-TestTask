namespace Infrastructure.StateMachine
{
    public interface IGameState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}