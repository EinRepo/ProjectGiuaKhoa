using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnInterval = 10f;

    private float leftXPosition = -1.2f;
    private float rightXPosition = 1.2f;
    private float spawnYPosition;




    void Start()
    {
        //spawn above player

        // Start spawning bats at regular intervals
        InvokeRepeating(nameof(SpawnBat), 7f, spawnInterval);
    }

    void SpawnBat()
    {
        int randomChoice = Random.Range(0, 2); //0 or 1
        if (randomChoice == 0)
        {
            Instantiate(batPrefab, new Vector3(leftXPosition, spawnYPosition, 0), Quaternion.identity);
        }
        else if (randomChoice == 1)
        {
            Instantiate(batPrefab, new Vector3(rightXPosition, spawnYPosition, 0), Quaternion.identity);
        }
    }
}
