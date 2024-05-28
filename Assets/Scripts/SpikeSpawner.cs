using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spikeLeftPrefab;
    [SerializeField] private GameObject spikeRightPrefab;
    [SerializeField] Transform player;
    private float spawnInterval; // Time in seconds between spawns
    private float spawnYPosition; // Y position to spawn spikes at (above the screen)
    private float leftXPosition = -1.375f; // X position for left spikes
    private float rightXPosition = 1.375f; // X position for right spikes

    private float timeSinceLastSpawn;
    private float timer;
    private float incrementMin;

    void Start()
    {
        incrementMin = 0.5f;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > 10)
        {
            incrementMin += 0.1f;
            timer = 0;
        }

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnSpike();
            timeSinceLastSpawn = 0;
        }
    }

    void SpawnSpike()
    {
        //spawnYposittion
        spawnYPosition = player.position.y + 10f;

        //random interval
        spawnInterval = Random.Range(incrementMin, incrementMin + 0.5f);

        // Randomly choose between 0 (left spike) and 1 (right spike)
        int randomChoice = Random.Range(0, 2);

        if (randomChoice == 0)
        {
            Instantiate(spikeLeftPrefab, new Vector3(leftXPosition, spawnYPosition, 0), Quaternion.Euler(0f, 0f, -90f));
        }
        else
        {
            Instantiate(spikeRightPrefab, new Vector3(rightXPosition, spawnYPosition, 0), Quaternion.Euler(0f, 0f, -90f));
        }
    }
}
