public abstract class State {
    
    protected MobileUnit mobileUnit;
    protected Developer developer;
    protected Unit unit;

    public State(MobileUnit mu) {
        mobileUnit = mu;
    }
    
    public State(Developer dev) {
        developer = dev;
    }
    
    public State(Unit u) {
        unit = u;
    }

    public abstract void Update();
    public virtual void OnStateEnter() {}
    public virtual void OnStateExit() {}
}