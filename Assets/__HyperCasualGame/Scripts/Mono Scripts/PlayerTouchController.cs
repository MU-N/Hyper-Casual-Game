using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] GameEvent playerDie;
    [SerializeField] GameContrllerData GCD;
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
        GCD.isGameWin = false;
        GCD.isGameLose = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!GCD.isGameLose && !GCD.isGameWin)
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

            input = new Vector3(horizontalInput, 0f, verticalInput);
            direction = input.x * cameraTransform.right + input.z * cameraTransform.forward;
            direction.y -= gravityValue * Time.deltaTime;


            characterController.Move(direction * playerSpeed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cameraTransform.forward.x;
                Quaternion rotaion = Quaternion.Euler(0f, targetAngle, 0f);
                gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotaion, Time.deltaTime * rotationSpeed);
            }


            playerVelocity.y += gravityValue * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);

            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z - 12f);

            animator.SetBool("isWalking", horizontalInput != 0f || verticalInput != 0f);
        }
        else
        {
            if (GCD.isGameWin)
            {
                //animtion dance;
            }
            else if (GCD.isGameLose)
            {
                Die();
            }
        }

    }
    public void Die()
    {
        animator.SetBool("isDead", true);
        StartCoroutine(WaitForSeconds(1f));
    }
    IEnumerator WaitForSeconds(float delay)
    {

        yield return new WaitForSeconds(delay);
        playerDie.Raise();
    }
}
