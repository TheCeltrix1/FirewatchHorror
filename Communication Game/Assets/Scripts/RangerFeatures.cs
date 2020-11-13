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

    public void Begin()
    {
        _selectedObject = false;
        _ntwrkcmds = this.GetComponent<NetworkCommands>();
        _currentCamera = this.transform.GetChild(0).GetComponent<Camera>();
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_selectedObject)
                {
                    _torchToggle = !_torchToggle;
                    _ntwrkcmds.CmdTorchActivate(this.gameObject,_torchToggle);
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    _ntwrkcmds.CmdSpawnBullet(new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, this.transform.position.z) + _currentCamera.transform.forward, _currentCamera.transform.rotation);
                    Shoot();
                    yield return new WaitForSeconds(_gunFireRate); 
                }
            }
            //this.transform.GetChild(1).rotation = this.transform.GetChild(0).rotation;
            _ntwrkcmds.CmdTorchAngle(this.gameObject);
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
