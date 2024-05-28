using UnityEngine;

public class TrailMovement : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Transform player;

    private void Update()
    {
        // Get the player's position
        Vector2 targetPosition = player.transform.position;

        // Calculate the desired position behind the player
        Vector2 back = -player.transform.forward * distance;



        transform.position = targetPosition + back;


        // Look at the PLAYER
        transform.LookAt(targetPosition);
    }
}
