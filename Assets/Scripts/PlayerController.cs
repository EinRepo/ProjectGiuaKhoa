using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float jumpSpeedBoost = 1;


    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    private float wallCheckDistance = 0.2f;
    private bool isTouchingWall;
    private bool isDead = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();
        if (isDead)
        {
            this.enabled = false;
        }
    }

    private void MovePlayer()
    {
        //raycast
        RaycastHit2D isTouchingRightWall = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.right, boxCollider2D.bounds.extents.x + wallCheckDistance, LayerMask.GetMask("Wall"));
        RaycastHit2D isTouchingLeftWall = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.left, boxCollider2D.bounds.extents.x + wallCheckDistance, LayerMask.GetMask("Wall"));
        isTouchingWall = isTouchingRightWall || isTouchingLeftWall;

        //move the player upwards
        rb.velocity = new Vector2(rb.velocity.x, moveSpeed * jumpSpeedBoost);
        jumpSpeedBoost = Mathf.Lerp(jumpSpeedBoost, 1, Time.deltaTime);


        //push the player to the wall so they dont drift away
        if (transform.position.x > 0)
        {
            rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime); //since this force is called many times, i added delta time to reduce the force and keep it consistant
        }
        else
        {
            rb.AddForce(Vector2.left * moveSpeed * Time.deltaTime);
        }




        //jump
        if (Input.GetButtonDown("Jump") && isTouchingWall)
        {
            rb.velocity = Vector3.zero;
            float jumpDirection = isTouchingRightWall ? -1 : 1;
            rb.AddForce(Vector2.right * jumpDirection * jumpForce, ForceMode2D.Impulse);
            jumpSpeedBoost = 2;
            transform.localScale = new Vector3(transform.localScale.x, -1 * transform.localScale.y, transform.localScale.z);
        }

    }

    public bool IsDead()
    {
        return isDead;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            isDead = true;
            moveSpeed = 0;
            animator.SetTrigger("isDead");

        }
    }


}
