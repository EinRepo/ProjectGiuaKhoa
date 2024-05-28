using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wallsPrefab;
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private Transform player;

    private float playerLastYPosition;
    private float wallsHeight = 3f; // Assuming this is the height of the wall
    private float backgroundHeight = 3.5f;
    private float wallSpawnYPosition;
    private float backgroundSpawnYPosition;
    private float playerDistanceMoved;
    private float playerExcessDistanceMoved = 0; //compensate for lag


    private void Start()
    {

        SpawnInitialWalls(10);
        SpawnInitialBackgrounds(10);

        playerLastYPosition = player.transform.position.y;

    }


    private void Update()
    {

        playerDistanceMoved = player.transform.position.y - playerLastYPosition;


        if (playerDistanceMoved >= wallsHeight - playerExcessDistanceMoved)
        {
            SpawnWall();
            SpawnBackground();
            wallSpawnYPosition += wallsHeight; // Update the spawn position
            backgroundSpawnYPosition += backgroundHeight;
            playerExcessDistanceMoved = playerDistanceMoved - wallsHeight;
            playerDistanceMoved = 0;
            playerLastYPosition = player.transform.position.y;
        }




    }

    private void SpawnWall()
    {
        Instantiate(wallsPrefab, new Vector3(0, wallSpawnYPosition, 0), Quaternion.identity);
    }


    private void SpawnBackground()
    {
        Instantiate(backgroundPrefab, new Vector3(0, backgroundSpawnYPosition, 0), Quaternion.identity);
    }

    private void SpawnInitialBackgrounds(int amt)
    {
        //initial backgrounds
        for (int i = 0; i < amt; i++)
        {
            SpawnBackground();
            backgroundSpawnYPosition += backgroundHeight;
        }
    }


    private void SpawnInitialWalls(int amt)
    {
        //initial walls
        for (int i = 0; i < amt; i++)
        {
            SpawnWall();
            wallSpawnYPosition += wallsHeight;
        }

    }

}
