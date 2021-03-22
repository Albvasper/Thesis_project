public abstract class State {
    
    protected MobileUnit mobileUnit;
    protected Intern intern;
    protected Unit unit;
    protected EnemyRecolectors enemyRecolector;

    public State(MobileUnit mu) {
        mobileUnit = mu;
    }
    
    public State(Intern i) {
        intern = i;
    }
    
    public State(Unit u) {
        unit = u;
    }

    public State(EnemyRecolectors er) {
        enemyRecolector = er;
    }

    public abstract void Update();
    public virtual void OnStateEnter() {}
    public virtual void OnStateExit() {}
}