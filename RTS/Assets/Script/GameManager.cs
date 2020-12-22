using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject buildLand;
    public GameObject tree;

    public Transform lands;
    public Transform trees;

    public int col;
    public int row;
    public int numberOfTree;

    public List<GameObject> listOfLand;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < col; i++)
        {
            for (int y = 0; y < row; y++)
            {
                GameObject land = Object.Instantiate(buildLand, new Vector3(i*2, -20.0f, y*2), Quaternion.identity, lands);              
                land.name = "Build land (" + i + " , " + y + ")";
                if (i + 1 % (col - (col - 1)) == 0)
                {
                    land.GetComponent<LandProperty>().cornelUp = true;
                }
                if ((i + 1) % col == 0)
                {
                    land.GetComponent<LandProperty>().cornelDown = true;
                }
                if (y + 1 % (row - (row - 1)) == 0)
                {
                    land.GetComponent<LandProperty>().cornelLeft = true;
                }
                if ((y + 1) % row == 0)
                {
                    land.GetComponent<LandProperty>().cornelRight = true;
                }

                listOfLand.Add(land);
            }
        }

        for (int i = 0; i < listOfLand.Count; i++)
        {
            // Not at cornel Left or Cornel Right
            ///////////////////////////////////////////////////////////////////////////////////////////
            if (listOfLand[i].GetComponent<LandProperty>().cornelLeft == true
                && !listOfLand[i].GetComponent<LandProperty>().cornelUp == true
                && !listOfLand[i].GetComponent<LandProperty>().cornelDown == true)
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - col], 2);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col - 1)], 3);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + 1], 5);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col)], 7);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col + 1)], 8);
            }

            else if (listOfLand[i].GetComponent<LandProperty>().cornelRight == true
                && !listOfLand[i].GetComponent<LandProperty>().cornelUp == true
                && !listOfLand[i].GetComponent<LandProperty>().cornelDown == true)
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col + 1)], 1);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col)], 2);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - 1], 4);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col - 1)], 6);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col)], 7);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////

            //Cornel Up
            ///////////////////////////////////////////////////////////////////////////////////////////
            else if (listOfLand[i].GetComponent<LandProperty>().cornelLeft == true
                && listOfLand[i].GetComponent<LandProperty>().cornelUp == true)
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + 1], 5);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col)], 7);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col + 1)], 8);
            }



            else if (listOfLand[i].GetComponent<LandProperty>().cornelRight == true
               && listOfLand[i].GetComponent<LandProperty>().cornelUp == true)
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - 1], 4);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col - 1)], 6);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col)], 7);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////


            //Cornel Down
            ///////////////////////////////////////////////////////////////////////////////////////////
            else if (listOfLand[i].GetComponent<LandProperty>().cornelLeft == true
                && listOfLand[i].GetComponent<LandProperty>().cornelDown == true)
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - col], 2);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col - 1)], 3);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + 1], 5);
            }

            else if (listOfLand[i].GetComponent<LandProperty>().cornelRight == true
               && listOfLand[i].GetComponent<LandProperty>().cornelDown == true)
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col + 1)], 1);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col)], 2);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - 1], 4);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////
            //Not Cornel Left or Cornel Right
            else if (listOfLand[i].GetComponent<LandProperty>().cornelRight == false
               && listOfLand[i].GetComponent<LandProperty>().cornelUp == true
               && listOfLand[i].GetComponent<LandProperty>().cornelLeft == false
               && listOfLand[i].GetComponent<LandProperty>().cornelDown == false)
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - 1], 4);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + 1], 5);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col - 1)], 6);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col)], 7);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col + 1)], 8);
            }

            else if (listOfLand[i].GetComponent<LandProperty>().cornelRight == false
               && listOfLand[i].GetComponent<LandProperty>().cornelUp == false
               && listOfLand[i].GetComponent<LandProperty>().cornelLeft == false
               && listOfLand[i].GetComponent<LandProperty>().cornelDown == true)
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col + 1)], 1);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - col], 2);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col - 1)], 3);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - 1], 4);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + 1], 5);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////
            // Not Cornel
            else
            {
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col + 1)], 1);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - col], 2);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - (col - 1)], 3);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i - 1], 4);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + 1], 5);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col - 1)], 6);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col)], 7);
                listOfLand[i].GetComponent<LandProperty>().setNeightbour(listOfLand[i + (col + 1)], 8);
            }

        }

        for(int i = 0; i < numberOfTree; i++)
        {
            int randomNumber = Random.Range(0, listOfLand.Count);
            GameObject Tree = Object.Instantiate(tree, new Vector3(listOfLand[randomNumber].transform.position.x, -20.0f, listOfLand[randomNumber].transform.position.z), Quaternion.identity, trees);
            listOfLand[randomNumber].GetComponent<LandProperty>().SetIsBuild(1, true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
