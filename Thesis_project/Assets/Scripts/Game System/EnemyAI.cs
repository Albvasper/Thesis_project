using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour {

    #region Singleton Pattern
    private static EnemyAI instance;
    public static EnemyAI Instance { 
        get { 
            return instance; 
        } 
    }
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
        
    }
    #endregion
    
    private int baseLevel;
    // AI resources
    private int money;
    private int assets;
    private int linesOfCode;
    // AI units
    [SerializeField] private List<GameObject> mobileUnits = new List<GameObject>();
    private int maxUnitSpaces;
    private int currentUnitSpaces;
    [SerializeField] private Transform unitsSpawnPoint;
    // Prefabs
    public GameObject recolectorPrefab;
    // Others
    public Slider healthBar;
    protected int maxHP;
    protected int currentHP;
    [SerializeField] private List<GameObject> resourcesAvailable = new List<GameObject>();
    [SerializeField] private List<GameObject> playerUnitsAttacking = new List<GameObject>();
    public Slider lvlUpBar;
    private int lvlUpTime;
    private int cLvlProgress;   // current level up progress
    private float time;
    private int delay;
    private bool lvlUpStudio;
    private bool isBeingAttacked;
    // AI bevahior system
    private BehaviorTree behaviorTree;
    #region BehaviorTree nodes
        private SequenceNode rootNode = new SequenceNode();
        private CheckIfUnderAttackNode checkIfUnderAttackNode;
        private CheckUnitSpacesNode checkUnitSpacesNode;
        private CheckRecolectionUnitsNode checkRecolectionUnitsNode;
        private CheckLevelUpBaseNode checkLevelUpBaseNode;
        private CountEnemyUnitsNode countEnemyUnitsNode;
        private CheckResourceForHouse checkResourceForHouse;
        private CheckResourcesForRecolectionUnits checkResourcesForRecolectionUnits;
        private LevelUpBaseNode levelUpBaseNode;
        private GatherResourceNode gatherResourceNode;  //For level up!
        private CheckOffensiveUnitGeneratorNode checkOffensiveUnitGeneratorNode;
        private BuyHouseNode buyHouseNode;
        private GatherResourceForHouseNode gatherResourceForHouseNode;
        private BuyRecolectionUnitNode buyReolectionUnitNode;
        private GatherResourcesForRecolectionUnitNode gatherResourcesForRecolectionUnitNode;
        private CheckOffensiveAndEnemyUnitsNode checkOffensiveAndEnemyUnitsNode;
        private CheckResourcesForOffensiveGeneratorUnitNode checkResourcesForOffensiveGeneratorUnitNode;
        private BuyOffensiveGeneratorUnitNode buyOffensiveGeneratorUnitNode;
        private GatherResourceForOffGenUnitNode gatherResourceForOffGenUnitNode;
        private ProtectBaseNode protectBaseNode;
        private BuyOffensiveUnitsNode buyOffensiveUnitsNode;
    #endregion

    private void Start() {
        baseLevel = 1;
        money = 0;
        assets = 0;
        linesOfCode = 0;
        maxUnitSpaces = 300;
        lvlUpTime = 30;
        lvlUpBar.maxValue = lvlUpTime;
        lvlUpBar.gameObject.SetActive(false);
        delay = 1;
        cLvlProgress = 0;
        lvlUpStudio = false;
        time = 0;
        currentUnitSpaces = 5;
        maxHP = 100;
        currentHP = maxHP;
        healthBar.maxValue = maxHP;
        isBeingAttacked = false;
        InitBehaviorTree();
    }

    private void Update() {
        behaviorTree.Update();
        LevelUpBase();
        CheckHP();
    }
    
    private void InitBehaviorTree() {
        behaviorTree = new BehaviorTree(rootNode);
        #region First node layer
            // Node 1
            checkIfUnderAttackNode = new CheckIfUnderAttackNode(behaviorTree);
            behaviorTree.AddNode(behaviorTree.GetRoot(), checkIfUnderAttackNode);
            // Node 2
            checkUnitSpacesNode = new CheckUnitSpacesNode(behaviorTree);
            behaviorTree.AddNode(behaviorTree.GetRoot(), checkUnitSpacesNode);
            // Node 3
            checkRecolectionUnitsNode = new CheckRecolectionUnitsNode(behaviorTree);
            behaviorTree.AddNode(behaviorTree.GetRoot(), checkRecolectionUnitsNode);
            // Node 4
            checkLevelUpBaseNode = new CheckLevelUpBaseNode(behaviorTree);
            behaviorTree.AddNode(behaviorTree.GetRoot(), checkLevelUpBaseNode);
        #endregion
        #region Second node layer
            // Node 1
            countEnemyUnitsNode = new CountEnemyUnitsNode(behaviorTree);
            behaviorTree.AddNode(checkIfUnderAttackNode, countEnemyUnitsNode);
            // Node 2
            checkResourceForHouse = new CheckResourceForHouse(behaviorTree);
            behaviorTree.AddNode(checkUnitSpacesNode, checkResourceForHouse);
            // Node 3
            checkResourcesForRecolectionUnits = new CheckResourcesForRecolectionUnits(behaviorTree);
            behaviorTree.AddNode(checkRecolectionUnitsNode, checkResourcesForRecolectionUnits);
            // Node 4
            levelUpBaseNode = new LevelUpBaseNode(behaviorTree);
            behaviorTree.AddNode(checkLevelUpBaseNode, levelUpBaseNode);
            // Node 5
            gatherResourceNode = new GatherResourceNode(behaviorTree);
            behaviorTree.AddNode(checkLevelUpBaseNode, gatherResourceNode);
        #endregion
        #region Third node layer
            // Node 1
            checkOffensiveUnitGeneratorNode = new CheckOffensiveUnitGeneratorNode(behaviorTree);
            behaviorTree.AddNode(countEnemyUnitsNode, checkOffensiveUnitGeneratorNode);
            // Node 2
            buyHouseNode = new BuyHouseNode(behaviorTree);
            behaviorTree.AddNode(checkResourceForHouse, buyHouseNode);
            // Node 3
            gatherResourceForHouseNode = new GatherResourceForHouseNode(behaviorTree);
            behaviorTree.AddNode(checkResourceForHouse, gatherResourceForHouseNode);
            // Node 4
            buyReolectionUnitNode = new BuyRecolectionUnitNode(behaviorTree);
            behaviorTree.AddNode(checkResourcesForRecolectionUnits, buyReolectionUnitNode);
            // Node 5
            gatherResourcesForRecolectionUnitNode = new GatherResourcesForRecolectionUnitNode(behaviorTree);
            behaviorTree.AddNode(checkResourcesForRecolectionUnits, gatherResourcesForRecolectionUnitNode);
        #endregion
        #region Forth node layer
            // Node 1
            checkOffensiveAndEnemyUnitsNode = new CheckOffensiveAndEnemyUnitsNode(behaviorTree);
            behaviorTree.AddNode(checkOffensiveUnitGeneratorNode, checkOffensiveAndEnemyUnitsNode);
            // Node 2
            checkResourcesForOffensiveGeneratorUnitNode = new CheckResourcesForOffensiveGeneratorUnitNode(behaviorTree);
            behaviorTree.AddNode(checkOffensiveUnitGeneratorNode, checkResourcesForOffensiveGeneratorUnitNode);
        #endregion
        #region  Fifth node layer
            // Node 1
            buyOffensiveGeneratorUnitNode = new BuyOffensiveGeneratorUnitNode(behaviorTree);
            behaviorTree.AddNode(checkResourcesForOffensiveGeneratorUnitNode, buyOffensiveGeneratorUnitNode);
            // Node 2
            gatherResourceForOffGenUnitNode = new GatherResourceForOffGenUnitNode(behaviorTree);
            behaviorTree.AddNode(checkResourcesForOffensiveGeneratorUnitNode, gatherResourceForOffGenUnitNode);
        #endregion
        #region  Sixth node layer
            // Node 1
            protectBaseNode = new ProtectBaseNode(behaviorTree);
            behaviorTree.AddNode(checkOffensiveAndEnemyUnitsNode, protectBaseNode);
            // Node 2
            buyOffensiveUnitsNode = new BuyOffensiveUnitsNode(behaviorTree);
            behaviorTree.AddNode(checkOffensiveAndEnemyUnitsNode, buyOffensiveUnitsNode);
        #endregion
    }

    private void CheckHP() {
        healthBar.value = currentHP;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
        if (currentHP <= 0) {
            Die();
        }
    }

    public bool GetIsBeingAttacked() {
        return isBeingAttacked;
    }

    public void IsBeingAttacked(bool b) {
        isBeingAttacked = b;
    }

    private void Die() {

    }

    public void AddMobileUnit(GameObject unit) {
        mobileUnits.Add(unit);
    }
    
    public List<GameObject> GetMobileUnits() {
        return mobileUnits;
    }

    public void ReceiveResource(EnemyRecolectors enemyRecolector) {
        if (enemyRecolector.GetResourceType() == "MONEY") {
            AddMoney(enemyRecolector.GiveResource());
        } else if (enemyRecolector.GetResourceType() == "LINEOFCODE") {
            AddLinesOfCode(enemyRecolector.GiveResource());
        } else {
            AddAssets(enemyRecolector.GiveResource());
        }
    }

    public void SpawnRecolector() {
        Instantiate(recolectorPrefab, unitsSpawnPoint.position, Quaternion.identity);
    }

    public void AddUnitSpaces(int space) {
        currentUnitSpaces += space;
    }

    public void UseMoney(int amount) {
        money -= amount;
    }

    public int GetMaxSpaces() {
        return maxUnitSpaces;
    }

    private void AddMoney(int amount) {
        money += amount;
    }

    private void AddLinesOfCode(int amount) {
        linesOfCode += amount;
    }

    private void AddAssets(int amount) {
        assets += amount;
    }

    public int GetMoney() {
        return money;
    }

    public int GetAssets() {
        return assets;
    }

    public int GetLinesOfCode() {
        return linesOfCode;
    }

    public int GetCurrentUnitSpaces() {
        return currentUnitSpaces;
    }

    public void InitLevelUpBase() {
        lvlUpStudio = true;
    }

    private void LevelUpBase() {
        if (lvlUpStudio == true) {
            lvlUpBar.gameObject.SetActive(true);
            time += Time.deltaTime;
            lvlUpBar.value = cLvlProgress;
            if (time >= delay){
                time = 0f;
                cLvlProgress++;
            }
            if (cLvlProgress >= lvlUpTime) {
                Player.Instance.AddBaseLvl();
                ResetLvlBar();
            }
        }
    }

    private void ResetLvlBar() {
        lvlUpStudio = false;
        cLvlProgress = 0;
        lvlUpBar.gameObject.SetActive(false);
        baseLevel += 1;
        time = 0;
    }

    public int GetBaseLvl() {
        return baseLevel;
    }

    public List<GameObject> GetResourcesAvailable() {
        return resourcesAvailable;
    }
}

