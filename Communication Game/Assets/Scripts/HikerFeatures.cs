using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HikerFeatures : MonoBehaviour
{
    public GameObject lightSource;
    public GameObject torch;
    private bool _torchOn = false;
    private NetworkCommands _ntwrkcmds;
    private Camera _currentCamera;

    void OnEnable()
    {
        _currentCamera = this.transform.parent.GetChild(0).GetComponent<Camera>();
        _ntwrkcmds = this.transform.parent.GetComponent<NetworkCommands>();
        if(!torch) _ntwrkcmds.CmdSpawnTorchCommand(this.transform.position, _currentCamera.transform.rotation,this.transform.parent.gameObject, this.transform.parent.GetComponent<NetworkIdentity>().netId);
    }
    
    void Update()
    {
        if (torch) {
            torch.transform.rotation = _currentCamera.transform.rotation;
            torch.transform.position = this.transform.position + (_currentCamera.transform.forward/10);
            if (Input.GetMouseButtonDown(0))
            {
                _torchOn = !_torchOn;
                torch.SetActive(_torchOn);
            }
        }
    }

}
