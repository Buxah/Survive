using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlleurPickup : MonoBehaviour {

    [SerializeField] public GameObject hache;
    [SerializeField] public GameObject hacheSol;
    [SerializeField] public GameObject flashlight;
    [SerializeField] public GameObject flashlightSol;
    [SerializeField] public GameObject flash;
    [SerializeField] public bool isHacheActive;
    [SerializeField] public bool isHacheSolActive;
    [SerializeField] public bool isFlashlightActive;
    [SerializeField] public bool isFlashlightSolActive;
    [SerializeField] public bool isFlashActive;

    void Start()
    {
        isHacheSolActive = true;
        isFlashlightSolActive = true;
        isFlashActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isHacheSolActive == false)
        {
            isHacheActive = true;
            isFlashlightActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && isFlashlightSolActive == false)
        {
            isHacheActive = false;
            isFlashlightActive = true;
        }

        

        if (Input.GetKeyDown(KeyCode.F) && isFlashlightActive && isFlashActive == false)
        {
            flash.SetActive(true);
            isFlashActive = true;
        }

        else if (Input.GetKeyDown(KeyCode.F) && isFlashlightActive && isFlashActive == true)
        {
            flash.SetActive(false);
            isFlashActive = false;
        }

        if (isHacheActive == false)
        {
            hache.SetActive(false);
        }

        else if (isHacheActive == true)
        {
            hache.SetActive(true);
        }

        if (isFlashlightActive == false)
        {
            flashlight.SetActive(false);
        }

        else if (isFlashlightActive == true)
        {
            flashlight.SetActive(true);
        }
    }

    public void ActiverHache()
    {
        hache.SetActive(true);
        isHacheActive = true;
    }

    public void ActiverFlashlight()
    {
        flashlight.SetActive(true);
        isFlashlightActive = true;
    }

}
