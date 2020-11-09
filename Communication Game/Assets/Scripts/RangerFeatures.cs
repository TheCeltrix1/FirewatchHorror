using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RangerFeatures : MonoBehaviour
{
    private float _gunFireRate = 1;
    public GameObject bullet;
    private Camera _currentCamera;
    private NetworkCommands _ntwrkcmds;

    void Start()
    {
        _ntwrkcmds = this.transform.parent.GetComponent<NetworkCommands>();
        StartCoroutine("Fire");
        _currentCamera = this.transform.parent.GetChild(0).GetComponent<Camera>();
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Shoot();
                _ntwrkcmds.CmdSpawnBullet(new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, this.transform.position.z) + _currentCamera.transform.forward, _currentCamera.transform.rotation);
                yield return new WaitForSeconds(_gunFireRate);
            }
            yield return null;
        }
    }

    private void Shoot()
    {
        GameObject pewpew = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y-0.25f, this.transform.position.z) + _currentCamera.transform.forward, _currentCamera.transform.rotation);
    }
}
