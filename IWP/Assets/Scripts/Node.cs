using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColour;

    [HideInInspector]
    public GameObject tower;    
    [HideInInspector]
    public TowerBluePrint towerBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer r;
    private Color startColor;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        startColor = r.material.color;

        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //check if hovering over untiy UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }



        if (tower != null)
        {
            buildManager.selectNode(this);

            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTower(buildManager.GetTowerToBuild());
    }

    void BuildTower(TowerBluePrint blueprint)
    {
        //not enough money dont build
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("not enough money" + PlayerStats.Money);
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        GameObject t = Instantiate(blueprint.prefab, transform.position, transform.rotation);
        tower = t;

        if (buildManager.PlayEffect)
        {
            GameObject effect = (GameObject)Instantiate(buildManager.Effect, transform.position, transform.rotation);
            Destroy(effect, 4f);
        }

        towerBluePrint = blueprint;
        isUpgraded = false;
    }

    public void UpgradeTower()
    {
        //not enough money dont build
        if (PlayerStats.Money < towerBluePrint.upgradeCost)
        {
            Debug.Log("not enough money to upgrade");
            return;
        }

        PlayerStats.Money -= towerBluePrint.upgradeCost;
        Destroy(tower);

        GameObject t = Instantiate(towerBluePrint.upgradedTower, transform.position, transform.rotation);
        tower = t;

        if (buildManager.PlayEffect)
        {
            GameObject effect = (GameObject)Instantiate(buildManager.Effect, transform.position, transform.rotation);
            Destroy(effect, 4f);
        }

        isUpgraded = true;
    }

    public void SellTower()
    {
        PlayerStats.Money += towerBluePrint.GetSellAmount();

        if (buildManager.PlayEffect)
        {
            GameObject effect = (GameObject)Instantiate(buildManager.Effect, transform.position, transform.rotation);
            Destroy(effect, 4f);
        }

        Destroy(tower);
        towerBluePrint = null;
    }

    void OnMouseEnter()
    {
        //check if hovering over untiy UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            r.material.color = hoverColour;
        }
        else
        {
            r.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        r.material.color = startColor;
    }
} 
