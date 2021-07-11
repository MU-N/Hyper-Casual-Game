using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithPistol : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float enemySpeed;

    int index; 
    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        index = 0;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
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
}
