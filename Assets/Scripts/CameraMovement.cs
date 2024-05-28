using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;

    private float cameraYPos;
    private float smoothSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //beginning intro
        if (target.transform.position.y < 8)
        {
            transform.position = new Vector3(transform.position.x, 6, transform.position.z);
        }
        else
        {
            cameraYPos = Mathf.Lerp(transform.position.y, target.transform.position.y + 3, Time.deltaTime * smoothSpeed);
            transform.position = new Vector3(transform.position.x, cameraYPos, transform.position.z);

        }

    }
}
