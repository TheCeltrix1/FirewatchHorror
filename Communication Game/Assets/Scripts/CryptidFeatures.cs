﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityStandardAssets.Characters.FirstPerson;

public class CryptidFeatures : NetworkBehaviour
{
    public float attackCooldown = 2;
    public GameObject hitBox;
    private NetworkCommands _ntwrkcmds;
    private bool _stunned = true;

    public void Begin()
    {
        _ntwrkcmds = this.GetComponent<NetworkCommands>();
        //this.transform.parent.GetComponent<NetworkCommands>();
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ntwrkcmds.CmdAttack(transform.position + transform.forward, transform.rotation);
                yield return new WaitForSeconds(attackCooldown);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine("Stun");
                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }
    }

    IEnumerator Stun()
    {
        this.transform.GetComponent<FirstPersonController>().m_RunSpeed /= 2;
        this.transform.GetComponent<FirstPersonController>().m_WalkSpeed /= 2;
        _stunned = !_stunned;
        yield return new WaitForSeconds(1);
        this.transform.GetComponent<FirstPersonController>().m_RunSpeed *= 2;
        this.transform.GetComponent<FirstPersonController>().m_WalkSpeed *= 2;
        _stunned = !_stunned;
        yield return null;
    }
}
