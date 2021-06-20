using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour {

    #region Singleton Pattern
        private static EnemyAI instance;
        public static EnemyAI Instance { 
            get { 
                return instance; 
            } 
        }
        
        private void Awake() {
            InitBehaviorTree();
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
    // AI unit managment
    [SerializeField] private List<GameObject> mobileUnits = new List<GameObject>();
    [SerializeField] private List<GameObject> offensiveUnitGenerators = new List<GameObject>();
    [SerializeField] private List<GameObject> currentAttackers = new List<GameObject>();
    [SerializeField] private List<GameObject> walls = new List<GameObject>();
    [SerializeField] private Transform unitSpawnPoint;
    private int maxUnitSpaces;
    private int currentUnitSpaces;
    // Unit prefabs
    [SerializeField] private GameObject bugGeneratorPrefab;
    [SerializeField] private GameObject criticGenerator;
    [SerializeField] private GameObject selfDoubtGenerator;
    // Other
    [SerializeField] private List<GameObject> resourcesAvailable = new List<GameObject>();
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
        private CheckIfWallCanBeBoughtNode checkIfWallCanBeBoughtNode;
        private BuyWallNode buyWallNode;
        private CheckNumberOfWalls checkNumberOfWalls;
    #endregion

    private void Start() {
        baseLevel = 1;
        money = 0;
        assets = 0;
        linesOfCode = 0;
        maxUnitSpaces = 300;
        currentUnitSpaces = 5;
    }

    private void Update() {
        behaviorTree.Update();
        /* If all recolection units are dead and theres
        no money left, the AI will lose the game.*/
        if (mobileUnits.Count < 0 && money < 40) {
            SceneManager.LoadScene(3);
        }
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
            checkNumberOfWalls = new CheckNumberOfWalls(behaviorTree);
            behaviorTree.AddNode(behaviorTree.GetRoot(), checkNumberOfWalls);
            // Node 5
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
            checkIfWallCanBeBoughtNode = new CheckIfWallCanBeBoughtNode(behaviorTree);
            behaviorTree.AddNode(checkNumberOfWalls, checkIfWallCanBeBoughtNode);
            // Node 5
            levelUpBaseNode = new LevelUpBaseNode(behaviorTree);
            behaviorTree.AddNode(checkLevelUpBaseNode, levelUpBaseNode);
            // Node 6
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
            // Node 6
            buyWallNode = new BuyWallNode(behaviorTree);
            behaviorTree.AddNode(checkIfWallCanBeBoughtNode, buyWallNode);
        #endregion
        #region Forth node layer
            // Node 1
            checkOffensiveAndEnemyUnitsNode = new CheckOffensiveAndEnemyUnitsNode(behaviorTree);
            behaviorTree.AddNode(checkOffensiveUnitGeneratorNode, checkOffensiveAndEnemyUnitsNode);
            // Node 2
            checkResourcesForOffensiveGeneratorUnitNode = new CheckResourcesForOffensiveGeneratorUnitNode(behaviorTree);
            behaviorTree.AddNode(checkOffensiveUnitGeneratorNode, checkResourcesForOffensiveGeneratorUnitNode);
        #endregion
        #region Fifth node layer
            // Node 1
            buyOffensiveGeneratorUnitNode = new BuyOffensiveGeneratorUnitNode(behaviorTree);
            behaviorTree.AddNode(checkResourcesForOffensiveGeneratorUnitNode, buyOffensiveGeneratorUnitNode);
            // Node 2
            gatherResourceForOffGenUnitNode = new GatherResourceForOffGenUnitNode(behaviorTree);
            behaviorTree.AddNode(checkResourcesForOffensiveGeneratorUnitNode, gatherResourceForOffGenUnitNode);
        #endregion
        #region Sixth node layer
            // Node 1
            protectBaseNode = new ProtectBaseNode(behaviorTree);
            behaviorTree.AddNode(checkOffensiveAndEnemyUnitsNode, protectBaseNode);
            // Node 2
            buyOffensiveUnitsNode = new BuyOffensiveUnitsNode(behaviorTree);
            behaviorTree.AddNode(checkOffensiveAndEnemyUnitsNode, buyOffensiveUnitsNode);
        #endregion
    }

    public void IsBeingAttacked(Unit attacker) {
        //isBeingAttacked = true;
        if (currentAttackers.Contains(attacker.GetGameObject()) == false) {
            currentAttackers.Add(attacker.GetGameObject());
        }
    }

    public void AddMobileUnit(GameObject unit) {
        mobileUnits.Add(unit);
    }
    
    public List<GameObject> GetMobileUnits() {
        return mobileUnits;
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

    public void AddBaseLvl() {
        baseLevel++;
    }

    public void AddMoney(int amount) {
        money += amount;
    }

    public void AddLinesOfCode(int amount) {
        linesOfCode += amount;
    }

    public void AddAssets(int amount) {
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

    public int GetBaseLvl() {
        return baseLevel;
    }

    public List<GameObject> GetResourcesAvailable() {
        return resourcesAvailable;
    }

    public List<GameObject> GetOffensiveUnitGenerators() {
        return offensiveUnitGenerators;
    }

    public void BuildBugGenerator() {
        Instantiate(bugGeneratorPrefab, unitSpawnPoint.position, Quaternion.identity);
    }

    public void BuildSelfDoubtGenerator() {
        Instantiate(bugGeneratorPrefab, unitSpawnPoint.position, Quaternion.identity);
    }

    public void BuildCriticGenerator() {
        Instantiate(criticGenerator, unitSpawnPoint.position, Quaternion.identity);
    }

    public List<GameObject> GetCurrentAttackers() {
        return currentAttackers;
    }

    public List<GameObject> GetWalls() {
        return walls;
    }
}

// Node implementations
public class CheckIfUnderAttackNode : ConditionNode {
    public CheckIfUnderAttackNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        //Debug.Log("Check if under attack");
        if (EnemyAI.Instance.GetCurrentAttackers().Count > 0) {
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
        for (int i = 0; i < EnemyAI.Instance.GetMobileUnits().Count; i++) {
            if (EnemyAI.Instance.GetMobileUnits()[i].GetComponent<EnemyRecolectors>() == true) {
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
        // USLESS NODE
        // USLESS NODE
        // USLESS NODE
        // USLESS NODE
        // USLESS NODE
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
        if (EnemyAI.Instance.GetMoney() >= 40 && 
            EnemyAI.Instance.GetMobileUnits().Count < EnemyAI.Instance.GetCurrentUnitSpaces()) {
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
        VideogameCore.Instance.InitLevelUpBase();
    }
}

public class GatherResourceNode : ActionNode {
    public GatherResourceNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        for (int i = 0; i < EnemyAI.Instance.GetMobileUnits().Count; i++) {
            EnemyRecolectors erScript = EnemyAI.Instance.GetMobileUnits()[i].GetComponent<EnemyRecolectors>();
            if (erScript == true) {
                if (erScript.IsFarming() == false && erScript.IsAttacking() == false) {
                    if (EnemyAI.Instance.GetResourcesAvailable().Count > 0) {
                        erScript.SetState(new GoingToFarmEnemy_State(
                            erScript, 
                            EnemyAI.Instance.GetResourcesAvailable()[0]
                            .GetComponent<StationaryResource>())
                        );
                    }
                }
            }
        }
        // foreach (GameObject unit in EnemyAI.Instance.GetMobileUnits()) {
        //     EnemyRecolectors erScript = unit.GetComponent<EnemyRecolectors>();
        //     if (erScript == true) {
        //         if (erScript.IsFarming() == false) {
        //             if (EnemyAI.Instance.GetResourcesAvailable().Count > 0) {
        //                 erScript.SetState(new GoingToFarmEnemy_State(
        //                     erScript, 
        //                     EnemyAI.Instance.GetResourcesAvailable()[0]
        //                     .GetComponent<StationaryResource>())
        //                 );
        //             }
        //         }
        //     }
        // }
    }
}

public class CheckOffensiveUnitGeneratorNode : ConditionNode {
    public CheckOffensiveUnitGeneratorNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetOffensiveUnitGenerators().Count > 0) {
            return true;
        }
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
        foreach (GameObject unit in EnemyAI.Instance.GetMobileUnits()) {
            EnemyRecolectors erScript = unit.GetComponent<EnemyRecolectors>();
            if (erScript == true) {
                if (erScript.IsFarming() == false && erScript.IsAttacking() == false) {
                    if (EnemyAI.Instance.GetResourcesAvailable().Count > 0) {
                        erScript.SetState(new GoingToFarmEnemy_State(
                            erScript, 
                            EnemyAI.Instance.GetResourcesAvailable()[0]
                            .GetComponent<StationaryResource>())
                        );
                    }
                }
            }
        }
    }
}

public class BuyRecolectionUnitNode : ActionNode {
    public BuyRecolectionUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Buy recolection unit
        EnemyAI.Instance.UseMoney(40);
        VideogameCore.Instance.SpawnRecolector();
    }
}

public class GatherResourcesForRecolectionUnitNode : ActionNode {
    public GatherResourcesForRecolectionUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        foreach (GameObject unit in EnemyAI.Instance.GetMobileUnits()) {
            EnemyRecolectors erScript = unit.GetComponent<EnemyRecolectors>();
            if (erScript == true) {
                if (erScript.IsFarming() == false && erScript.IsAttacking() == false) {
                    if (EnemyAI.Instance.GetResourcesAvailable().Count > 0) {
                        erScript.SetState(new GoingToFarmEnemy_State(
                            erScript, 
                            EnemyAI.Instance.GetResourcesAvailable()[0]
                            .GetComponent<StationaryResource>())
                        );
                    }
                }
            }
        }
    }
}

public class CheckOffensiveAndEnemyUnitsNode : ConditionNode {
    public CheckOffensiveAndEnemyUnitsNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetMobileUnits().Count >= EnemyAI.Instance.GetCurrentAttackers().Count) {
            return true;
        }
        return false;
    }
}

