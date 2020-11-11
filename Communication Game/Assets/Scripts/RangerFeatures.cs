using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RangerFeatures : NetworkBehaviour
{
    private float _gunFireRate = 1;
    private bool _selectedObject;
    public GameObject bulletSound;
    private Camera _currentCamera;
    private NetworkCommands _ntwrkcmds;
    public GameObject torch;
    private bool _torchToggle = false;

    void OnEnable()
    {
        _selectedObject = false;
        _ntwrkcmds = this.GetComponent<NetworkCommands>();
        //this.transform.parent.GetComponent<NetworkCommands>();
        _currentCamera = this.transform.GetChild(0).GetComponent<Camera>();
        _ntwrkcmds.CmdSpawnTorchCommand(this.transform.position, _currentCamera.transform.rotation, this.gameObject, this.transform.GetComponent<NetworkIdentity>().netId,this.gameObject);
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        while (true)
        {
            _torchToggle = false;
            if (Input.GetMouseButtonDown(0))
            {
                if (_selectedObject)
                {
                    _torchToggle = true;
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    _ntwrkcmds.CmdSpawnBullet(new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, this.transform.position.z) + _currentCamera.transform.forward, _currentCamera.transform.rotation);
                    Shoot();
                    yield return new WaitForSeconds(_gunFireRate); 
                }
            }
            if(torch) _ntwrkcmds.CmdUpdateTorchLocation(this.gameObject, this.transform.position + (_currentCamera.transform.forward / 10), _currentCamera.transform.rotation, _torchToggle);
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0) _selectedObject = !_selectedObject;
    }

    private void Shoot()
    {
        Instantiate(bulletSound, this.transform.position, this.transform.rotation);
    }
}
