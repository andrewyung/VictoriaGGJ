using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float moveForce = 40;
    public float jumpForce = 10;

    private Rigidbody2D rigidbody;
    private Vector2 movementVector;
    private enum JumpState { Ground, SingleJump, DoubleJump };
    private JumpState jumpState;

    //player faces right by default
    private bool isFacingRight = true;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        movementVector = new Vector2();
        jumpState = JumpState.Ground;
        excitementJump();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("left");
            moveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("right");
            moveRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("jump");
            jump();
        }

        if (rigidbody.velocity == Vector2.zero)
        {
            anim.SetBool("Walking", false);
        }
    }

    void moveLeft()
    {
        movementVector.Set(-moveForce, 0);
        rigidbody.AddForce(movementVector);

        //orient player to the direction of movement
        if (isFacingRight)
        {
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }

        anim.SetBool("Walking", true);
    }

    void moveRight()
    {
        movementVector.Set(moveForce, 0);
        rigidbody.AddForce(movementVector);

        //orient player to the direction of movement
        if (!isFacingRight)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }

        anim.SetBool("Walking", true);
    }

    void jump()
    {
        movementVector.Set(0, jumpForce);
        Debug.Log(jumpState);
        switch (jumpState)
        {
            case JumpState.Ground:
                jumpState = JumpState.SingleJump;
                rigidbody.AddForce(movementVector);
                break;
            case JumpState.SingleJump:
                jumpState = JumpState.DoubleJump;
                rigidbody.AddForce(movementVector);
                break;
            case JumpState.DoubleJump:
            default:
                break;
        }

        anim.SetBool("Walking", false);
    }

    void excitementJump()
    {
        jump();
        jump();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (jumpState == JumpState.SingleJump || jumpState == JumpState.DoubleJump)
        {
            jumpState = JumpState.Ground;
        }
    }

}
