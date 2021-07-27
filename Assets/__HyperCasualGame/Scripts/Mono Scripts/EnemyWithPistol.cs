
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithPistol : MonoBehaviour
{
    [SerializeField] GameContrllerData GCD;
    [SerializeField] Transform[] points;
    [SerializeField] float enemySpeed;

    [SerializeField] GameObject particlesEffect;
    [SerializeField] Transform particlesEffectLocation;
    [SerializeField] float smooth ;

    private Quaternion targetRotation;

    int index;

    bool entred = false;


    private Animator animator;
    private CharacterController character;

    Vector3 direction;

    void Start()
    {
        index = 0;

        animator = GetComponentInChildren<Animator>();
        character = GetComponentInChildren<CharacterController>();
    }

    void Update()
    {
        if (!GCD.isGameLose && !GCD.isGameWin)
            EnemyPatrol();

    }

    private void EnemyPatrol()
    {
        if (character.transform.position != points[index].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[index].position, enemySpeed * Time.deltaTime);
            transform.LookAt(points[index].position, Vector3.up);
            animator.SetBool("isEnemyWalking", true);
        }
        else if (!entred)
        {

            targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smooth * Time.deltaTime);

            entred = true;
            StartCoroutine(wait());
        }
    }

    public void attackPlayer()
    {
        animator.SetTrigger("isShooting");
        FindObjectOfType<AudioManager>().Play("Gun");
        GameObject effect = Instantiate(particlesEffect, particlesEffectLocation.position, particlesEffectLocation.rotation);
        Destroy(effect, 0.4f);

    }

    IEnumerator wait()
    {


        yield return new WaitForSeconds(.25f);
        index = (index + 1) % points.Length;
        entred = false;
    }

}
