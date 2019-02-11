using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class JoueurVitaux : MonoBehaviour {

    # region Attributs Vie
    [Header("Vie")]
    [Space(10)]
    public Slider sliderVie;
    public int maxVie;
    public float vitesseDiminutionVie;
    public float demarrageVie;
    [Space(20)]
    #endregion

    #region Attributs Stamina
    [Header("Stamina")]
    [Space(10)]
    public Slider sliderStamina;
    public int normMaxStamina;
    public float fatMaxStamina;
    private float vitesseDiminutionStamina;
    public float diminutionMultStamina;
    private float vitesseRegenerationStamina;
    public float regenerationMultStamina;
    public int minStaminaSaut;
    public bool isFalling;
    [Space(20)]
    #endregion

    # region Attributs Soif
    [Header("Soif")]
    [Space(10)]
    public Slider sliderSoif;
    public int maxSoif;
    public float vitesseDiminutionSoif;
    public float demarrageSoif;
    [Space(20)]
    #endregion

    # region Attributs Faim
    [Header("Faim")]
    [Space(10)]
    public Slider sliderFaim;
    public int maxFaim;
    public float vitesseDiminutionFaim;
    public float demarrageFaim;
    [Space(20)]
    #endregion

    # region Attributs Energie
    [Header("Energie")]
    [Space(10)]
    public Slider sliderEnergie;
    public int maxEnergie;
    public float vitesseDiminutionEnergie;
    public float demarrageEnergie;

    public bool niveauFat1 = true;
    public bool niveauFat2 = true;
    public bool niveauFat3 = true;
    [Space(20)]
    #endregion

    private CharacterController charController;
    private FirstPersonController playerController;

    void Start()
    {
        #region Sliders
        //Initialisation Vie
        sliderVie.maxValue = maxVie;
        sliderVie.value = demarrageVie;;
        

        //Initialisation Stamina
        sliderStamina.maxValue = fatMaxStamina;
        sliderStamina.value = fatMaxStamina;

        vitesseDiminutionStamina = 1;
        vitesseRegenerationStamina = 1;

        charController = GetComponent<CharacterController>();
        playerController = GetComponent<FirstPersonController>();
        

        //Initialisation Soif
        sliderSoif.maxValue = maxSoif;
        sliderSoif.value = demarrageSoif;
        

        //Initialisation Faim
        sliderFaim.maxValue = maxFaim;
        sliderFaim.value = demarrageFaim;
        

        //Initialisation Energie
        sliderEnergie.maxValue = maxEnergie;
        sliderEnergie.value = demarrageEnergie;
        #endregion
    }

    void Update()
    {
        #region Appel méthode MortPersonnage
        if (sliderVie.value <= 0)
        {
            MortPersonnage();
        }
        #endregion

        #region Controle Vie
        if (sliderFaim.value <= 0 && sliderSoif.value <= 0)
        {
            sliderVie.value -= Time.deltaTime / vitesseDiminutionVie * 2;
        }

        else if(sliderFaim.value <= 0 || sliderSoif.value <= 0)
        {
            sliderVie.value -= Time.deltaTime / vitesseDiminutionVie;
        }
        #endregion

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        #region Controle Faim

        if (sliderFaim.value >= 0)
        {
            sliderFaim.value -= Time.deltaTime / vitesseDiminutionFaim;
        }

        else if(sliderFaim.value <= 0)
        {
            sliderFaim.value = 0;
        }

        else if(sliderFaim.value >= maxFaim)
        {
            sliderFaim.value = maxFaim;
        }
        #endregion

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        #region Controle Soif

        if (sliderSoif.value >= 0)
        {
            sliderSoif.value -= Time.deltaTime / vitesseDiminutionSoif;
        }

        else if (sliderSoif.value <= 0)
        {
            sliderSoif.value = 0;
        }

        else if (sliderSoif.value >= maxSoif)
        {
            sliderSoif.value = maxSoif;
        }
        #endregion

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        #region Controle Stamina

        //Vérification pour le saut
        if (charController.isGrounded == true)
        {
            isFalling = false;
        }

        else
        {
            isFalling = true;
        }

        //Diminution de la stamina quand on cours
        if (charController.velocity.magnitude >= 0 && Input.GetKey(KeyCode.LeftShift))
        {
            sliderStamina.value -= Time.deltaTime / vitesseDiminutionStamina * diminutionMultStamina;
        }

        //Diminution de la stamina quand on saute
        else if (sliderStamina.value >= minStaminaSaut && isFalling == false && Input.GetKeyDown(KeyCode.Space) && playerController.m_JumpSpeed != 0)
        {
            sliderStamina.value -= minStaminaSaut;
        }

        //Régénération de la stamina si l'on ne cours/saute pas
        else
        {
            sliderStamina.value += Time.deltaTime / vitesseRegenerationStamina * regenerationMultStamina;
        }

        //Régénération de la stamina plus rapide si l'on ne bouge pas
        if (charController.velocity.magnitude == 0)
        {
            sliderStamina.value += Time.deltaTime / vitesseRegenerationStamina * regenerationMultStamina * 1.5f;
        }

        //Si on dépasse la stamina max elle est bloquée à la valeur max
        if (sliderStamina.value >= fatMaxStamina)
        {
            sliderStamina.value = fatMaxStamina;
        }

        //Si on dépasse la stamina min elle est bloquée à la valeur min 0 et on ne peut plus courrir
        else if (sliderStamina.value <= 0)
        {
            sliderStamina.value = 0;
            playerController.m_RunSpeed = playerController.m_WalkSpeed;
        }

        //Si la stamina est plus grande que 0, on peut courrir
        else if(sliderStamina.value >= 0)
        {
            playerController.m_RunSpeed = playerController.m_RunSpeedNorm;
        }

        //Pour le saut, si on tombe ou qu'on saute alors que alors que la stamina c'est pas assez élevée, on ne peut pas sauter
        if (isFalling == true || isFalling == true && sliderStamina.value < minStaminaSaut && Input.GetKeyDown(KeyCode.Space))
        {
            playerController.m_JumpSpeed = 0;
        }

        //Si on a assez de stamina et qu'on ne tombe pas, on peut sauter
        else if(sliderStamina.value >= minStaminaSaut && isFalling == false)
        {
            playerController.m_JumpSpeed = playerController.m_JumpSpeedNorm;
        }
        #endregion

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        #region Controle Energie
        if(sliderEnergie.value <= 60 && niveauFat1)
        {
            fatMaxStamina = 80;
            sliderStamina.value = fatMaxStamina;
            niveauFat1 = false;
        }

        else if(sliderEnergie.value <= 40 && niveauFat2)
        {
            fatMaxStamina = 60;
            sliderStamina.value = fatMaxStamina;
            niveauFat2 = false;
        }

        else if (sliderEnergie.value <= 20 && niveauFat3)
        {
            fatMaxStamina = 20;
            sliderStamina.value = fatMaxStamina;
            niveauFat3 = false;
        }

        if (sliderEnergie.value >= 0)
        {
            sliderEnergie.value -= Time.deltaTime * vitesseDiminutionEnergie;
        }

        else if (sliderEnergie.value <= 0)
        {
            sliderEnergie.value = 0;
        }

        else if(sliderEnergie.value >= maxEnergie)
        {
            sliderEnergie.value = maxEnergie;
        }
        #endregion

    }

    #region Méthodes
    void MortPersonnage()
    {

    }
    #endregion
}
