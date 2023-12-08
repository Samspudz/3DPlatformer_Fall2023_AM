using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerAttack : MonoBehaviour
{
    PlatformPlayerMovement playerCTRL;
    public GameObject playerAttackSphere;

    public float meleeTime = 0.2f;


    private void Awake()
    {
        playerCTRL = GetComponent<PlatformPlayerMovement>();
        playerAttackSphere.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!playerCTRL.isZoomed)
            {
                StartCoroutine(PlayerMelee());
            }
            else
            {
                StartCoroutine(PlayerShoot());
            }
        }
    }

    IEnumerator PlayerMelee()
    {
        playerAttackSphere.SetActive(true);
        yield return new WaitForSeconds(meleeTime);
        playerAttackSphere.SetActive(false);
    }

    IEnumerator PlayerShoot()
    {
        yield return new WaitForSeconds(0);
    }
}