// Node implementations
public class CheckIfUnderAttackNode : ConditionNode {
    public CheckIfUnderAttackNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        //Debug.Log("Check if under attack");
        if (EnemyAI.Instance.GetIsBeingAttacked() == true) {
            return true;
        }
        return false;
    }
}

public class CheckUnitSpacesNode : ConditionNode {
    public CheckUnitSpacesNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        //Debug.Log("Check unit spaces");
        if (EnemyAI.Instance.GetMobileUnits().Count == EnemyAI.Instance.GetCurrentUnitSpaces()) {
            return true;
        }
        return false;
    }
}

public class CheckRecolectionUnitsNode : ConditionNode {
    public CheckRecolectionUnitsNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        //Debug.Log("Check Recolection Units Node");
        int counter = 0;
        foreach (GameObject go in EnemyAI.Instance.GetMobileUnits()) {
            if (go.GetComponent<EnemyRecolectors>() == true) {
                counter ++;
            }
        }
        if (counter < 15) {
            return true;
        }
        return false;
    }
}

public class CheckLevelUpBaseNode : ConditionNode {
    public CheckLevelUpBaseNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        //Debug.Log("Check Level Up Base Node");
        if (EnemyAI.Instance.GetMoney() >= 200) {
            return true;
        }
        return false;
    }
}

