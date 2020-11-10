using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _timer = 5;

    private void OnEnable()
    {
        transform.GetComponent<Rigidbody>().velocity = this.transform.forward * 20;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponentInChildren<HikerFeatures>() || other.GetComponentInChildren<RangerFeatures>())
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<CryptidFeatures>().StartCoroutine("Stun");
            }
        }
        Destroy(this.gameObject);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0) Destroy(this.gameObject);
    }
}
