public abstract class State {
    
    protected MobileUnit unit;
    protected Developer developer;

    public State(MobileUnit u) {
        unit = u;
    }
    
    public State(Developer dev) {
        developer = dev;
    }
    
    public abstract void Update();
    public virtual void OnStateEnter() {}
    public virtual void OnStateExit() {}
}