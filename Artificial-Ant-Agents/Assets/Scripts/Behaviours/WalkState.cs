public class WalkState : IState
{
    private AntController antController;

    public WalkState(AntController antController)
    {
        this.antController = antController;
    }

    public void Enter() { }

    public void Excute() { }

    public void ExcutePhysics() { }

    public void Exit() { }
}