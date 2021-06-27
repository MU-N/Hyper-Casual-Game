using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float playerSpeed;


    bool isActive = true;
    float her, ver;

    CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(joystick.Horizontal);
        if (joystick.Horizontal >= .2)
            her = playerSpeed;
        else if (joystick.Horizontal <= -.2)
            her = -playerSpeed;
        else
            her = 0f;

        characterController.Move(new Vector3(her,0f,0f) * Time.deltaTime );
    }
}
