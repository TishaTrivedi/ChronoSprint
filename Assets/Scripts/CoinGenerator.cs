using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public ObjectPooler coinPool;
    public float distanceBetweenCoins; // Adjust this value to control spacing
    public int numberOfCoins;
    public Vector3[] tileStartPositions;
    public float tileSizeZ;
    public float[] coinXPositions = { -1.5f, 0f, 1.5f };
    public float coinYPosition = 2.0f; // Y position of the coins
    public float coinZOffset = -10.0f; // Base Z offset
    public float coinZOffsetRandomRange = 1.0f; // Optional randomness range

    public void SpawnCoins(Vector3 playerPosition)
    {
        float coinSpacing = distanceBetweenCoins * (numberOfCoins + 1); // Increased spacing
        float coinZ = playerPosition.z + coinZOffset;

        // Optional randomness for Z position
        if (coinZOffsetRandomRange > 0)
        {
            coinZ += Random.Range(-coinZOffsetRandomRange / 2f, coinZOffsetRandomRange / 2f);
        }

        for (int i = 0; i < numberOfCoins; i++)
        {
            GameObject coin = coinPool.GetPooledObject(0); // Assuming 0 is the index of your coin prefab

            // Check if the object is null before using it
            if (coin != null)
            {
                float xPosition = playerPosition.x + coinXPositions[Random.Range(0, coinXPositions.Length)];
                coin.transform.position = new Vector3(xPosition, coinYPosition, coinZ);
                coin.SetActive(true);

                // Update coinZ for the next coin
                coinZ += coinSpacing;
            }
        }
    }
}