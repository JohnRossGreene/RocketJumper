using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Valve.VR;
public class Launch : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public GameObject player;
    public GameObject boostJet;
    public GameObject playerScript;
    public GameObject fuelUI;


    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    [Header("Booster")]
    public float fuelMax = 100;
    public float fuel;
    public bool readyToThrow;

    public SteamVR_Action_Boolean shootLauncher = null;
    public SteamVR_Action_Boolean useBoost = null;

    private SteamVR_Behaviour_Pose launchPose = null;



    private void Start()
    {
        fuel = fuelMax;
        launchPose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        readyToThrow = true;
        
    }
    private void Update()
    {

        if(shootLauncher.GetStateDown(launchPose.inputSource) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
        if(useBoost.GetState(launchPose.inputSource) && fuel > 0)
        {
            Boost();
        }
        if(playerScript.GetComponent<BodyCollision>().isGrounded && fuel <= fuelMax && !useBoost.GetState(launchPose.inputSource))
        {
            fuel += 1.2f;
        }
        else if(!playerScript.GetComponent<BodyCollision>().isGrounded && !useBoost.GetState(launchPose.inputSource))
        {
            fuel += .2f;
        }
        fuelUI.GetComponent<Image>().fillAmount = fuel/fuelMax;
    }
    public void Throw()
    {
        // Debug.Log("kms");
        readyToThrow = false;
        Vector3 rot = attackPoint.rotation.eulerAngles;
        rot = new Vector3(rot.x+90,rot.y,rot.z);

        GameObject projectile = Instantiate (objectToThrow, attackPoint.position, Quaternion.Euler(rot));
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceDirection = attackPoint.transform.forward;
        RaycastHit hit;
        if(Physics.Raycast(attackPoint.position, attackPoint.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        totalThrows--;
        Invoke(nameof(ResetThrow), throwCooldown);
    }
    private void ResetThrow()
    {
        readyToThrow = true;
    }
    public void Boost()
    {
        player.GetComponent<Rigidbody>().AddForce(boostJet.transform.forward*22);
        Debug.Log("BOOSTING");
        fuel= fuel-.8f;
    }
}