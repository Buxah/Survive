using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlleurSommeil : MonoBehaviour {

    [SerializeField] public GameObject dormirUI;
    [SerializeField] public Slider sliderDormir;
    [SerializeField] private Text nombreDormir;

    [SerializeField] private float regenerationHeures;
    [SerializeField] private float diminutionHeures;
    [SerializeField] private ControllerDesactivation controllerDesactivation;
    [SerializeField] public TempsControlleur tempsControlleur;

    void Start()
    {
        controllerDesactivation = GameObject.FindGameObjectWithTag("ControlleurDesactivation").GetComponent<ControllerDesactivation>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sliderDormir.value = 1;
            dormirUI.SetActive(false);
        }
    }

    public void ActiverUIDormir()
    {
        dormirUI.SetActive(true);
        controllerDesactivation.JoueurDesactiverAutre();
    }

    public void UpdateSlider()
    {
        nombreDormir.text = sliderDormir.value.ToString("0");
    }

    public void BoutonDormir(JoueurVitaux joueurVitaux)
    {
        joueurVitaux.sliderEnergie.value += sliderDormir.value * regenerationHeures;
        joueurVitaux.fatMaxStamina = joueurVitaux.sliderEnergie.value;
        joueurVitaux.sliderStamina.value = joueurVitaux.normMaxStamina;

        joueurVitaux.niveauFat1 = true;
        joueurVitaux.niveauFat2 = true;
        joueurVitaux.niveauFat3 = true;
        
        tempsControlleur.heureActuelle += (sliderDormir.value / 24f);
        joueurVitaux.sliderFaim.value -= (sliderDormir.value * diminutionHeures);
        joueurVitaux.sliderSoif.value -= (sliderDormir.value * diminutionHeures * 2.2f);

        if (joueurVitaux.sliderFaim.value <= 0 && joueurVitaux.sliderSoif.value <= 0)
        {
            joueurVitaux.sliderVie.value -= (sliderDormir.value * joueurVitaux.vitesseDiminutionVie * 2);
        }

        else if (joueurVitaux.sliderFaim.value <= 0 || joueurVitaux.sliderSoif.value <= 0)
        {
            joueurVitaux.sliderVie.value -= (sliderDormir.value * joueurVitaux.vitesseDiminutionVie);
        }


        sliderDormir.value = 1;
        

        controllerDesactivation.JoueurActiver();
        dormirUI.SetActive(false);

    }
}
