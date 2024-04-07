using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public ObjectPooler obstaclePool;
    public float distanceBetweenObstacles; // Adjust this value to control spacing
    public int numberOfObstacles;
    public Vector3[] tileStartPositions;
    public float tileSizeZ;
    public float[] obstacleXPositions = { -1.5f, 0f, 1.5f };
    public float obstacleZOffset = -10.0f; // Base Z offset
    public float obstacleZOffsetRandomRange = 1.0f; // Optional randomness range

    public void SpawnObstacles(Vector3 playerPosition)
    {
        float obstacleZ = playerPosition.z + obstacleZOffset;
        Debug.Log("Calculated obstacle Z position: " + obstacleZ);

        // Optional randomness for Z position
        if (obstacleZOffsetRandomRange > 0)
        {
            obstacleZ += Random.Range(-obstacleZOffsetRandomRange / 2f, obstacleZOffsetRandomRange / 2f);
        }

        for (int i = 0; i < numberOfObstacles; i++)
        {
            GameObject obstacle = obstaclePool.GetPooledObject(Random.Range(0, obstaclePool.pooledObjects.Length)); // Corrected line
            float xPosition = playerPosition.x + obstacleXPositions[Random.Range(0, obstacleXPositions.Length)];
            obstacle.transform.position = new Vector3(xPosition, playerPosition.y + 2, obstacleZ);
            obstacle.SetActive(true);

            // Update obstacleZ for the next obstacle using constant spacing
            obstacleZ += distanceBetweenObstacles;
        }
    }
}
