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
    public bool atLedge = false;
    public bool usingLadder = false;
    public bool atTalkable = false;

    [Header("Keybinds")]
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode leftMove = KeyCode.A;
    public KeyCode rightMove = KeyCode.D;
    public KeyCode doorInterract = KeyCode.E;
    public KeyCode talk = KeyCode.E;
    public KeyCode ladderUp = KeyCode.W;
    public KeyCode ladderDown = KeyCode.S;

    [Header("Ground Check")]
    public bool grounded;

    [Header("Rigidbody")]
    public Rigidbody2D rb;
    public GameObject player;

    [Header("Raycast")]
    public LayerMask terrainLayer;
    public float sideLenght;
    public float downLength;
    public bool leftHit;
    public bool rightHit;
    public bool leftDownHit;
    public bool rightDownHit;
    public bool talking;

    private Animator anim;

    public Door_script doorScript;
    public Yarninterractable talkScript;

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
        atDoor = false;
        atLedge = false;
        atTalkable = false;
        leftHit = false;
        leftDownHit = false;
        rightHit = false;
        rightDownHit = false;
        talking = false;
}

    // Update is called once per frame
    void Update()
    {
        Interaction();
        StateHandler();
        Movement();
    }

    private void FixedUpdate()
    {
        //RaycastHit2D hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * sideLenght, Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * sideLenght, Color.yellow);
        Debug.DrawRay(transform.position + transform.right * 0.5f, transform.TransformDirection(Vector2.down) * sideLenght, Color.yellow);
        Debug.DrawRay(transform.position + -transform.right * 0.5f, transform.TransformDirection(Vector2.down) * sideLenght, Color.yellow);

        if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), sideLenght, terrainLayer))
        {
            rightHit = true;
        }

        else
        {
             rightHit= false;
        }

        if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), sideLenght, terrainLayer))
        {
            leftHit = true;
        }

        else
        {
            leftHit= false;
        }

        if (Physics2D.Raycast(transform.position + transform.right * 0.5f, transform.TransformDirection(Vector2.down), downLength, terrainLayer))
        {
            rightDownHit = true;
        }

        else { rightDownHit= false; }

        if (Physics2D.Raycast(transform.position + -transform.right * 0.5f, transform.TransformDirection(Vector2.down), downLength, terrainLayer))
        {
            leftDownHit = true;
        }

        else {  leftDownHit= false; }
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
        }

        if (collision.CompareTag("Talkable"))
        {
            talkScript = collision.gameObject.GetComponent<Yarninterractable>();
            atTalkable = true;

            //if (Input.GetKeyDown(talk) && atTalkable && talkScript.interactable && !talking)
            //{
            //    talking = true;
            //    talkScript.StartConversation();
            //}
            //talkScript = collision.gameObject.GetComponent<TalkTrigger>();

            //if (Input.GetKeyDown(talk) && atTalkable)
            //{
            //    talkScript.StartDialogue();
            //}
        }

        if (collision.CompareTag("Ledge"))
        {
            atLedge = true;
            Ledge_script ledgeScript = collision.gameObject.GetComponent<Ledge_script>();
            pos = ledgeScript.destination.transform.position;
            
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

        if (collision.CompareTag("Talkable"))
        {
            talkScript = null;
            atTalkable = false;
        }

        if (collision.CompareTag("Ledge"))
        {

            atLedge= false;
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
            //Debug.Log("Running");
            state = MovementState.running;
            moveSpeed = runSpeed;
            running = true;
            walking = false;
        }
        else if (grounded)
        {
            //Debug.Log("Walking");
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            running = false;
            walking = true;
        }
    }

    private void Movement()
    {
        if (!talking)
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);
            anim.SetBool("isKickBoard", false);

            if (Input.GetAxisRaw("Horizontal") < 0 && !leftHit && leftDownHit)
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
            if (Input.GetAxisRaw("Horizontal") > 0 && !rightHit && rightDownHit)
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

    void Interaction()
    {
        if (Input.GetKeyDown(doorInterract) && atDoor)
        {
            //Debug.Log("using door");
            player.transform.position = new Vector3(pos.x, pos.y, 0);
        }

        if (Input.GetKeyDown(doorInterract) && atLedge)
        {
            //Debug.Log("Using Ledge");
            player.transform.position = new Vector3(pos.x, pos.y, 0);
        }

        if (Input.GetKeyDown(talk) && atTalkable && talkScript.interactable && !talking)
        {
            talking = true;
            talkScript.StartConversation();
        }
        //if (Input.GetKeyDown(talk) && atTalkable)
        //{
        //    talkScript.StartConversation();
        //}
    }

   
}
