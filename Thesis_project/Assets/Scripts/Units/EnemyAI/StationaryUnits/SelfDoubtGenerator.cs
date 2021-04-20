using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDoubtGenerator : StationaryUnit {

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject selfDoubtPrefab;

    protected override void Start() {
        maxHP = 200;
        attackDamage = 0;
        base.Start();
        aiUnit = true;
    }
 
    protected override void Update() {
        base.Update();
    }

    public void SpawnSelfDoubt() {
        Instantiate(selfDoubtPrefab, spawnPoint.position, Quaternion.identity);
    }
}
