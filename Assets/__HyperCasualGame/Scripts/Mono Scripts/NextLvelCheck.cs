using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextLvelCheck : MonoBehaviour
{
    [SerializeField] UnityEvent callWinMenu;
    [SerializeField] UnityEvent callStopPlayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // event to handel call next Mneu
            callWinMenu.Invoke();
            callStopPlayer.Invoke();
        }
    }

    
}
