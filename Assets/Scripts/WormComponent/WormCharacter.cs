using UnityEngine;

// Related to movement
public enum Direction { Left, Right, Stop }
public class WormCharacter : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1;
    [SerializeField]
    private float jumpForce = 3;
    [Range(0.4f, 0.5f)]
    [SerializeField]
    private float airFactor = 0.4f;

    public bool onGround = false;

    private bool isDead = false;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
    }
	
    public void Jump()
    {
        AnimJump(true);
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
    }

    public void Movement(Direction dir)
    {
        switch (dir)
        {
            case Direction.Right:
                AnimWalk(true);
                sRenderer.flipX = true;
                transform.position = new Vector2(transform.position.x + (movementSpeed * Time.deltaTime), transform.position.y);
                break;
            case Direction.Left:
                AnimWalk(true);
                sRenderer.flipX = false;
                transform.position = new Vector2(transform.position.x - (movementSpeed * Time.deltaTime), transform.position.y);
                break;
            case Direction.Stop:
                AnimWalk(false);
                break;
        }
    }

    public void MovementInAir(Direction dir)
    {
        switch (dir)
        {
            case Direction.Right:
                transform.position = new Vector2(transform.position.x + (movementSpeed * airFactor * Time.deltaTime), transform.position.y);
                break;
            case Direction.Left:
                transform.position = new Vector2(transform.position.x - (movementSpeed * airFactor * Time.deltaTime), transform.position.y);
                break;
            case Direction.Stop:
                AnimWalk(false);
                break;
        }
    }

    public void AnimJump(bool active)
    {
        if (!anim) anim = GetComponent<Animator>();
        anim.SetBool("Jump", active);
    }

    public void AnimWalk(bool active)
    {
        if (!anim) anim = GetComponent<Animator>();
        anim.SetBool("Walk", active);
    }

    public void AnimDie()
    {
        if (!anim) anim = GetComponent<Animator>();
        anim.SetBool("Die", true);
    }

    public void AnimDead()
    {
        ///TODO : Here !!
        //if (!anim) anim = GetComponent<Animator>();
        //anim.SetBool("Dead", true);

        Destroy(gameObject, 0.1f);
    }
}
