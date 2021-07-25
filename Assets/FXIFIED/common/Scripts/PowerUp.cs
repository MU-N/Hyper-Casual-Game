using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameContrllerData GCD;
    [SerializeField] int maxCoinValue;
    [SerializeField] int minCoinValue;
    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
            GCD.Score += Random.Range(maxCoinValue, minCoinValue);

        }
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);


        Destroy(gameObject);
    }
}
