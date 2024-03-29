using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefab;
    [SerializeField] GameObject coinPrefab;
    private Transform playerTransform;

    private float spawnZ = 0.0f;
    private float tileLength = 290.0f;
    private int amnTilesOnScreen = 7;

    private float safeZone = 300.0f;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;

    private CoinGenerator theCoinGenerator;

    private ObstacleGenerator theObstacleGenerator;

    // Speed at which the tiles move towards the player
    public float tileMoveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform != null)
        {
            for (int i = 0; i < amnTilesOnScreen; i++)
            {
                SpawnTile();
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found in the scene!");
        }
        // SpawnCoins();

        // theCoinGenerator=FindObjectOfType<CoinGenerator>();
         theObstacleGenerator = FindObjectOfType<ObstacleGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has moved into the safe zone
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }

        MoveTiles();
    }

private void SpawnTile(int prefabIndex = -1)
{
    GameObject go;
    go = Instantiate(tilePrefab[RandomPrefabIndex()], Vector3.forward * spawnZ, Quaternion.identity) as GameObject;
    go.transform.SetParent(transform);
    spawnZ += tileLength;
    activeTiles.Add(go);

    theObstacleGenerator.SpawnObstacles(go.transform.position);
    // theCoinGenerator.SpawnCoins(go.transform.position);
}
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefab.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefab.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    private void MoveTiles()
    {
        // Move all active tiles towards the player
        foreach (var tile in activeTiles)
        {
            // Calculate the direction of movement
            Vector3 moveDirection = Vector3.back * tileMoveSpeed * Time.deltaTime;

            // Translate the tile
            tile.transform.Translate(moveDirection);
        }
    }



    // Destroy tiles that are behind the player
    public void DestroyPassedTiles()
    {
        for (int i = 0; i < activeTiles.Count; i++)
        {
            if (activeTiles[i].transform.position.z < playerTransform.position.z - safeZone)
            {
                Destroy(activeTiles[i]);
                activeTiles.RemoveAt(i);
                i--;
            }
        }
    }

    
}
