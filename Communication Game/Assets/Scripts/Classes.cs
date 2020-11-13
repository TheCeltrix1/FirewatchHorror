using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Mirror;

public class Classes : NetworkBehaviour
{
    public GameObject canvas;
    private NetworkCommands _ntwrkcmds;
    private int _iNeededAVariable;

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
        _ntwrkcmds = this.GetComponent<NetworkCommands>();
        cryptidObject = this.GetComponent<CryptidFeatures>();
        hikerObject = this.GetComponent<HikerFeatures>();
        rangerObject = this.GetComponent<RangerFeatures>();
        GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
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
        _iNeededAVariable = 4;
        GameOn();
        cryptidObject.Begin();
    }

    public void HikerSelect()
    {
        hikers--;
        _iNeededAVariable = 2;
        GameOn();
        hikerObject.Begin();
    }

    public void RangerSelect()
    {
        rangers--;
        _iNeededAVariable = 3;
        GameOn();
        rangerObject.Begin();
    }

    public void GameOn()
    {
        _ntwrkcmds.CmdAnimationModelToggle(this.gameObject,_iNeededAVariable);
        canvas.SetActive(false);
        Cursor.visible = false;
        GetComponent<FirstPersonController>().enabled = true;
        GetComponent<FirstPersonController>().animationModel = _iNeededAVariable;
    }
}
