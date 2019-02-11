using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempsControlleur : MonoBehaviour {

    [SerializeField] private Light soleil;
    [SerializeField] public float secondesParJours;

    [Range(0, 1)] [SerializeField] public float heureActuelle;
    public float multiplicateurDeTemps;
    private float intensiteSoleilInitial;

    void Start()
    {
        intensiteSoleilInitial = soleil.intensity;
    }

    void Update()
    {
        UpdateSoleil();

        heureActuelle += (Time.deltaTime / secondesParJours) * multiplicateurDeTemps;

        if(heureActuelle >= 1f)
        {
            heureActuelle = 0f;
        }
    }

    void UpdateSoleil()
    {
        soleil.transform.localRotation = Quaternion.Euler((heureActuelle * 360f) - 90, 170, 0);
        float multiplicateurIntensite = 1f;

        if (heureActuelle <= 0.23f || heureActuelle >= 0.75f)
        {
            multiplicateurIntensite = 0f;
        }

        else if(heureActuelle <= 0.25f)
        {
            multiplicateurIntensite = Mathf.Clamp01((heureActuelle - 0.23f) * (1/ 0.02f));
        }

        else if (heureActuelle >= 0.73f)
        {
            multiplicateurIntensite = Mathf.Clamp01(1 - ((heureActuelle - 0.73f) * (1 / 0.02f)));
        }

        soleil.intensity = intensiteSoleilInitial * multiplicateurIntensite;

    }

}
