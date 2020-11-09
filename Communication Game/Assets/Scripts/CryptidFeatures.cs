using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CryptidFeatures : MonoBehaviour
{
    public float attackCooldown = 2;
    public GameObject hitBox;
    private NetworkCommands _ntwrkcmds;

    void Start()
    {
        _ntwrkcmds = this.transform.parent.GetComponent<NetworkCommands>();
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                _ntwrkcmds.CmdAttack(transform.position + transform.forward, transform.rotation);
                yield return new WaitForSeconds(attackCooldown);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
