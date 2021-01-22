using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int nOfTotalUnits;
    private List<GameObject> recolectionUnits = new List<GameObject>();
    private List<GameObject> selectedUnits = new List<GameObject>();
    private int unitSpaces;
    private int maxUnitSpaces;
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
        unitSpaces = 5;
        maxUnitSpaces = 300;
        InitBehaviorTree();
    }

    private void Update() {
        behaviorTree.Update();
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

    public List<GameObject> GetRecolectionUnits() {
        return recolectionUnits;
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

    public void LevelUpBase() {
        baseLevel =+ 1;
    }

    public int GetTotalUnitSpaces() {
        return unitSpaces;
    }

    public int GetCurrentNumberOfUnits() {
        return nOfTotalUnits;
    }
}

// Node implementations
public class CheckIfUnderAttackNode : ConditionNode {
    public CheckIfUnderAttackNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        // if (base is being attacked) {
        //     return true;
        // }
        return false;
    }
}

public class CheckUnitSpacesNode : ConditionNode {
    public CheckUnitSpacesNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetCurrentNumberOfUnits() >= EnemyAI.Instance.GetTotalUnitSpaces()) {
            return true;
        }
        return false;
    }
}

public class CheckRecolectionUnitsNode : ConditionNode {
    public CheckRecolectionUnitsNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetRecolectionUnits().Count >= 5) {
            return true;
        }
        return false;
    }
}

public class CheckLevelUpBaseNode : ConditionNode {
    public CheckLevelUpBaseNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetLinesOfCode() >= 200 && EnemyAI.Instance.GetMoney() >= 300) {
            return true;
        }
        return false;
    }
}

public class CountEnemyUnitsNode : DecoratorNode {
    public CountEnemyUnitsNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        // Count up how many units are attacking the base
    }
}

public class CheckResourceForHouse : ConditionNode {
    public CheckResourceForHouse(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetMoney() >= 20) {
            return true;
        }
        return false;
    }
}

public class CheckResourcesForRecolectionUnits : ConditionNode {
    public CheckResourcesForRecolectionUnits(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override bool Condition() {
        if (EnemyAI.Instance.GetMoney() >= 40) {
            return true;
        }
        return false;
    }
}

public class LevelUpBaseNode : ActionNode {
    public LevelUpBaseNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        EnemyAI.Instance.LevelUpBase();
    }
}

public class GatherResourceNode : ActionNode {
    public GatherResourceNode(BehaviorTree behaviorTree) : base(behaviorTree) {}
    public override void Action() {
        foreach (GameObject unit in EnemyAI.Instance.GetRecolectionUnits()) {
            // farm resource
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
        // Buy house
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