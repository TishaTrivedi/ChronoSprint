using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public ObjectPooler obstaclePool;
    public float distanceBetweenObstacles;
    public int numberOfObstacles;
    public Vector3[] tileStartPositions;
    public float tileSizeZ;
    public float[] obstacleXPositions = { -1.5f, 0f, 1.5f };

    public void SpawnObstacles(Vector3 startPosition)
    {
        float obstacleSpacing = tileSizeZ/(numberOfObstacles+1);
        
        for (int i = 0; i < numberOfObstacles; i++)
        {
            GameObject obstacle = obstaclePool.GetPooledObject(Random.Range(0,2));
            float zPosition = startPosition.z + obstacleSpacing * (i + 1);  
            int randomXIndex = Random.Range(0, obstacleXPositions.Length);
            float xPosition = startPosition.x + obstacleXPositions[randomXIndex];
            obstacle.transform.position = new Vector3(xPosition, startPosition.y+2, zPosition);
            obstacle.SetActive(true);
        }
    }
}