using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WinScript : NetworkBehaviour
{
    [SyncVar] public int joinedPlayer;
    [SyncVar] public int presentPlayers;

    public void PlayerJoined()
    {
        joinedPlayer++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (joinedPlayer > 1) {
            if (other.tag == "Player" && !other.transform.GetComponentInChildren<CryptidFeatures>())
            {
                presentPlayers++;
            }
        }
    }

    private void Update()
    {
        if (joinedPlayer > 1)
        {
            if (joinedPlayer-1 >= presentPlayers)
            {
                Debug.Log("Escaped");
            }
        }
    }
}