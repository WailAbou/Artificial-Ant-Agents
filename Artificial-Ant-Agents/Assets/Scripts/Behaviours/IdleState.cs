public class IdleState : IState
{
    private AntController antController;

    public IdleState(AntController antController)
    {
        this.antController = antController;
    }

    public void Enter() { }

    public void Excute() { }

    public void ExcutePhysics() { }

    public void Exit() { }
}