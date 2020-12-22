using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LandProperty : MonoBehaviour
{
    public bool isBuild;
    Renderer rend;

    public GameObject neighbour1;
    public GameObject neighbour2;
    public GameObject neighbour3;
    public GameObject neighbour4;
    public GameObject neighbour5;
    public GameObject neighbour6;
    public GameObject neighbour7;
    public GameObject neighbour8;

    public bool cornelLeft = false;
    public bool cornelRight = false;
    public bool cornelUp = false;
    public bool cornelDown = false;

    public int towerSelection = 0;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (towerSelection == 0)
        {
            rend.material.color = Color.white;          
        }
    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (towerSelection == 0)
            return;
        
           
        if(towerSelection == 1)
        {
            rend.material.color = Color.red;
        }
       


        if (towerSelection == 2)
        {           
            if (cornelDown == false && cornelLeft == false
                && cornelUp == false && cornelRight == false)
            {
                rend.material.color = Color.red;
                neighbour1.GetComponent<Renderer>().material.color = Color.red;
                neighbour2.GetComponent<Renderer>().material.color = Color.red;
                neighbour3.GetComponent<Renderer>().material.color = Color.red;
                neighbour4.GetComponent<Renderer>().material.color = Color.red;
                neighbour5.GetComponent<Renderer>().material.color = Color.red;
                neighbour6.GetComponent<Renderer>().material.color = Color.red;
                neighbour7.GetComponent<Renderer>().material.color = Color.red;
                neighbour8.GetComponent<Renderer>().material.color = Color.red;
            }
        }


    }

    private void OnMouseExit()
    {
        
        rend.material.color = Color.white;


        if (towerSelection == 2)
        {           
            if (cornelDown == false && cornelLeft == false
                && cornelUp == false && cornelRight == false)
            {
                neighbour1.GetComponent<Renderer>().material.color = Color.white;
                neighbour2.GetComponent<Renderer>().material.color = Color.white;
                neighbour3.GetComponent<Renderer>().material.color = Color.white;
                neighbour4.GetComponent<Renderer>().material.color = Color.white;
                neighbour5.GetComponent<Renderer>().material.color = Color.white;
                neighbour6.GetComponent<Renderer>().material.color = Color.white;
                neighbour7.GetComponent<Renderer>().material.color = Color.white;
                neighbour8.GetComponent<Renderer>().material.color = Color.white;
            }
        }

    }

    public void setNeightbour(GameObject neightbour, int index)
    {
        if (index == 1)
        {
            neighbour1 = neightbour;
        }
        else if (index == 2)
        {
            neighbour2 = neightbour;
        }
        else if (index == 3)
        {
            neighbour3 = neightbour;
        }
        else if (index == 4)
        {
            neighbour4 = neightbour;
        }
        else if (index == 5)
        {
            neighbour5 = neightbour;
        }
        else if (index == 6)
        {
            neighbour6 = neightbour;
        }
        else if (index == 7)
        {
            neighbour7 = neightbour;
        }
        else if (index == 8)
        {
            neighbour8 = neightbour;
        }
    }

    public void SetIsBuild(int buildingType, bool IsBuild)
    {
        if(buildingType == 1)
        {
            isBuild = IsBuild;
        }
        else if(buildingType == 2)
        {
            isBuild = IsBuild;

            neighbour1.GetComponent<LandProperty>().isBuild = IsBuild;
            neighbour2.GetComponent<LandProperty>().isBuild = IsBuild;
            neighbour3.GetComponent<LandProperty>().isBuild = IsBuild;
            neighbour4.GetComponent<LandProperty>().isBuild = IsBuild;
            neighbour5.GetComponent<LandProperty>().isBuild = IsBuild;
            neighbour6.GetComponent<LandProperty>().isBuild = IsBuild;
            neighbour7.GetComponent<LandProperty>().isBuild = IsBuild;
            neighbour8.GetComponent<LandProperty>().isBuild = IsBuild;
        }
    }

    public bool getIsbuild()
    {
        return isBuild;
    }

}
