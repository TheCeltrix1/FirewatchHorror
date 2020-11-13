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
    public void CmdTorchActivate(GameObject target, bool on)
    {
        RpcTorchActivate(target, on);
    }

    [Command(ignoreAuthority = true)]
    public void CmdTorchAngle(GameObject obj)
    {
        RpcTorchAngle(obj);
    }

    [Command]
    public void CmdAnimationModelToggle(GameObject obj, int i)
    {
        RpcAnimationModelToggle(obj,i);
        RpcToggleAnimationModelClient(connectionToClient, obj, i);
    }

    [Command]
    public void CmdUpdateAnimations(GameObject obj, int i, int animation)
    {
        RpcAnimationUpdate(obj, i, animation);
        RpcToggleAnimationModelClient(connectionToClient, obj, i);
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
    public void RpcTorchActivate(GameObject target, bool on)
    {
        target.transform.GetChild(1).gameObject.SetActive(on);
    }
    
    [ClientRpc]
    public void RpcTorchAngle(GameObject obj)
    {
        obj.transform.GetChild(1).rotation = obj.transform.GetChild(0).rotation;
    }

    [ClientRpc]
    public void RpcAnimationModelToggle(GameObject obj, int i)
    {
        obj.transform.GetChild(i).gameObject.SetActive(true);
    }

    [ClientRpc]
    public void RpcAnimationUpdate(GameObject obj, int i, int animation)
    {
        Animator kms = obj.transform.GetChild(i).GetChild(0).GetComponent<Animator>();
        kms.SetInteger(animation, animation);
    }

    [TargetRpc]
    public void RpcToggleAnimationModelClient(NetworkConnection net, GameObject obj, int i)
    {
        obj.transform.GetChild(i).gameObject.SetActive(false);
    }
}