using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class CamRespawn : MonoBehaviour
{
    public Transform spawnPoint;
    private SteamVR_Behaviour_Pose spawnPose = null;

    public SteamVR_Action_Boolean respawn = null;
    public GameObject rightController;

    // Start is called before the first frame update
    void Start()
    {
        spawnPose = rightController.GetComponent<SteamVR_Behaviour_Pose>();

    }

    // Update is called once per frame
    void Update()
    {
        if(respawn.GetStateDown(spawnPose.inputSource))
        {
            Respawn();
        }
    }
        private void Respawn()
    {
        transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}

