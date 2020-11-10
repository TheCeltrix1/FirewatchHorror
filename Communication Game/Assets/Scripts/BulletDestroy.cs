using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(this.gameObject, 2);
    }
}
