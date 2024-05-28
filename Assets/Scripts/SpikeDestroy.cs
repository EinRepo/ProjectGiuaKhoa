using UnityEngine;

public class SpikeDestroy : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();

    }
    private void Update()
    {
        if (player.position.y > transform.position.y + 6f)
        {
            Destroy(gameObject);
        }
    }

}
