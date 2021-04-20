using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideogameCore : StationaryUnit {

    #region Singleton pattern
        private static VideogameCore instance;
        public static VideogameCore Instance {
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
    [SerializeField] private GameObject recolectorPrefab;
    [SerializeField] private Transform spawnPoint;
    private int lvlUpTime;
    private int cLvlProgress;   // current level up progress
    private float time;
    private int delay;
    private bool lvlUpStudio;

    protected override void Start() {
        maxHP = 5000;
        attackDamage = 0;
        base.Start();
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
        LevelUpBase();
    }

    protected override void Die() {
        Debug.Log("Win!");
        //SceneManager.LoadScene("Ending_4");
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
                EnemyAI.Instance.AddBaseLvl();
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

    public void ReceiveResource(EnemyRecolectors enemyRecolector) {
        if (enemyRecolector.GetResourceType() == "MONEY") {
            EnemyAI.Instance.AddMoney(enemyRecolector.GiveResource());
        } else if (enemyRecolector.GetResourceType() == "LINEOFCODE") {
            EnemyAI.Instance.AddLinesOfCode(enemyRecolector.GiveResource());
        } else {
            EnemyAI.Instance.AddAssets(enemyRecolector.GiveResource());
        }
    }

    public void SpawnRecolector() {
        Instantiate(recolectorPrefab, spawnPoint.position, Quaternion.identity);
    }
}