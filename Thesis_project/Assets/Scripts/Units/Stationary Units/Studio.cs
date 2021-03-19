/*
=============================================================================
 *  Description: The "Studio" is the building that represents the core of 
 *  the players base. If it gets destroyed, the player will get the fourth 
 *  ending.
=============================================================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Studio : StationaryUnit {
    
    #region Singleton pattern
    private static Studio instance;
    public static Studio Instance {
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
    
    public Slider lvlUpBar;
    private int lvlUpTime;
    private int cLvlProgress;   // current level up progress
    private float time;
    private int delay;
    private bool lvlUpStudio;

    protected override void Start() {
        base.Start();
        maxHP = 1000;
        currentHP = maxHP;
        lvlUpTime = 30;
        lvlUpBar.maxValue = lvlUpTime;
        lvlUpBar.gameObject.SetActive(false);
        delay = 1;
        cLvlProgress = 0;
        lvlUpStudio = false;
        time = 0;
    }

    protected override void Update() {
        base.Update(); 
        LevelUpStudio();
    }

    protected override void Die() {
        Debug.Log("Died!");
        //SceneManager.LoadScene("Ending_4");
    }

    public void InitLvlUp() {
        // Condition logic (if the player has enough resources, if its alread at max lvl, etc.)
        lvlUpStudio = true;
    }

    public void LevelUpStudio() {
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
        time = 0;
    }

    public bool IsUpgrading() {
        return lvlUpStudio;    
    }

    public void ReceiveResource(Intern intern) {
        if (intern.GetResourceType() == "MONEY") {
            Player.Instance.AddMoney(intern.GiveResource());
        } else if (intern.GetResourceType() == "LINEOFCODE") {
            Player.Instance.AddLinesOfCode(intern.GiveResource());
        } else {
            Player.Instance.AddAssets(intern.GiveResource());
        }
    }
}
