using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RangerFeatures : MonoBehaviour
{
    private float _gunFireRate = 1;
    private bool _selectedObject;
    public GameObject bulletSound;
    private Camera _currentCamera;
    private NetworkCommands _ntwrkcmds;
    public GameObject torch;
    private bool _torchOn = false;

    void Start()
    {
        _selectedObject = false;
        _ntwrkcmds = this.transform.parent.GetComponent<NetworkCommands>();
        _currentCamera = this.transform.parent.GetChild(0).GetComponent<Camera>();
        _ntwrkcmds.CmdSpawnTorchCommand(this.transform.position, _currentCamera.transform.rotation, this.transform.parent.gameObject, this.transform.parent.GetComponent<NetworkIdentity>().netId);
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
                    _torchOn = !_torchOn;
                    torch.SetActive(_torchOn);
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    _ntwrkcmds.CmdSpawnBullet(new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, this.transform.position.z) + _currentCamera.transform.forward, _currentCamera.transform.rotation);
                    Shoot();
                    yield return new WaitForSeconds(_gunFireRate); 
                }
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0) _selectedObject = !_selectedObject;
        if (torch)
        {
            torch.transform.rotation = _currentCamera.transform.rotation;
            torch.transform.position = this.transform.position + (_currentCamera.transform.forward / 10);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletSound, this.transform.position, this.transform.rotation);
    }
}
