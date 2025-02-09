using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Node_UI : MonoBehaviour
{
    public GameObject ui;

    public TMP_Text upgradeCost;
    public Button upgradeButton;    
    
    public TMP_Text sellCost;
    public Button sellButton;

    private Node target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Node node)
    {
        target = node;

        transform.position = target.transform.position;
        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.towerBluePrint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellCost.text = "$" + target.towerBluePrint.GetSellAmount();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }    
    
    public void Sell()
    {
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }
}
