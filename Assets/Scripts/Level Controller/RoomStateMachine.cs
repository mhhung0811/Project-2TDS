public class RoomStateMachine
{
    public IState CurrentState { get; private set; }
    
    public IdleState idleState { get; }
    public SetupState setupState { get; }
    public ActiveState activeState { get; }
    public TriggeredState triggeredState { get; }
    
    public RoomStateMachine(RoomController roomController)
    {
        idleState = new IdleState(roomController, this);
        setupState = new SetupState(roomController, this);
        activeState = new ActiveState(roomController, this);
        triggeredState = new TriggeredState(roomController, this);
    }
    
    public void Initialize(bool isActiveRoom)
    {
        CurrentState = isActiveRoom ? activeState : idleState;
        CurrentState.Enter();
    }
    
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = setupState;
        CurrentState.Enter();
    }
    
    public void FrameExecute()
    {
        CurrentState.FrameUpdate();
    }
    
    public void PhysicsExecute()
    {
        CurrentState.PhysicsUpdate();
    }
}