public class CountEnemyUnitsNode : DecoratorNode {
    public CountEnemyUnitsNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        //Debug.Log("Count EnemyUnits Node");
    }
}

public class CheckResourceForHouse : ConditionNode {
    public CheckResourceForHouse(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        //Debug.Log("Check Resource For House");
        if (EnemyAI.Instance.GetMoney() >= 100) {
            return true;
        }
        return false;
    }
}

public class CheckResourcesForRecolectionUnits : ConditionNode {
    public CheckResourcesForRecolectionUnits(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        //Debug.Log("CheckResourcesForRecolectionUnits");
        if (EnemyAI.Instance.GetMoney() >= 40 && EnemyAI.Instance.GetMobileUnits().Count < EnemyAI.Instance.GetCurrentUnitSpaces()) {
            return true;
        }
        return false;
    }
}

public class LevelUpBaseNode : ActionNode {
    public LevelUpBaseNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        //Debug.Log("LevelUpBaseNode");
        EnemyAI.Instance.UseMoney(300);
        EnemyAI.Instance.InitLevelUpBase();
    }
}

public class GatherResourceNode : ActionNode {
    public GatherResourceNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        foreach (GameObject unit in EnemyAI.Instance.GetMobileUnits()) {
            if (unit.GetComponent<EnemyRecolectors>() == true) {
                if (unit.GetComponent<EnemyRecolectors>().IsFarming() == false) {
                    if (EnemyAI.Instance.GetResourcesAvailable().Count > 0) {
                        unit.GetComponent<EnemyRecolectors>().SetState(new GoingToFarmEnemy_State(
                            unit.GetComponent<EnemyRecolectors>(), 
                            EnemyAI.Instance.GetResourcesAvailable()[0]
                            .GetComponent<StationaryResource>())
                        );
                    }
                }
            }
        }
    }
}

