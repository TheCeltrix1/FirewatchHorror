using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;
using UnityStandardAssets.Characters.FirstPerson;

public class CharacterStats : MonoBehaviour
{
    public float characterHeight;
    public float walkSpeed;
    public float runSpeed;
    public AudioClip[] stepSound;

    private FirstPersonController _fpsController;

    private void OnEnable()
    {
        this.transform.parent.GetComponent<CharacterController>().height = characterHeight;
        _fpsController = this.transform.parent.GetComponent<FirstPersonController>();
        _fpsController.m_WalkSpeed = walkSpeed;
        _fpsController.m_RunSpeed = runSpeed;
        //if (stepSound != null) _fpsController.m_FootstepSounds = stepSound;
    }

    void Update()
    {
        
    }
}
