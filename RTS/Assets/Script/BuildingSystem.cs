using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour
{
    public GameObject building1;
    public GameObject building2;
    GameObject selectedBuilding;
    int listNumber;
    bool gotIt;
    // Start is called before the first frame update
    void Start()
    {
        selectedBuilding = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                CurrentClickedGameObject(raycastHit.transform.gameObject);
            }
        }


    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.tag == "Build area")
        {
            //For checking
            /*
            Debug.Log(gameObject.name);
            Renderer rend = gameObject.GetComponent<Renderer>();
            rend.material.color = Color.red;
            */
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (selectedBuilding != null)
            {
               if(selectedBuilding == building1)
               {
                    if(CheckBuildCondition(1, gameObject) == true)
                    {
                        gameObject.GetComponent<LandProperty>().SetIsBuild(1, true);
                        Instantiate(selectedBuilding, gameObject.transform.position, Quaternion.identity);
                        selectedBuilding = null;
                    }
                    
               }
               else if (selectedBuilding == building2)
               {     
                    if(CheckBuildCondition(2, gameObject) == true)
                    {
                        gameObject.GetComponent<LandProperty>().SetIsBuild(2, true);                     
                        Instantiate(selectedBuilding, gameObject.transform.position, Quaternion.identity);
                        selectedBuilding = null;
                    }
                  
               }
               
            }
            SetBuildingSelection(0);
        }
        else
        {
            Debug.Log("not hit");
        }
    }

    public void SelectBuilding1()
    {
        selectedBuilding = building1;
        SetBuildingSelection(1);
    }

    public void SelectBuilding2()
    {
        selectedBuilding = building2;
        SetBuildingSelection(2);
    }

    public void CheckCanBuild(GameObject gameObject)
    {
        if (selectedBuilding == building2)
        {

        }
    }

    void SetBuildingSelection(int buildingIndex)
    {
        for (int i = 0; i < this.GetComponent<GameManager>().listOfLand.Count; i++)
        {
            GameObject land = this.GetComponent<GameManager>().listOfLand[i];
            land.GetComponent<LandProperty>().towerSelection = buildingIndex;
        }
        
    }

    bool CheckBuildCondition(int buildingType, GameObject land)
    {
        if (buildingType == 1)
        {
            if (land.GetComponent<LandProperty>().getIsbuild() == false)
            {
                return true;
            }
        }

        else if (buildingType == 2)
        {
            if (land.GetComponent<LandProperty>().getIsbuild() == false)
            {
                if (land.GetComponent<LandProperty>().cornelDown == false && land.GetComponent<LandProperty>().cornelLeft == false
                    && land.GetComponent<LandProperty>().cornelRight == false && land.GetComponent<LandProperty>().cornelUp == false)
                {
                    if (land.GetComponent<LandProperty>().neighbour1.GetComponent<LandProperty>().getIsbuild() == false && land.GetComponent<LandProperty>().neighbour2.GetComponent<LandProperty>().getIsbuild() == false
                        && land.GetComponent<LandProperty>().neighbour3.GetComponent<LandProperty>().getIsbuild() == false && land.GetComponent<LandProperty>().neighbour4.GetComponent<LandProperty>().getIsbuild() == false
                        && land.GetComponent<LandProperty>().neighbour5.GetComponent<LandProperty>().getIsbuild() == false && land.GetComponent<LandProperty>().neighbour6.GetComponent<LandProperty>().getIsbuild() == false
                        && land.GetComponent<LandProperty>().neighbour7.GetComponent<LandProperty>().getIsbuild() == false && land.GetComponent<LandProperty>().neighbour8.GetComponent<LandProperty>().getIsbuild() == false)
                    {
                        return true;
                    }

                }
            }
        }

        return false;

        
    }
}
