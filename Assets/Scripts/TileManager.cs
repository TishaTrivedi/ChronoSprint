using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefab;
    public GameObject coinPrefab;
    public Transform playerTransform;
    public Transform mainCameraTransform;

    private float spawnZ = 0.0f;
    private float tileLength =500.0f;
    private int amnTilesOnScreen = 7;
    private float safeZone = 510.0f;
    private int lastPrefabIndex = 0;
    private List<GameObject> activeTiles;

    public float cameraSpeed = 10.0f;

    // Reference to the CoinGenerator script
    public CoinGenerator coinGenerator;

    // Reference to the ObstacleGenerator script
    public ObstacleGenerator obstacleGenerator;

    void Start()
    {
        activeTiles = new List<GameObject>();
        mainCameraTransform = Camera.main.transform;

        if (playerTransform == null)
        {
            Debug.LogWarning("Player GameObject not found. Disabling player movement.");
        }

        // Fetch references to CoinGenerator and ObstacleGenerator scripts
        coinGenerator = FindObjectOfType<CoinGenerator>();
        obstacleGenerator = FindObjectOfType<ObstacleGenerator>();

        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        mainCameraTransform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime);

        if (mainCameraTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }

        if (playerTransform != null)
        {
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, mainCameraTransform.position.z);
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go = Instantiate(tilePrefab[RandomPrefabIndex()], Vector3.forward * spawnZ, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        spawnZ += tileLength;
        activeTiles.Add(go);

        if (obstacleGenerator != null)
        {
            obstacleGenerator.SpawnObstacles(go.transform.position);
        }

        if (coinGenerator != null)
        {
            coinGenerator.SpawnCoins(go.transform.position);
        }
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
