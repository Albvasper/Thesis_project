using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugGenerator : StationaryUnit {

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bugPrefab;

    protected override void Start() {
        maxHP = 200;
        attackDamage = 0;
        base.Start();
        aiUnit = true;
    }
 
    protected override void Update() {
        base.Update();
    }

    public void SpawnBug() {
        Instantiate(bugPrefab, spawnPoint.position, Quaternion.identity);
    }
}