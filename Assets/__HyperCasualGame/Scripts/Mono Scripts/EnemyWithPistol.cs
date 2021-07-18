using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithPistol : MonoBehaviour
{
    [SerializeField] GameContrllerData GCD;
    [SerializeField] Transform[] points;
    [SerializeField] float enemySpeed;
    [SerializeField] float attackRange;

    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] Transform tagetRayCastObject;


    [SerializeField] GameObject particlesEffect;
    [SerializeField] Transform particlesEffectLocation;


    int index;

    private bool isTouchingPlayer;

    private Animator animator;


    void Start()
    {
        index = 0;

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!GCD.isGameLose&&!GCD.isGameWin)
            EnemyPatrol();

    }

    private void EnemyPatrol()
    {
        if (transform.position != points[index].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[index].position, enemySpeed * Time.deltaTime);
            transform.LookAt(points[index].position, Vector3.up);
            animator.SetBool("isEnemyWalking", true);
        }
        else
        {
            index = (index + 1) % points.Length;
        }
    }

    public void attackPlayer()
    {
        animator.SetTrigger("isShooting");
        GameObject effect =  Instantiate(particlesEffect, particlesEffectLocation.position, particlesEffectLocation.rotation);
        Destroy(effect, 0.4f);

    }

}
