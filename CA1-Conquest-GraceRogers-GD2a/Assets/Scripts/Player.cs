using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    public int speed;
    private int direction;
    private Rigidbody2D rb;
    private Vector2 move;
    private Animator animator;
    public float jumpHeight = 2.4f;
    public int baseJumpCount = 2;
    private int jumpCount;
    private int score = 0;

    [SerializeField] private GameManager gm;
    private int lives = 5;
    private Vector2 lastPosition;


    //Spawnpoint for player
    public Transform spawnPoint;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = baseJumpCount;
    }

    public void Spawn() //spawnpoint method
    {
        rb.transform.position = spawnPoint.position;
    }

    //Movement & Jumping
    void Update()
    {
        move = InputSystem.actions["Move"].ReadValue<Vector2>();

        if (move.x != 0)
        {
            direction = move.x < 0 ? 1 : -1;
            animator.SetInteger("Direction", direction);
        }
        animator.SetFloat("Move", move.x);


        if (InputSystem.actions["Jump"].IsPressed() && jumpCount > 0)
        {
            InputSystem.actions["Jump"].Reset();
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpHeight)), ForceMode2D.Impulse);
            jumpCount -= 1;
            animator.SetBool("Jumping", true);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb.transform.position;
        position.x += (move.x * speed) * Time.fixedDeltaTime;
        rb.transform.position = position;
    }

    //When the player touches the ground, changes the Boolean 'jumping' to false, to stop the jump animation
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = baseJumpCount;
            animator.SetBool("Jumping", false);
        }
        if (collision.gameObject.tag == "Enemy Projectile")
        {
            if(lives <= 1) //if the player loses all their hearts
            {
                Spawn(); //respawn them to the object 'spawnpoint'
            }
            lives--;
            gm.updateLives(lives);
        }
    }

    public void AddCollectible()
    {
        score++;
        gm.UpdateScore(score);
    }
}
