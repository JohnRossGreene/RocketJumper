using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class hiddenController : MonoBehaviour
{
    public GameObject launcher;
    public SteamVR_Action_Boolean shootLauncher = null;
    public SteamVR_Action_Boolean useBoost = null;
    private SteamVR_Behaviour_Pose launchPose = null;
    // Start is called before the first frame update
    void Start()
    {
        launchPose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shootLauncher.GetStateDown(launchPose.inputSource) && launcher.GetComponent<Launch>().readyToThrow && launcher.GetComponent<Launch>().totalThrows > 0)
        {
            launcher.GetComponent<Launch>().Throw();
        }
        if(useBoost.GetState(launchPose.inputSource) && launcher.GetComponent<Launch>().fuel > 0)
        {
            launcher.GetComponent<Launch>().Boost();
        }
    }
}
