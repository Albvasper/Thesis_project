/*
=============================================================================
 *  Description: Class that manages the players resources, units and actions.
=============================================================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    
    #region Singleton Pattern
        private static Player instance;
        public static Player Instance { 
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
    
    private Camera mainCam;
    private int baseLevel;
    private int maxBaseLevel;
    // Player resources
    private int money;
    private int assets;
    private int linesOfCode;
    // Player units
    private List<GameObject> mobileUnits = new List<GameObject>();
    private List<GameObject> selectedUnits = new List<GameObject>();
    private List<GameObject> idleUnits = new List<GameObject>();
    private int unitSpaces;
    private int maxUnitSpaces;
    // HUD / GUI
    [SerializeField] private Text linesOfCodeText;
    [SerializeField] private Text assetsText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text idleUnitsText;
    [SerializeField] private Text nOfUnitSpaces;
    [SerializeField] private GameObject MsgToPlayer;
    [SerializeField] private Text msgToPlayerText;
    [SerializeField] private Text baseLevelText;
    [SerializeField] private Text fpsCounter;

    private int msgShowTime;

    private void Start() {
        mainCam = Camera.main;
        baseLevel = 1;
        maxBaseLevel = 3;
        money = 10000000;
        assets = 0;
        linesOfCode = 0;
        unitSpaces = 5;
        maxUnitSpaces = 300;
        msgShowTime = 3;
    }

    private void Update() {
        //BoundSpaces();
        UpdateHudValues();
    }
    
    // private void BoundSpaces () {
    //     if (unitSpaces < 0) {
    //         unitSpaces = 0;
    //     }
    //     if (unitSpaces > maxUnitSpaces) {
    //         unitSpaces = maxUnitSpaces;
    //     }
    // }
    
    private void UpdateHudValues() {
        baseLevelText.text = "Studio level: " + baseLevel.ToString();
        linesOfCodeText.text = linesOfCode.ToString();
        assetsText.text = assets.ToString();
        moneyText.text = money.ToString();
        idleUnitsText.text = idleUnits.Count.ToString();
        nOfUnitSpaces.text =  mobileUnits.Count.ToString() + "/" + unitSpaces.ToString();
        float fps = 1 / Time.unscaledDeltaTime;
        fpsCounter.text = "FPS: " + fps;
    }

    public void AddBaseLvl() {
        baseLevel += 1;
    }

    public int GetBaseLvl() {
        return baseLevel;
    }

    public void AddMoney(int amount) {
        money += amount;
    }

    public void UseMoney(int amount) {
        money -= amount;
    }

    public void AddLinesOfCode(int amount) {
        linesOfCode += amount;
    }

    public void AddAssets(int amount) {
        assets += amount;
    }

    public void AddUnitSpaces(int amount) {
        unitSpaces += amount;
    }

    public List<GameObject> GetSelectedUnits() {
        return selectedUnits;
    }

    public List<GameObject> GetMobileUnits() {
        return mobileUnits;
    }

    public void AddMobileUnit(GameObject unit) {
        mobileUnits.Add(unit);
    }

    public List<GameObject> GetIdleUnits() {
        return idleUnits;
    }

    public int GetMoney() {
        return money;
    }

    public int GetLinesOfCode() {
        return linesOfCode;
    }

    public int GetAssets() {
        return assets;
    }

    public int GetMaxUnitSpaces() {
        return maxUnitSpaces;
    }

    public int GetCurrentUnitSpaces() {
        return unitSpaces;
    }

    public int GetMaxLevelBase() {
        return maxBaseLevel;
    }
    
    public void ShowAlertToPlayer(string msg) {
        msgToPlayerText.text = msg;
        MsgToPlayer.SetActive(true);
        StartCoroutine(DisableMsgAlert());
    }

    IEnumerator DisableMsgAlert() {
        yield return new WaitForSeconds(msgShowTime);
        MsgToPlayer.SetActive(false);
        msgToPlayerText.text = "";
    }
}
