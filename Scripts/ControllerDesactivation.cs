using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class ControllerDesactivation : MonoBehaviour {

    [SerializeField] private FirstPersonController fpsControlleur;
    [SerializeField] private Image crossHair;
    [SerializeField] private Text textNomItem;
    [SerializeField] public GameObject menuUI;
    [SerializeField] public bool isMenuActive;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (fpsControlleur.enabled == true)
            {
                JoueurDesactiver();
            }

            else
            {
                JoueurActiver();
            }
            
        }

        
    }

    public void JoueurDesactiver()
    {
        fpsControlleur.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crossHair.enabled = false;
        textNomItem.enabled = false;
        menuUI.SetActive(true);
        isMenuActive = true;
    }

    public void JoueurDesactiverAutre()
    {
        fpsControlleur.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crossHair.enabled = false;
        textNomItem.enabled = false;
    }

    public void JoueurActiver()
    {
        fpsControlleur.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crossHair.enabled = true;
        textNomItem.enabled = true;
        menuUI.SetActive(false);
        isMenuActive = false;
    }

    
}
