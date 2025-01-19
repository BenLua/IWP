using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBluePrint archerTower;
    public TowerBluePrint bombTower;
    public TowerBluePrint boostSpell;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectArcherTower()
    {
        buildManager.SetTowerToBuild(archerTower);
    }

    public void SelectBombTower()
    {
        buildManager.SetTowerToBuild(bombTower);
    }

    public void SelectBoostSpell()
    {
        buildManager.SetSpellToActivate(boostSpell);
    }
}
