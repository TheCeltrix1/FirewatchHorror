using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private float _hitTimer = 0.5f;
    public GameObject source;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.transform.GetComponentInChildren<CryptidFeatures>())
        {
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        _hitTimer -= Time.deltaTime;
        if (_hitTimer <= 0) Destroy(this.gameObject);
    }
}
