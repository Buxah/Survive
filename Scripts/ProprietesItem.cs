using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProprietesItem : MonoBehaviour {

    public string nomItem;

    [Header("Vos consommables")]
    [SerializeField] private bool nourriture;
    [SerializeField] private bool eau;
    [SerializeField] private bool vie;
    [SerializeField] private bool sacCouchage;

    [SerializeField] private bool hache;
    [SerializeField] private bool flashlight;

    [SerializeField] private float valeurVie;
    [SerializeField] private float valeurEau;
    [SerializeField] private float valeurNourriture;
    [SerializeField] private ControlleurSommeil controlleurSommeil;
    [SerializeField] private ControlleurPickup controlleurPickup;

    void Start()
    {
        controlleurSommeil = GameObject.FindGameObjectWithTag("ControlleurDormir").GetComponent<ControlleurSommeil>();
        controlleurPickup = GameObject.FindGameObjectWithTag("ControlleurPickup").GetComponent<ControlleurPickup>();
    }

    public void Interaction(JoueurVitaux joueurVitaux)
    {
        if (nourriture)
        {
            joueurVitaux.sliderFaim.value += valeurNourriture;
            this.gameObject.SetActive(false);
        }

        else if (eau)
        {
            joueurVitaux.sliderSoif.value += valeurEau;
            this.gameObject.SetActive(false);
        }

        else if (vie)
        {
            joueurVitaux.sliderVie.value += valeurVie;
        }

        else if (sacCouchage)
        {
            controlleurSommeil.ActiverUIDormir();
        }

        else if (hache)
        {
            this.gameObject.SetActive(false);
            controlleurPickup.isHacheSolActive = false;
            controlleurPickup.ActiverHache();

            if (controlleurPickup.isFlashlightActive)
            {
                controlleurPickup.isFlashlightActive = false;
            }
            
        }

        else if (flashlight)
        {
            this.gameObject.SetActive(false);
            controlleurPickup.isFlashlightSolActive = false;
            controlleurPickup.ActiverFlashlight();

            if (controlleurPickup.isHacheActive)
            {
                controlleurPickup.isHacheActive = false;
            }
        }

        if (nourriture && vie)
        {
            joueurVitaux.sliderFaim.value += valeurNourriture;
            joueurVitaux.sliderVie.value += valeurVie;
            this.gameObject.SetActive(false);
        }

        if (nourriture && vie && eau)
        {
            joueurVitaux.sliderFaim.value += valeurNourriture;
            joueurVitaux.sliderVie.value += valeurVie;
            joueurVitaux.sliderSoif.value += valeurEau;
            this.gameObject.SetActive(false);
        }
    }
}

