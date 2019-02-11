using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlleurInventaire : MonoBehaviour {

    [SerializeField] public GameObject inventaireUI;
    [SerializeField] private ControllerDesactivation controllerDesactivation;
    [SerializeField] public bool isInventaireActive;
    [SerializeField] public bool isPommeActive;
    [SerializeField] public bool isBouteilleActive;
    [SerializeField] public int nbPommes;
    [SerializeField] public int nbBouteille;


    void Start()
    {
        controllerDesactivation = GameObject.FindGameObjectWithTag("ControlleurDesactivation").GetComponent<ControllerDesactivation>();
        isInventaireActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isInventaireActive == true)
        {
            DesactiverUIInventaire();
        }

        if (Input.GetKeyDown(KeyCode.I) && isInventaireActive == false)
        {
            ActiverUIInventaire();
        }

        else if (Input.GetKeyDown(KeyCode.I) && isInventaireActive == true)
        {
            DesactiverUIInventaire();
        }
    }

    public void ActiverUIInventaire()
    {
        inventaireUI.SetActive(true);
        controllerDesactivation.JoueurDesactiverAutre();
        isInventaireActive = true;

    }

    public void DesactiverUIInventaire()
    {
        inventaireUI.SetActive(false);
        controllerDesactivation.JoueurActiver();
        isInventaireActive = false;
    }
}