﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpells : MonoBehaviour
{
    // Other variables

    // Editor variables

    // Public variables
    public float minAngle = 20;
    public float throwingForce = 10;
    public float raycastDistance = 6;

    public Camera cam;

    public bool hasObject = false;

    public GameObject SpellPanel;
    public Transform ObjectHolder;
    public LayerMask InteractibleLayer;

    //[HideInInspector]
    public List<InteractibleLevel> inRangeObjects = new List<InteractibleLevel>();

    // Private variables
    private InteractibleLevel activableObject;

    //--------------------------
    // MonoBehaviour events
    //--------------------------
    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawLine(cam.transform.position, cam.transform.position + cam.transform.forward * raycastDistance, Color.red);
        if (!hasObject)
        {
            activableObject = null;

            //If the player is looking that object
            for (int i = 0; i < inRangeObjects.Count; i++)
            {
                Vector3 dir = (inRangeObjects[i].transform.position - cam.transform.position).normalized;
                Debug.DrawLine(inRangeObjects[i].transform.position, cam.transform.position);

                float angle = Vector3.Angle(dir, cam.transform.forward);

                bool lookingAt = false;
                if(Physics.Raycast(cam.transform.position, cam.transform.forward, raycastDistance, InteractibleLayer))
                {
                    lookingAt = true;
                }
                if (angle < minAngle || lookingAt)
                {
                    activableObject = inRangeObjects[i];
                    break;
                }
            }

            if (activableObject != null)
            {
                if(activableObject.typeOfObject != InteractibleLevel.TypeOfInteractableObject.SCALE || !activableObject.activated)
                {
                    SpellPanel.SetActive(true); 
                }
                else
                {
                    SpellPanel.SetActive(false);
                }
                
            }
            else
            {
                SpellPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(activableObject != null)
            {
                activableObject.ActivateObject(this);
            }
        }
    }

    //--------------------------
    // PlayerSpells events
    //--------------------------
}
