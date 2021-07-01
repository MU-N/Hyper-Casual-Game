using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float playerSpeed;


    bool isActive = true;
    float horizontalInput, verticalInput;

    CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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

        characterController.Move(new Vector3(horizontalInput,0f,verticalInput) * Time.deltaTime );
    }
}
