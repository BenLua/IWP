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
    private Node selectedNode;

    public GameObject Effect;

    public Node_UI nodeUI;

    [HideInInspector]
    public bool PlayEffect = true;

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

    public void selectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;

        nodeUI.Hide();
    }

    public void SetTowerToBuild(TowerBluePrint tower)
    {
        towerToBuild = tower;
        selectedNode = null;
        PlayEffect = true;

        nodeUI.Hide();
    }

    public void SetSpellToActivate(TowerBluePrint tower)
    {
        towerToBuild = tower;
        PlayEffect = false;
    }

    public TowerBluePrint GetTowerToBuild()
    {
        return towerToBuild;
    }
}
