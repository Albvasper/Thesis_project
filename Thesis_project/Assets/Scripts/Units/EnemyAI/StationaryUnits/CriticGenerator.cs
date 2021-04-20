using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticGenerator : StationaryUnit {

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject criticPrefab;

    protected override void Start() {
        maxHP = 200;
        attackDamage = 0;
        base.Start();
        aiUnit = true;
    }
 
    protected override void Update() {
        base.Update();
    }

    public void SpawnCritic() {
        Instantiate(criticPrefab, spawnPoint.position, Quaternion.identity);
    }
}