public class CheckOffensiveUnitGeneratorNode : ConditionNode {
    public CheckOffensiveUnitGeneratorNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        // if (/*Theres an offensive unit generator*/) {
        //     return true;
        // }
        return false;
    }
}

public class BuyHouseNode : ActionNode {
    public BuyHouseNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        EnemyAI.Instance.UseMoney(50);
        EnemyAI.Instance.AddUnitSpaces(5);
    }
}

public class GatherResourceForHouseNode : ActionNode {
    public GatherResourceForHouseNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Assign units for collection of materials for houses
    }
}

public class BuyRecolectionUnitNode : ActionNode {
    public BuyRecolectionUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Buy recolection unit
        EnemyAI.Instance.UseMoney(40);
        EnemyAI.Instance.SpawnRecolector();
    }
}

public class GatherResourcesForRecolectionUnitNode : ActionNode {
    public GatherResourcesForRecolectionUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Assign units for collection of materials for recolection unit!
    }
}

public class CheckOffensiveAndEnemyUnitsNode : ConditionNode {
    public CheckOffensiveAndEnemyUnitsNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        // if there are the same or more offensive units than enemy units { 
            //return true;
        //}
        return false;
    }
}

public class CheckResourcesForOffensiveGeneratorUnitNode : ConditionNode {
    public CheckResourcesForOffensiveGeneratorUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        // if there are enough resources to buy a 
        return false;
    }
}

public class BuyOffensiveGeneratorUnitNode : ActionNode {
    public BuyOffensiveGeneratorUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Buy offensive generator unit
    }
}

public class GatherResourceForOffGenUnitNode : ActionNode {
    public GatherResourceForOffGenUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Gather resources for offensive generator unit 
    }
}

public class ProtectBaseNode : ActionNode {
    public ProtectBaseNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Make mobile units attack enemy units that are attackin the base
    }
}

public class BuyOffensiveUnitsNode : ActionNode {
    public BuyOffensiveUnitsNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Buy offensive units until there are the same numbers of enemy units as well as offensive units
    }
}