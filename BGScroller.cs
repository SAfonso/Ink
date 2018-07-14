using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;

    private float posY;
    private TileSpawner tileSpawner;

    private int level = 0;

    void Start(){
        posY = transform.position.y;
        tileSpawner = this.gameObject.GetComponent<TileSpawner>();
    }

    void Update()
    {
        if (!GameManager.instance.gameOver) {
            posY -= Time.deltaTime * GameManager.instance.globalSpeed;
            this.transform.position = new Vector3(0, posY, 0);
            if (this.transform.position.y <= -10f)
            {
                // Deactivate actual tiles
                //tileSpawner.DeletePositions();
                // Select a random block depending on the difficulty level
                //Debug.Log("Level >>>> " + level);
                // Read Positions from file
                if (GameManager.instance.minMap < 2)
                {
                    level = Random.Range(GameManager.instance.minMap, GameManager.instance.maxMap);
                    Debug.Log("Leemos de fichero" + level);
                    tileSpawner.GetPositions(level, false);
                }
                else // Generate new procedural levels
                {
                    level = Random.Range(3, 5);
                    tileSpawner.GetPositions(level, true);
                }
                // Spawn tiles
                tileSpawner.SpawnTiles();
                posY = posY + 20f;
            }
        }
        
    }
}
