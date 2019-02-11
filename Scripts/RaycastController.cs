using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour {

    private GameObject raycastedObj;

    [Header("Raycast settings")]
    [SerializeField] private float longueurRay = 10f;
    [SerializeField] private LayerMask hitMask;

    [Header("References")]
    [SerializeField] private JoueurVitaux joueurVitaux;
    [SerializeField] private Image crossHair;
    [SerializeField] private Text textNomItem;

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, longueurRay, hitMask))
        {
            if (hit.collider.CompareTag("Consommable"))
            {
                CrosshairActive();
                raycastedObj = hit.collider.gameObject;
                ProprietesItem properties = raycastedObj.GetComponent<ProprietesItem>();
                textNomItem.text = properties.nomItem;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    properties.Interaction(joueurVitaux);
                    //raycastedObj.SetActive(false);
                }
            }

            else if (hit.collider.CompareTag("Pickup"))
            {
                CrosshairActive();
                raycastedObj = hit.collider.gameObject;
                ProprietesItem properties = raycastedObj.GetComponent<ProprietesItem>();
                textNomItem.text = properties.nomItem;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    properties.Interaction(joueurVitaux);
                    //raycastedObj.SetActive(false);
                }
            }
        }
        else
        {
            CrosshairNormal();
            textNomItem.text = null;
        }
    }

    void CrosshairActive()
    {
        crossHair.color = Color.red;
    }
    void CrosshairNormal()
    {
        crossHair.color = Color.white;
    }
}

