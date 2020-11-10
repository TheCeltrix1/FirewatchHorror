using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private float _hitTimer = 1;

    private void OnEnable()
    {
        this.transform.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, _hitTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.transform.GetComponentInChildren<CryptidFeatures>())
        {
            Destroy(other.gameObject);
        }
    }
}
