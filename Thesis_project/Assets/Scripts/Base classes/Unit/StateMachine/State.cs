public abstract class State {
    
    protected MobileUnit mobileUnit;
    protected Intern intern;
    protected Unit unit;

    public State(MobileUnit mu) {
        mobileUnit = mu;
    }
    
    public State(Intern i) {
        intern = i;
    }
    
    public State(Unit u) {
        unit = u;
    }

    public abstract void Update();
    public virtual void OnStateEnter() {}
    public virtual void OnStateExit() {}
}