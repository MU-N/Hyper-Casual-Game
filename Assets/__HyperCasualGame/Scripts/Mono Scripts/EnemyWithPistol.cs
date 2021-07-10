using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithPistol : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }
}
