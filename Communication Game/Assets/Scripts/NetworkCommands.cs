using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkCommands : NetworkBehaviour
{
    private HikerFeatures _hikerFeat;
    private RangerFeatures _rangerFeat;

    void OnEnable()
    {
        _hikerFeat = this.transform.GetComponentInChildren<HikerFeatures>(true);
        _rangerFeat = this.transform.GetComponentInChildren<RangerFeatures>(true);

    }

    [Command(ignoreAuthority = true)]
    public void CmdSpawnTorchCommand(Vector3 position, Quaternion rotation, GameObject parent, uint id, GameObject gam)
    {
        RpcTorchSpawn(position, rotation, parent, id, gam);
    }

    [Command(ignoreAuthority = true)]
    public void CmdSpawnBullet(Vector3 pos, Quaternion rotation)
    {
        RpcBulletSpawn(pos, rotation);
    }

    [Command(ignoreAuthority = true)]
    public void CmdAttack(Vector3 pos, Quaternion rot)
    {
        RpcMeleeAttack(pos, rot);
    }

    [Command(ignoreAuthority = true)]
    public void CmdUpdateTorchLocation(GameObject torch, Vector3 pos, Quaternion rot, bool toggle)
    {
        RpcUpdateTorchLocation(torch,pos,rot,toggle);
    }

    [ClientRpc]
    public void RpcTorchSpawn(Vector3 position, Quaternion rotation, GameObject parent, uint id, GameObject dis)
    {
        GameObject torch = Instantiate(NetworkManager.singleton.spawnPrefabs[2], position + parent.transform.forward, rotation);
        if (id == netId)
        {
            /*if (dis.GetComponent<HikerFeatures>())
            {*/
                dis.GetComponent<HikerFeatures>().torch = torch;
            /*}
            else if (dis.GetComponent<RangerFeatures>())
            {*/
                dis.GetComponent<RangerFeatures>().torch = torch;
            //}
        }
        torch.SetActive(false);
    }

    [ClientRpc]
    public void RpcBulletSpawn(Vector3 pos, Quaternion rotation)
    {
        GameObject bullet = Instantiate(NetworkManager.singleton.spawnPrefabs[0], pos, rotation);
        //NetworkServer.Spawn(bullet, connectionToClient);
    }

    [ClientRpc]
    public void RpcMeleeAttack(Vector3 pos, Quaternion rot)
    {
        GameObject nya = Instantiate(NetworkManager.singleton.spawnPrefabs[1], pos, rot);
        //NetworkServer.Spawn(nya, connectionToClient);
    }
    
    [ClientRpc]
    public void RpcUpdateTorchLocation(GameObject torch, Vector3 pos, Quaternion rot, bool toggle)
    {
        if (torch.GetComponent<HikerFeatures>().torch) {
            GameObject hik = torch.GetComponent<HikerFeatures>().torch;
            hik.transform.position = pos;
            hik.transform.rotation = rot;
            if (toggle) {
                hik.SetActive(!hik.activeInHierarchy);
            }
        }
    }

}