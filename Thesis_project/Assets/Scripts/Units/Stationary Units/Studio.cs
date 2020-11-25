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
        CheckHP();
        LevelUpStudio();
    }

    protected override void CheckHP() { 
        healthBar.value = currentHP;
        if (currentHP <= 0) {
            Die();
        }
    }

    protected override void Die() {
        Debug.Log("Died!");
        //SceneManager.LoadScene("Ending_4");
    }

    public void GenerateDev() {
        // Spawn neutral dev
        // Condition logic (if the player has enough resources)

    }

    public void InitLvlUp() {
        // Condition logic (if the player has enough resources, if its alread at max lvl, etc.)
        lvlUpStudio = true;
    }

    private void LevelUpStudio() {
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

    public void ReceiveResource(Developer dev) {
        if (dev.GetResourceType() == "MONEY") {
            Player.Instance.AddMoney(dev.GiveResource());
        } else if (dev.GetResourceType() == "LINEOFCODE") {
            Player.Instance.AddLinesOfCode(dev.GiveResource());
        } else {
            Player.Instance.AddAssets(dev.GiveResource());
        }
    }
}
