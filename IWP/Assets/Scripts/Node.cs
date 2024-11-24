using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColour;

    private GameObject tower;

    private Renderer r;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        startColor = r.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (tower != null)
        {
            Debug.Log("tower already exists on this node");

            return;        
        }

        //Build tower
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
        tower = (GameObject)Instantiate(towerToBuild, transform.position, transform.rotation);
    }

    void OnMouseEnter()
    {
        r.material.color = hoverColour;
    }

    private void OnMouseExit()
    {
        r.material.color = startColor;
    }
} 
