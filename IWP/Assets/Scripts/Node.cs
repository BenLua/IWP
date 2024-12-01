using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColour;

    public GameObject tower;

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

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (tower != null)
        {
            Debug.Log("tower already exists on this node");

            return;        
        }

        buildManager.BuildTowerOn(this);
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
