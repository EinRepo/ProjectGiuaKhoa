using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float spawnInterval = 20f;

    private float leftXPosition = -1.2f;
    private float rightXPosition = 1.2f;
    private float spawnYPosition;


    void Start()
    {

        // Start spawning bats at regular intervals
        InvokeRepeating(nameof(SpawnBat), 3, spawnInterval);
    }

    private void Update()
    {
        spawnYPosition = mainCamera.position.y + 10f;

    }

    void SpawnBat()
    {
        int randomChoice = Random.Range(0, 2); //0 or 1
        if (randomChoice == 0)
        {
            Instantiate(batPrefab, mainCamera);
            batPrefab.transform.position = new Vector3(leftXPosition, spawnYPosition, 0);
        }
        else if (randomChoice == 1)
        {
            Instantiate(batPrefab, mainCamera);
            batPrefab.transform.position = new Vector3(rightXPosition, spawnYPosition, 0);
        }
    }

}
