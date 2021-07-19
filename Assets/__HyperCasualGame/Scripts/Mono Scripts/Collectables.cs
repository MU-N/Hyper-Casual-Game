using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] GameContrllerData GCD;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag =="Player")
        {
            gameObject.SetActive(false);
            GCD.Score++;
            
        }
    }
}
