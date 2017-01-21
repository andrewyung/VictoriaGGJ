using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D sprite;
    private Vector2 movementVector;
    private enum JumpState {Ground, SingleJump, DoubleJump};
    private JumpState jumpState;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<Rigidbody2D>();
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
        if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W))
        {
            Debug.Log("jump");
            jump();
        }
    }

    void moveLeft()
    {
        movementVector.Set(-50, 0);
        sprite.AddForce(movementVector);
    }

    void moveRight()
    {
        movementVector.Set(50, 0);
        sprite.AddForce(movementVector);
    }

    void jump()
    {
        movementVector.Set(0, 500);
        switch (jumpState) {
            case JumpState.Ground:
                jumpState = JumpState.SingleJump;
                sprite.AddForce(movementVector);
                break;
            case JumpState.SingleJump:
                jumpState = JumpState.DoubleJump;
                sprite.AddForce(movementVector);
                break;
            case JumpState.DoubleJump:
            default:
                break;
        }
    }

    void excitementJump()
    {
        jump();
        jump();
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (jumpState == JumpState.SingleJump || jumpState == JumpState.DoubleJump) {
            jumpState = JumpState.Ground;
        }
    }

}