public class CheckResourcesForOffensiveGeneratorUnitNode : ConditionNode {
    public CheckResourcesForOffensiveGeneratorUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetMoney() >= 100) {
            return true;
        }
        return false;
    }
}

public class BuyOffensiveGeneratorUnitNode : ActionNode {
    public BuyOffensiveGeneratorUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Buy offensive generator unit
        EnemyAI.Instance.UseMoney(100);
        EnemyAI.Instance.BuildBugGenerator();
    }
}

public class GatherResourceForOffGenUnitNode : ActionNode {
    public GatherResourceForOffGenUnitNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        for (int i = 0; i < EnemyAI.Instance.GetMobileUnits().Count; i++) {
            EnemyRecolectors erScript = EnemyAI.Instance.GetMobileUnits()[i].GetComponent<EnemyRecolectors>();
            if (erScript == true) {
                if (erScript.IsFarming() == false && erScript.IsAttacking() == false) {
                    if (EnemyAI.Instance.GetResourcesAvailable().Count > 0) {
                        erScript.SetState(new GoingToFarmEnemy_State(
                            erScript, 
                            EnemyAI.Instance.GetResourcesAvailable()[0]
                            .GetComponent<StationaryResource>())
                        );
                    }
                }
            }
        }
    }
}

public class ProtectBaseNode : ActionNode {
    public ProtectBaseNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Make mobile units attack enemy units that are attackin the base
        for (int i = 0; i < EnemyAI.Instance.GetCurrentAttackers().Count; i++) {
            MobileUnit mbScript = EnemyAI.Instance.GetMobileUnits()[i].GetComponent<MobileUnit>();
            if (mbScript == true) {
                mbScript.SetState(
                    new ApproachingEnemy_State(
                        mbScript, 
                        EnemyAI.Instance.GetCurrentAttackers()[i].GetComponent<MobileUnit>()
                    )
                );
            }
        }
    }
}

