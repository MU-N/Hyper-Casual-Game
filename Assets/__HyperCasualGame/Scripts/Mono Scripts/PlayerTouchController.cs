using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float playerSpeed;



    float horizontalInput, verticalInput;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal >= .2)
            horizontalInput = playerSpeed;
        else if (joystick.Horizontal <= -.2)
            horizontalInput = -playerSpeed;
        else
            horizontalInput = 0f;

        if (joystick.Vertical >= .2)
            verticalInput = playerSpeed;
        else if (joystick.Vertical <= -.2)
            verticalInput = -playerSpeed;
        else
            verticalInput = 0f;

        rb.velocity = new Vector3(horizontalInput, rb.velocity.y, verticalInput);
    }
}
