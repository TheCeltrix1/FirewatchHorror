using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkCommands : NetworkBehaviour
{
    private HikerFeatures _hikerFeat;
    private RangerFeatures _rangerFeat;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        this.transform.GetComponentInChildren<Camera>().enabled = true;
        this.transform.GetComponentInChildren<AudioListener>().enabled = true;
    }

    void OnEnable()
    {
        _hikerFeat = this.transform.GetComponentInChildren<HikerFeatures>(true);
        _rangerFeat = this.transform.GetComponentInChildren<RangerFeatures>(true);

    }

    [Command]
    public void CmdSpawnTorchCommand(Vector3 position, Quaternion rotation, GameObject parent, uint id)
    {
        RpcTorchSpawn(position, rotation, parent, id);
    }

    [Command]
    public void CmdSpawnBullet(Vector3 pos, Quaternion rotation)
    {
        RpcBulletSpawn(pos, rotation);
    }

    [Command]
    public void CmdAttack(Vector3 pos, Quaternion rot)
    {
        RpcMeleeAttack(pos, rot);
    }

    [ClientRpc]
    public void RpcTorchSpawn(Vector3 position, Quaternion rotation, GameObject parent, uint id)
    {
        GameObject torch = Instantiate(NetworkManager.singleton.spawnPrefabs[2], position + parent.transform.forward, rotation);
        NetworkServer.Spawn(torch, connectionToClient);
        if (id == netId)
        {
            if (this.transform.GetComponentInChildren<HikerFeatures>())
            {
                _hikerFeat.torch = torch;
            }
            else if (this.transform.GetComponentInChildren<RangerFeatures>())
            {
                _rangerFeat.torch = torch;
            }
        }
        torch.SetActive(false);
    }

    [ClientRpc]
    public void RpcBulletSpawn(Vector3 pos, Quaternion rotation)
    {
        GameObject bullet = Instantiate(NetworkManager.singleton.spawnPrefabs[0], pos, rotation);
        NetworkServer.Spawn(bullet, connectionToClient);
    }

    [ClientRpc]
    public void RpcMeleeAttack(Vector3 pos, Quaternion rot)
    {
        GameObject nya = Instantiate(NetworkManager.singleton.spawnPrefabs[1], pos, rot);
        NetworkServer.Spawn(nya, connectionToClient);
    }
}