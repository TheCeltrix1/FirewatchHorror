using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HikerFeatures : NetworkBehaviour
{
    public GameObject torch;
    private bool _torchToggle = false;
    private NetworkCommands _ntwrkcmds;
    private Camera _currentCamera;

    void OnEnable()
    {
        _ntwrkcmds = this.GetComponent<NetworkCommands>();
        _currentCamera = this.transform.GetChild(0).GetComponent<Camera>();
        //this.transform.parent.GetComponent<NetworkCommands>();
        if (!torch) _ntwrkcmds.CmdSpawnTorchCommand(this.transform.position, _currentCamera.transform.rotation,this.transform.gameObject, this.transform.GetComponent<NetworkIdentity>().netId,this.gameObject);
    }
    
    void Update()
    {
        _torchToggle = false;
        if (torch) {
            if (Input.GetMouseButtonDown(0))
            {
                _torchToggle = true;
            }
            _ntwrkcmds.CmdUpdateTorchLocation(this.gameObject, this.transform.position + (_currentCamera.transform.forward / 10), _currentCamera.transform.rotation, _torchToggle);
        }
    }

}
