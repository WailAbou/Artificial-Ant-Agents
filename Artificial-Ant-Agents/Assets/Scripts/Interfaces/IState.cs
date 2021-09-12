public interface IState
{
    void Enter();
    void Excute();
    void ExcutePhysics();
    void Exit();
}