public class BuyOffensiveUnitsNode : ActionNode {
    public BuyOffensiveUnitsNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        for (int i = 0; i < EnemyAI.Instance.GetCurrentAttackers().Count; i++) {
            EnemyAI.Instance.GetOffensiveUnitGenerators()[i].GetComponent<BugGenerator>().SpawnBug();
        }
    }
}

public class CheckIfWallCanBeBoughtNode : ConditionNode {
    private int counter;
    public CheckIfWallCanBeBoughtNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetMoney() >= 150) {
            return true;
        }
        return false;
    }
}

public class BuyWallNode : ActionNode {
    public BuyWallNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        for (int i = 0; i < EnemyAI.Instance.GetWalls().Count; i++) {
            if (EnemyAI.Instance.GetWalls()[i].activeSelf == false) {
                EnemyAI.Instance.UseMoney(150);
                EnemyAI.Instance.GetWalls()[i].SetActive(true);
                break;
            }
        }
        EnemyAI.Instance.GetWalls();
    }
}

public class CheckNumberOfWalls : ConditionNode {
    public CheckNumberOfWalls(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        int counter = 0;
        for (int i = 0; i < EnemyAI.Instance.GetWalls().Count; i++) {
            if (EnemyAI.Instance.GetWalls()[i].activeSelf == true) {
                counter = counter + 1;
            }
        }
        //Debug.Log(counter);
        if (counter < 21) {
            return true;
        }
        return false;
    }
}