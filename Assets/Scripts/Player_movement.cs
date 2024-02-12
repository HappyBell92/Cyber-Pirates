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

    [Header("Keybinds")]
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode leftMove = KeyCode.A;
    public KeyCode rightMove = KeyCode.D;
    public KeyCode doorInterract = KeyCode.W;

    [Header("Ground Check")]
    public bool grounded;

    [Header("Rigidbody")]
    public Rigidbody2D rb;

    private Animator anim;
    

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = false;
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
        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }
}
