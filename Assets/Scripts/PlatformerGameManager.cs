using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGameManager : MonoBehaviour
{
    PlatformPlayerMovement playerCTRL;
    public GameObject crosshairs;

    private void Awake()
    {
        playerCTRL = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformPlayerMovement>();
        crosshairs.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (playerCTRL.isZoomed)
        {
            crosshairs.SetActive(true);
        }
        else
        {
            crosshairs.SetActive(false);
        }
    }
}
