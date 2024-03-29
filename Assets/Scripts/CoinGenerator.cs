// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CoinGenerator : MonoBehaviour
// {
//     public ObjectPooler coinPool;
//     public float distanceBetweenCoins;

//     public void SpawnCoins(Vector3 startPositions)
//     {
//         // Get the first coin from the pool
//         GameObject coin1 = coinPool.GetPooledObject();
//         coin1.transform.position = startPositions;
//         coin1.SetActive(true);

//         // Get the second coin from the pool and position it to the left of the first coin
//         GameObject coin2 = coinPool.GetPooledObject();
//         coin2.transform.position = new Vector3(startPositions.x - distanceBetweenCoins, startPositions.y, startPositions.z);
//         coin2.SetActive(true);

//         // Get the third coin from the pool and position it to the right of the first coin
//         GameObject coin3 = coinPool.GetPooledObject();
//         coin3.transform.position = new Vector3(startPositions.x + distanceBetweenCoins, startPositions.y, startPositions.z);
//         coin3.SetActive(true);
//     }
// }
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public ObjectPooler coinPool;
    
    // Define the x-positions where coins can spawn
    private float[] coinXPositions = { -1.5f, 0f, 1.5f };

    public float distanceBetweenCoins;

    public void SpawnCoins(Vector3 startPosition)
    {
        // Loop through the predefined x-positions to spawn coins
        for (int i = 0; i < coinXPositions.Length; i++)
        {
            GameObject coin = coinPool.GetPooledObject();
            coin.transform.position = new Vector3(coinXPositions[i], 3.76f, startPosition.z);
            coin.SetActive(true);
        }
    }
}

