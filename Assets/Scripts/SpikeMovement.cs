using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    private float moveSpeed = 10;

    void Start()
    {

    }

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
