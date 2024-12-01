using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("BUILDMANAGER INSTANCE IS REPEATED");
            return;
        }
        instance = this;
    }

    public GameObject standardTowerPrefab;

    private void Start()
    {
        
    }

    private TowerBluePrint towerToBuild;

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

    public void BuildTowerOn(Node node)
    {
        //not enough money dont build
        if (PlayerStats.Money < towerToBuild.cost)
        {
            Debug.Log("not enough money" + PlayerStats.Money);
            return;
        }

        PlayerStats.Money -= towerToBuild.cost;
        GameObject tower = Instantiate(towerToBuild.prefab, node.transform.position, node.transform.rotation);
        node.tower = tower;
    }

    public void SetTowerToBuild(TowerBluePrint tower)
    {
        towerToBuild = tower;
    }

    public void SetSpellToActivate(TowerBluePrint tower)
    {
        towerToBuild = tower;
    }
}
