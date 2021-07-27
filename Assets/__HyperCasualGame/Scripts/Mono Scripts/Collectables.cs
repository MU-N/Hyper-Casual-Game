
using UnityEngine;

public class Collectables : MonoBehaviour
{

    // Key Collect Scripte
    [SerializeField] GameContrllerData GCD;
    [SerializeField] ParticleSystem portalEnd;
    private void Awake()
    {
        portalEnd.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag =="Player")
        {
            gameObject.SetActive(false);
            GCD.hasTheKey = true;
            portalEnd.Play();
            FindObjectOfType<AudioManager>().Play("Key");

        }
    }
}
