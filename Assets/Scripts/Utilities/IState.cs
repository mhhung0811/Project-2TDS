public interface IState
{
    public void Enter();
    public void FrameUpdate();
    public void PhysicsUpdate();
    public void Exit();
}