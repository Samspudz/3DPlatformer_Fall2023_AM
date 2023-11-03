using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    PlatformPlayerMovement playerCTRL;

    //---- Reference to cameras ----
    public CinemachineFreeLook platformingCam;
    public CinemachineVirtualCamera shootCam;

    void Start()
    {
        playerCTRL = FindObjectOfType<PlatformPlayerMovement>();
    }

    void Update()
    {
        if (playerCTRL.isZoomed)
        {
            platformingCam.Priority = 0;
            shootCam.Priority = 1;
        }
        else
        {
            platformingCam.Priority = 1;
            shootCam.Priority = 0;
        }
    }
}
