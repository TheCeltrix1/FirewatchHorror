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

    public void Begin()
    {
        _ntwrkcmds = this.GetComponent<NetworkCommands>();
        _currentCamera = this.transform.GetChild(0).GetComponent<Camera>();
    }
    
    void Update()
    {
        if (torch && _ntwrkcmds) {
            if (Input.GetMouseButtonDown(0))
            {
                _torchToggle = !_torchToggle;
                _ntwrkcmds.CmdTorchActivate(this.gameObject,_torchToggle);
            }
            _ntwrkcmds.CmdTorchAngle(this.gameObject);
        }
    }
}
