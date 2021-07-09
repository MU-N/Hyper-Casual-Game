using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float playerSpeed;
    [SerializeField] float rotaionSpeed;



    private float horizontalInput, verticalInput;

    private bool isPlayerStoped = false;

    private Rigidbody rb;
    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerStoped)
        {
            if (joystick.Horizontal >= 0.2f)
                horizontalInput = playerSpeed;
            else if (joystick.Horizontal <= -0.2f)
                horizontalInput = -playerSpeed;
            else
                horizontalInput = 0f;

            if (joystick.Vertical >= 0.2f)
                verticalInput = playerSpeed;
            else if (joystick.Vertical <= -0.2f)
                verticalInput = -playerSpeed;
            else
                verticalInput = 0f;

            rb.velocity = new Vector3(0, rb.velocity.y, verticalInput );
            transform.Rotate(new Vector3(0, horizontalInput *rotaionSpeed*  Time.deltaTime, 0));

            animator.SetBool("isWalking", horizontalInput != 0f || verticalInput != 0f);
            // todo : update player movement
            // todo : adding anemy with fild of view
        }
        else
        {
            //if
            //win dance
            //esle
            //cry
        }
        
    }

    public void StopPlayer()
    {
        isPlayerStoped = true;
    }
    public void RunPlayer()
    {
        isPlayerStoped = false;
    }
}
