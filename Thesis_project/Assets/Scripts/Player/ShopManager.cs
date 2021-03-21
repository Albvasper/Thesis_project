using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ShopManager : MonoBehaviour {

    // Prices
    private int officeSpacePrice = 50;          //Costs money
    private int internPrice = 10;               //Costs money
    private int studioUpgradePrice = 200;       //Costs money
    private int wallPrice = 20;                 //Costs money
    private int automaticDoorsPrice = 100;       //Costs money
    private int recluterDevPrice = 100;         //Costs money
    private int developerPrice = 50;            //Costs money
    private int recluterDesignerPrice = 100;    //Costs money
    private int designerPrice = 50;             //Costs money
    private int recluterDesignerArtist = 100;   //Costs money
    private int artistPrice = 50;               //Costs money

    // Mobile unit prefabs
    [SerializeField] private Transform unitsSpawnPoint;
    [SerializeField] private GameObject internPrefab;
    [SerializeField] private GameObject developerPrefab;
    [SerializeField] private GameObject designerPrefab;
    [SerializeField] private GameObject artistPrefab;
    // Statioary unit prefabs
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject automaticDoorsPrefab;
    [SerializeField] private GameObject recluterDevPrefab;
    [SerializeField] private GameObject recluterDesignerPrefab;
    [SerializeField] private GameObject recluterArtistPrefab;

    // Other
    private int officeSpaceValue = 10;
    [SerializeField] private List<GameObject> blockedOfficeSpaces = new List<GameObject>();
    [SerializeField] private NavMeshSurface navMeshSurface;

    public void BuyOfficeSpace() {
        // If there are anymore office spaces
        if (blockedOfficeSpaces.Count > 0) {    
            // Check if player can afford office space
            if (Player.Instance.GetMoney() >= officeSpacePrice) {
                // Get money from player
                Player.Instance.UseMoney(officeSpacePrice);
                // Buy next unit space
                blockedOfficeSpaces[blockedOfficeSpaces.Count - 1].SetActive(false);
                blockedOfficeSpaces.RemoveAt(blockedOfficeSpaces.Count - 1);
                Player.Instance.AddUnitSpaces(officeSpaceValue);
                // Update nav mesh
                navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            } else {
                // Not enough money
                Player.Instance.ShowAlertToPlayer("Not enough money!");
            }
        } else {
            Player.Instance.ShowAlertToPlayer("You already own the whole office!");
        }
    }

    // Stationary units
    public void UpgradeStudio() {
        // Check for available unit spaces
        if (Studio.Instance.IsUpgrading() == false) {
            if (Player.Instance.GetMoney() >= studioUpgradePrice) {
                if (Player.Instance.GetBaseLvl() < Player.Instance.GetMaxLevelBase()) {
                    Player.Instance.UseMoney(studioUpgradePrice);
                    Studio.Instance.InitLvlUp();
                } else {
                    Player.Instance.ShowAlertToPlayer("Studio at max level!");
                }
            } else {
                Player.Instance.ShowAlertToPlayer("Not enough money!");
            }
        } else {
            Player.Instance.ShowAlertToPlayer("Studio is being upgraded!");
        }
    }

    public void BuyWall() {
        if (Player.Instance.GetMoney() >= wallPrice) {
            Player.Instance.UseMoney(wallPrice);
            BuildManager.Instance.InitBuilding(wallPrefab);
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough money!");
        }
    }

    public void BuyAutomaticDoors() {
        if (Player.Instance.GetMoney() >= automaticDoorsPrice) {
            Player.Instance.UseMoney(automaticDoorsPrice);
            BuildManager.Instance.InitBuilding(automaticDoorsPrefab);
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough money!");
        }
    }
    
    public void HireRecluterDev() {
        if (Player.Instance.GetMoney() >= recluterDevPrice) {
            Player.Instance.UseMoney(recluterDevPrice);
            BuildManager.Instance.InitBuilding(recluterDevPrefab);
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough money!");
        }
    }

    public void HireRecluterDesigner() {
        if (Player.Instance.GetMoney() >= recluterDesignerPrice) {
            Player.Instance.UseMoney(recluterDesignerPrice);
            BuildManager.Instance.InitBuilding(recluterDesignerPrefab);
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough money!");
        }
    }

    public void HireRecluterArtist() {
        if (Player.Instance.GetMoney() >= recluterDesignerArtist) {
            Player.Instance.UseMoney(recluterDesignerArtist);
            BuildManager.Instance.InitBuilding(recluterArtistPrefab);
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough money!");
        }
    }


    // Mobile units
    public void HireIntern() {
        // Check for available unit spaces
        if (Player.Instance.GetMobileUnits().Count < Player.Instance.GetCurrentUnitSpaces()) {
            if (Player.Instance.GetMoney() >= internPrice) {
                Player.Instance.UseMoney(internPrice);
                Instantiate(internPrefab, unitsSpawnPoint.position, Quaternion.identity);
            } else {
                Player.Instance.ShowAlertToPlayer("Not enough money!");
            }
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough office space!");
        }
    }

    public void HireDeveloper() {
        // Check for available unit spaces
        if (Player.Instance.GetMobileUnits().Count < Player.Instance.GetCurrentUnitSpaces()) {
            if (Player.Instance.GetMoney() >= developerPrice) {
                Player.Instance.UseMoney(developerPrice);
                Instantiate(developerPrefab, unitsSpawnPoint.position, Quaternion.identity);
            } else {
                Player.Instance.ShowAlertToPlayer("Not enough money!");
            }
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough office space!");
        }
    }

    public void HireDesigner() {
        // Check for available unit spaces
        if (Player.Instance.GetMobileUnits().Count < Player.Instance.GetCurrentUnitSpaces()) {
            if (Player.Instance.GetMoney() >= designerPrice) {
                Player.Instance.UseMoney(designerPrice);
                Instantiate(designerPrefab, unitsSpawnPoint.position, Quaternion.identity);
            } else {
                Player.Instance.ShowAlertToPlayer("Not enough money!");
            }
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough office space!");
        }
    }

    public void HireArtist() {
        // Check for available unit spaces
        if (Player.Instance.GetMobileUnits().Count < Player.Instance.GetCurrentUnitSpaces()) {
            if (Player.Instance.GetMoney() >= artistPrice) {
                Player.Instance.UseMoney(artistPrice);
                Instantiate(artistPrefab, unitsSpawnPoint.position, Quaternion.identity);
            } else {
                Player.Instance.ShowAlertToPlayer("Not enough money!");
            }
        } else {
            Player.Instance.ShowAlertToPlayer("Not enough office space!");
        }
    }
}

