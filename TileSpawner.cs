using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{

    public ContainerSide Left;
    public ContainerSide Right;
    public GameObject TileDcha;
    public GameObject TileVacioDcha;
    public GameObject TileIzqda;
    public GameObject TileVacioIzqda;

    private List<int> positions;
    public List<GameObject> currentTiles;

    private int level;

    // Use this for initialization
    void Start()
    {
        //int j = 0;
        currentTiles = new List<GameObject>();
        positions = new List<int>();
        GetPositions(0, false);
        SpawnTiles();
/*        for (int i = 0; i < 10; i++){
            Left.tiles[j].tileMoneda.GetComponent<Renderer>().enabled = true;
            Left.tiles[j].tileMoneda.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        }
        for (int i = 10; i < positions.Count; i++){
            Right.tiles[j].tileMoneda.GetComponent<Renderer>().enabled = true;
            Right.tiles[j].tileMoneda.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        }*/
    }

    public void GetPositions(int level, bool proceduralLevels)
    {
        if (!proceduralLevels){
            positions = FileReader.instance.ReadTheFile(level); 
        }  
        else if (Random.Range(0f, 1f) < 0.5f){
            Debug.Log("Generamos ALEATORIO" + level);
            positions = FileReader.instance.GetNewLevel(level);
        }else{
            Debug.Log("Generamos BINARIO");
            positions = FileReader.instance.GetNewBinaryLevel();
        }

    }

    public void SpawnTiles()
    {
        int j = 0;
        GameObject tile;
        GameObject moneda;
        Renderer tileRend, tileMonedaRend, monedaRend;
        Collider tileCol, monedaCol;
        for (int i = 0; i < 10; i++)
        {
            tileMonedaRend = Left.tiles[j].tileMoneda.GetComponent<Renderer>();

            tileRend = Left.tiles[j].tileVacio.GetComponent<Renderer>();
            tileCol = Left.tiles[j].tileVacio.GetComponent<Collider>();

            monedaRend = Left.tiles[j].tileMoneda.transform.GetChild(0).gameObject.GetComponent<Renderer>();
            monedaCol = Left.tiles[j].tileMoneda.transform.GetChild(0).gameObject.GetComponent<Collider>();


            if (GameManager.instance.invincible)
            {
                //tile = Left.tiles[j].tileMoneda;
                if(!tileMonedaRend.enabled){
                    tileRend.enabled = false;
                    tileCol.enabled = false;
                    tileMonedaRend.enabled = true;
                }
                //moneda = tile.transform.GetChild(0).gameObject;
                tileMonedaRend.enabled = true;
                int spawnCoin = Random.Range(0, 5);
                if (spawnCoin < 3)
                {
                    if(monedaRend.enabled){
                        monedaRend.enabled = false;
                        monedaCol.enabled = false;
                    }

                }
                else
                {
                    if(!monedaRend.enabled){
                        monedaRend.enabled = true;
                        monedaCol.enabled = true;
                    }

                }
                currentTiles.Add(Left.tiles[j].tileMoneda);

            }
            else
            {
                if (positions[i] == 1)
                {

                    //tile = Left.tiles[j].tileMoneda;
                    //moneda = tile.transform.GetChild(0).gameObject;
                    if(!tileMonedaRend.enabled){
                        tileRend.enabled = false;
                        tileCol.enabled = false;
                        tileMonedaRend.enabled = true;
                    }
                    
                    int spawnCoin = Random.Range(0, 5);
                    if (spawnCoin < 4)
                    {
                        if(monedaRend.enabled){
                            monedaRend.enabled = false;
                            monedaCol.enabled = false;
                        }
                    }
                    else
                    {
                        if(!monedaRend.enabled){
                            monedaRend.enabled = true;
                            monedaCol.enabled = true;
                        }
                    }

                    currentTiles.Add(Left.tiles[j].tileMoneda);
                }
                else if (positions[i] == 0)
                {
                    //tile = Left.tiles[j].tileVacio;
                    if(!tileRend.enabled){
                        Left.tiles[j].tileMoneda.GetComponent<Renderer>().enabled = false;
                        tileRend.enabled = true;
                        tileCol.enabled = true;
                    }
                    currentTiles.Add(Left.tiles[j].tileVacio);
                }

            }
            j++;
        }
        j = 0;
        for (int i = 10; i < positions.Count; i++)
        {
            tileMonedaRend = Right.tiles[j].tileMoneda.GetComponent<Renderer>();

            tileRend = Right.tiles[j].tileVacio.GetComponent<Renderer>();
            tileCol = Right.tiles[j].tileVacio.GetComponent<Collider>();

            monedaRend = Right.tiles[j].tileMoneda.transform.GetChild(0).gameObject.GetComponent<Renderer>();
            monedaCol = Right.tiles[j].tileMoneda.transform.GetChild(0).gameObject.GetComponent<Collider>();
            
            if (GameManager.instance.invincible)
            {
                //tile = Right.tiles[j].tileMoneda;
                if(!tileMonedaRend.enabled){
                    tileRend.enabled = false;
                    tileCol.enabled = false;
                    tileMonedaRend.enabled = true;
                }
                //moneda = tile.transform.GetChild(0).gameObject;
                tileMonedaRend.enabled = true;
                int spawnCoin = Random.Range(0, 5);
                if (spawnCoin < 3)
                {
                    if(monedaRend.enabled){
                        monedaRend.enabled = false;
                        monedaCol.enabled = false;
                    }
                }
                else
                {
                    if(!monedaRend.enabled){
                        monedaRend.enabled = true;
                        monedaCol.enabled = true;
                    }
                }
                currentTiles.Add(Right.tiles[j].tileMoneda);

            }
            else
            {
                if (positions[i] == 1)
                {
                    //tile = Right.tiles[j].tileMoneda;
                    //moneda = tile.transform.GetChild(0).gameObject;
                    if(!tileMonedaRend.enabled){
                        tileRend.enabled = false;
                        tileCol.enabled = false;
                        tileMonedaRend.enabled = true;
                    }
                    int spawnCoin = Random.Range(0, 5);
                    if (spawnCoin < 4)
                    {
                        if(monedaRend.enabled){
                            monedaRend.enabled = false;
                            monedaCol.enabled = false;
                        }
                    }
                    else
                    {
                        if(!monedaRend.enabled){
                            monedaRend.enabled = true;
                            monedaCol.enabled = true;
                        }
                    }

                    currentTiles.Add(Right.tiles[j].tileMoneda);
                }

                else if (positions[i] == 0)
                {
                    tile = Right.tiles[j].tileVacio;
                    if(!tileRend.enabled){
                        Right.tiles[j].tileMoneda.GetComponent<Renderer>().enabled = false;
                        tileRend.enabled = true;
                        tileCol.enabled = true;
                    }
                    currentTiles.Add(tile);
                }

            }
            j++;
        }
    }

    
    public void DeletePositions()
    {
        Collider thisCollider;
        foreach (GameObject tile in currentTiles)
        {
            tile.gameObject.GetComponent<Renderer>().enabled = false;
            thisCollider = tile.GetComponent<Collider>();
            if (thisCollider != null) {
                thisCollider.enabled = false;
            }
            else {
                tile.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            }
        }
        currentTiles.Clear();
    }

}
