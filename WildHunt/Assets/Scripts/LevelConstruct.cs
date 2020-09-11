using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConstruct : MonoBehaviour
{

    public GameObject finish;
    public GameObject randomEnd;
    public static bool destoryIt = false;

    private GameObject[] horizontalTileset;
    private GameObject[] verticalTileset;
    private Vector2 tempV2;
    private int randomTileset;
    private GameObject tempGO;
    private bool directionSpawn = false;
    private bool firstVCheck = true;
   
    // Use this for initialization
    void Start()
    {
        //Instantiate arrays with prefabs
        horizontalTileset = Resources.LoadAll<GameObject>("HorizontalTiles");
        verticalTileset = Resources.LoadAll<GameObject>("VerticalTiles");
        tempV2 = this.transform.position;
        //Generates random modular level
        for (int i = 10; i > 0; i--)
            ModularGeneration();

        //calls the finish correction method
        FinishCorrection();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ModularGeneration()
    {
        //Switch statement determines direction of next module 
        switch (directionSpawn)
        {
            //If true vertical,false horizontal
            case true:
                //random tileset

                randomTileset = Random.Range(0, verticalTileset.Length);

                //if the final it switches direction
                if (verticalTileset[randomTileset].name == "VTileGrid3")
                {
                    directionSpawn = false;
                }

                //If first check it had different parameters 
                if (firstVCheck == true)
                {
                    //creates object and does some organization
                    tempGO = Instantiate(verticalTileset[randomTileset], new Vector2(tempV2.x + 4, tempV2.y + 11f), Quaternion.identity);
                    tempGO.transform.parent = this.transform;
                    tempV2 = tempGO.transform.position;
                    firstVCheck = false;
                    destoryIt = true;
                    break;
                }
                else
                {

                    //creates object and does some organization
                    tempGO = Instantiate(verticalTileset[randomTileset], new Vector2(tempV2.x, tempV2.y + 17f), Quaternion.identity);
                    tempGO.transform.parent = this.transform;
                    tempV2 = tempGO.transform.position;
                    if (verticalTileset[randomTileset].name == "VTileGrid1" || verticalTileset[randomTileset].name == "VTileGrid2")
                    {

                        pathVariation(verticalTileset[randomTileset].name, tempV2, tempGO);
                        Destroy(tempGO.transform.GetChild(2).gameObject);
                    }
                    break;
                }

            case false:
                //random tileset
                destoryIt = false;
                randomTileset = Random.Range(0, horizontalTileset.Length);
                //Checks for final tileset to change direction
                if (horizontalTileset[randomTileset].name == "TileGrid6")
                {
                    directionSpawn = true;

                }


                if (firstVCheck == false)
                {
                    //creates object and does some organization
                    tempGO = Instantiate(horizontalTileset[randomTileset], new Vector2(tempV2.x + 23f, tempV2.y + 11f), Quaternion.identity);
                    tempGO.transform.parent = this.transform;
                    tempV2 = tempGO.transform.position;
                    firstVCheck = true;
                    break;
                }
                else
                {
                    //creates object and does some organization
                    tempGO = Instantiate(horizontalTileset[randomTileset], new Vector2(tempV2.x + 25f, tempV2.y), Quaternion.identity);
                    tempGO.transform.parent = this.transform;
                    tempV2 = tempGO.transform.position;
                    break;
                }

        }
    }

    void FinishCorrection()
    {
        //Corrects the finish to be applied correctly regardless of the finish line
        if (directionSpawn == false && firstVCheck == false)
        {
            tempGO = Instantiate(finish, new Vector2(tempV2.x + 20f, tempV2.y + 11f), Quaternion.identity);
            tempGO.transform.parent = this.transform;
            tempV2 = tempGO.transform.position;
        }
        else if (directionSpawn == true && firstVCheck == false)
        {
            //Ends on a vertical
            tempGO = Instantiate(verticalTileset[verticalTileset.Length - 1], new Vector2(tempV2.x, tempV2.y + 17f), Quaternion.identity);
            tempGO.transform.parent = this.transform;
            tempV2 = tempGO.transform.position;

            tempGO = Instantiate(finish, new Vector2(tempV2.x + 20f, tempV2.y + 11f), Quaternion.identity);
            tempGO.transform.parent = this.transform;
            tempV2 = tempGO.transform.position;
        }
        else if (directionSpawn == true && firstVCheck == true)
        {
            //Ends on a upwards horizontal

            tempGO = Instantiate(verticalTileset[verticalTileset.Length - 1], new Vector2(tempV2.x + 4, tempV2.y + 11f), Quaternion.identity);
            tempGO.transform.parent = this.transform;
            tempV2 = tempGO.transform.position;

            tempGO = Instantiate(finish, new Vector2(tempV2.x + 20f, tempV2.y + 11f), Quaternion.identity);
            tempGO.transform.parent = this.transform;
            tempV2 = tempGO.transform.position;
        }
        else
        {
            //Ends on a horizontal

            tempGO = Instantiate(finish, new Vector2(tempV2.x + 20f, tempV2.y), Quaternion.identity);
            tempGO.transform.parent = this.transform;
            tempV2 = tempGO.transform.position;
        }
    }

    /*Creates a path offshoot of the main level. This is dependant on the Tile Grid give. 
     */
    void pathVariation(string s, Vector2 v, GameObject g)
    {
        int randLevels = Random.Range(2, 4);
        int num = 11;
        switch (s)
        {
            //A right path if the tile is VTileGrid1
            case "VTileGrid1":

                for (int i = 0; i < randLevels; i++)
                {
                    //Instantiates the levels on a loop
                    int randInt = Random.Range(0, horizontalTileset.Length - 2);
                    g = Instantiate(horizontalTileset[randInt], new Vector2(v.x - 24f, v.y), Quaternion.identity);
                    g.transform.parent = this.transform;
                    v = g.transform.position;

                }
                g = Instantiate(randomEnd, new Vector2(v.x - 23f, v.y), Quaternion.identity);
                g.transform.parent = this.transform;

                break;
            //A right path if the tile is VTileGrid2
            case "VTileGrid2":
                for (int i = 0; i < randLevels; i++)
                {
                    //Instantiates the objects in a loop
                    int randInt = Random.Range(0, horizontalTileset.Length - 2);
                    g = Instantiate(horizontalTileset[randInt], new Vector2(v.x + 23f, v.y + num), Quaternion.identity);
                    g.transform.parent = this.transform;
                    v = g.transform.position;
                    num = 0;
                }
                g = Instantiate(randomEnd, new Vector2(v.x + 23f, v.y), Quaternion.identity);
                g.transform.localScale = new Vector3(-1, 1, 0);
                g.transform.parent = this.transform;
                break;
        }
    }
}
