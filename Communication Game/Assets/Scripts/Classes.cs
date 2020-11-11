using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Mirror;

public class Classes : NetworkBehaviour
{
    public GameObject canvas;

    [SyncVar] public int cryptids = 1;
    public GameObject cryptidButton;
    private CryptidFeatures cryptidObject;

    [SyncVar]public int hikers = 2;
    public GameObject hikerButton;
    private HikerFeatures hikerObject;

    [SyncVar] public int rangers = 2;
    public GameObject rangerButton;
    private RangerFeatures rangerObject;

    private void OnEnable()
    {
        //canvas = GetComponentInChildren<Canvas>().gameObject;
        cryptidObject = this.GetComponent<CryptidFeatures>();
        hikerObject = this.GetComponent<HikerFeatures>();
        rangerObject = this.GetComponent<RangerFeatures>();
        GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        this.transform.GetComponentInChildren<Camera>().enabled = true;
        this.transform.GetComponentInChildren<AudioListener>().enabled = true;
    }

    private void Update()
    {
        if (cryptids <= 0) cryptidButton.SetActive(false);
        if (hikers <= 0) hikerButton.SetActive(false);
        if (rangers <= 0) rangerButton.SetActive(false);
    }

    public void CryptidSelect()
    {
        cryptids--;
        GameOn(cryptidObject);
    }

    public void HikerSelect()
    {
        hikers--;
        GameOn(hikerObject);
    }

    public void RangerSelect()
    {
        rangers--;
        GameOn(rangerObject);
    }

    public void GameOn(NetworkBehaviour obj)
    {
        canvas.SetActive(false);
        Cursor.visible = false;
        GetComponent<FirstPersonController>().enabled = true;
        obj.enabled = true;
    }
}
