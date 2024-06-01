using System.Collections;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform player;
    [SerializeField] float dashForce;

    PlayerController playerController;
    private Rigidbody2D rb;
    private LineRenderer attackIndicator;
    private Animator animator;
    private Vector2 batIdleSpot;
    private Vector2 idleSpot1;
    private Vector2 idleSpot2;



    private float batXpos;
    private float magnitude;
    private bool flag1 = true;



    private void Start()
    {
        attackIndicator = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();


        idleSpot1 = new Vector2(1.3f, 2);
        idleSpot2 = new Vector2(-1.3f, 2);
        batIdleSpot = (Random.Range(0, 2) == 0) ? idleSpot1 : idleSpot2;

        InvokeRepeating(nameof(InitiateDash), 8, 10);
    }

    private void Update()
    {
        Idle();
        LookAtPlayer();


    }

    private void Idle()
    {
        if (flag1)
        {
            //lerp towards an idle spot 
            transform.localPosition = Vector2.Lerp(transform.localPosition, batIdleSpot, Time.deltaTime);
        }
        magnitude = Vector2.Distance(transform.position, batIdleSpot);



    }

    private void LookAtPlayer()
    {
        batXpos = transform.position.x;
        if (batXpos > player.transform.position.x)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
    }

    private void InitiateDash()
    {
        // Start the coroutine to show the attack indicator and then dash
        StartCoroutine(DashSequence());
    }

    private IEnumerator DashSequence()
    {
        // Create the attack indicator
        attackIndicator.enabled = true;


        // Configure the line renderer
        attackIndicator.positionCount = 2;


        // First 1.5 seconds: follow the player
        float elapsed = 0f;
        while (elapsed < 1.5f)
        {
            if (attackIndicator != null)
            {
                attackIndicator.SetPosition(0, transform.position); // Starting point of the line (bat's position)
                attackIndicator.SetPosition(1, player.position);    // End point of the line (player's position)
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Next 0.5 seconds: freeze the indicator in place
        float elapsed2 = 0f;
        Vector2 playerLastPosition = mainCamera.transform.InverseTransformPoint(player.position);
        while (elapsed2 < 0.5f)
        {
            if (attackIndicator != null)
            {
                attackIndicator.SetPosition(0, transform.position); // Starting point of the line (bat's position)
                attackIndicator.SetPosition(1, mainCamera.transform.TransformPoint(playerLastPosition));    // End point of the line (player's position in camera's local space)
            }
            elapsed2 += Time.deltaTime;
            yield return null;
        }

        // Dash in the direction of the frozen indicator
        if (attackIndicator != null)
        {
            Vector2 directionToIndicator = (attackIndicator.GetPosition(1) - transform.position).normalized;
            rb.AddForce(directionToIndicator * dashForce, ForceMode2D.Impulse);

            // Disable the attack indicator after dashing
            attackIndicator.enabled = false;
        }
    }

    public void IsDead()
    {
        animator.SetTrigger("batIsDead");
        StartCoroutine(DestroyAfterDelay(gameObject, 0.2f));
    }

    private IEnumerator DestroyAfterDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerController.Invulnerable)
        {
            flag1 = false;
            IsDead();
        }
    }
}
