using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public ObjectPooler obstaclePool;
    public float distanceBetweenObstacles;
    public Vector3[] tileStartPositions;
    public float tileSizeZ = 50f;
    public float[] obstacleXPositions = { -1.5f, 0f, 1.5f };

    public void SpawnObstacles(Vector3 startPosition)
    {
        int numberOfObstacles = 3;
        float obstacleSpacing = tileSizeZ / (numberOfObstacles + 1);

        for (int i = 0; i < numberOfObstacles; i++)
        {
            GameObject obstacle = obstaclePool.GetPooledObject();
            float zPosition = startPosition.z + obstacleSpacing * (i + 1);  
            int randomXIndex = Random.Range(0, obstacleXPositions.Length);
            float xPosition = startPosition.x + obstacleXPositions[randomXIndex];
            obstacle.transform.position = new Vector3(xPosition, startPosition.y, zPosition);
            obstacle.SetActive(true);
        }
    }
}