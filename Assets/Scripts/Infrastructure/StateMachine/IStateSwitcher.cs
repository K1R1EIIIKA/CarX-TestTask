namespace Infrastructure.StateMachine
{
    public interface IStateSwitcher
    {
        void Enter<T>() where T : IGameState;
        void Exit();
    }
}