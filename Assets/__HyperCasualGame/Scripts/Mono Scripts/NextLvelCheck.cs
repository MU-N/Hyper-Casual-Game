using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextLvelCheck : MonoBehaviour
{
    [SerializeField] GameContrllerData GCD;
    [SerializeField] UnityEvent callWinMenu;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GCD.hasTheKey)
        {
            // event to handel call next Mneu
            callWinMenu.Invoke();
            GCD.isGameWin = true;
            FindObjectOfType<AudioManager>().Play("Next");
        }
    }

    
}
