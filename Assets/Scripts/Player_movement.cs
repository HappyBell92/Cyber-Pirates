using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed = 10f;
    public float runSpeed = 15f;
    private int direction = 1;
    public bool walking;
    public bool running;
    public bool atDoor = false;
    public bool usingLadder = false;

    [Header("Keybinds")]
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode leftMove = KeyCode.A;
    public KeyCode rightMove = KeyCode.D;
    public KeyCode doorInterract = KeyCode.E;
    public KeyCode ladderUp = KeyCode.W;
    public KeyCode ladderDown = KeyCode.S;

    [Header("Ground Check")]
    public bool grounded;

    [Header("Rigidbody")]
    public Rigidbody2D rb;
    public GameObject player;

    private Animator anim;

    public Door_script doorScript;

    public Vector3 pos;
    

    public MovementState state;
    public enum MovementState
    {
        walking,
        running,
        air
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();
        Movement();
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.CompareTag("Door"))
        {
            atDoor = true;
            doorScript = collision.gameObject.GetComponent<Door_script>();
            pos = doorScript.exit.transform.position;

            if (Input.GetKeyDown(doorInterract))
            {
                player.transform.position = new Vector3(pos.x, pos.y, 0);
            }
        }

        if (collision.CompareTag("Ladder"))
        {
            usingLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = false;
        }

        if (collision.CompareTag("Door"))
        {
            doorScript = null;
            atDoor = false;
        }

        if (collision.CompareTag("Ladder"))
        {
            usingLadder = false;
        }
    }

    private void StateHandler()
    {
        // Mode - Running
        if(grounded && Input.GetKey(runKey))
        {
            Debug.Log("Running");
            state = MovementState.running;
            moveSpeed = runSpeed;
            running = true;
            walking = false;
        }
        else if (grounded)
        {
            Debug.Log("Walking");
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            running = false;
            walking = true;
        }
    }

    private void Movement()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);
        anim.SetBool("isKickBoard", false);

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
            {
                if (running)
                {
                    anim.SetBool("isRun", false);
                    anim.SetBool("isKickBoard", true);
                }
                else if (walking)
                {
                    anim.SetBool("isRun", true);
                    anim.SetBool("isKickBoard", false);
                }
            }
                

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
            {
                if (running)
                {
                    anim.SetBool("isRun", false);
                    anim.SetBool("isKickBoard", true);
                }
                else if (walking)
                {
                    anim.SetBool("isRun", true);
                    anim.SetBool("isKickBoard", false);
                }
            }
        }
        if (usingLadder)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                moveVelocity = Vector3.up;

                transform.localScale = new Vector3(1, 1, 1);
                if (!anim.GetBool("isJump"))
                {
                    if (running)
                    {
                        anim.SetBool("isRun", false);
                        anim.SetBool("isKickBoard", true);
                    }
                    else if (walking)
                    {
                        anim.SetBool("isRun", true);
                        anim.SetBool("isKickBoard", false);
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                moveVelocity = Vector3.down;

                transform.localScale = new Vector3(1, 1, 1);
                if (!anim.GetBool("isJump"))
                {
                    if (running)
                    {
                        anim.SetBool("isRun", false);
                        anim.SetBool("isKickBoard", true);
                    }
                    else if (walking)
                    {
                        anim.SetBool("isRun", true);
                        anim.SetBool("isKickBoard", false);
                    }
                }
            }
        }
        if (!usingLadder)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }

            transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }

   
}
