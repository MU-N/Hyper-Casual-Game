using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float playerSpeed;
    [SerializeField] float rotationSpeed;

    private float horizontalInput, verticalInput;
    private float gravityValue = -9.81f;

    private bool isPlayerStoped = false;

    private Vector3 input;
    private Vector3 direction;
    private Vector3 playerVelocity;
    private Quaternion cameraRotaion;

    private Transform cameraTransform;
    private Animator animator;
    private CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;
        cameraRotaion = cameraTransform.rotation;
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

            input = new Vector3(horizontalInput,0f, verticalInput);
            direction = input.x * cameraTransform.right + input.z * cameraTransform.forward;
            direction.y = 0f;


            characterController.Move(direction * playerSpeed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cameraTransform.forward.x;
                Quaternion rotaion = Quaternion.Euler(0f,targetAngle,0f);
                gameObject.transform.rotation = Quaternion.Lerp(transform.rotation , rotaion , Time.deltaTime * rotationSpeed);
            }


            playerVelocity.y += gravityValue * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);

            cameraTransform.position = new Vector3(cameraTransform.position.x , cameraTransform.position.y , transform.position.z -12f);

            animator.SetBool("isWalking", horizontalInput != 0f || verticalInput != 0f);
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
