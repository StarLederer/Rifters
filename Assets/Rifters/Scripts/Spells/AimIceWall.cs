﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIceWall : MonoBehaviour
{
    public GameObject IcePS; //Marker that will show how the wall will behave
    public GameObject iceWall; //Ice Wall

    public LayerMask levelLayerMask; //Level Layer
    public float cooldown; //Cooldown *Default is 12*

    public bool aiming; //Indicates when the player is aiming to place the wall

    private GameObject marker; 
    private float _cooldown; //Time remaining to be able to aim again

    // Start is called before the first frame update
    void Start()
    {
        marker = Instantiate(IcePS) as GameObject;
        marker.SetActive(false);
        _cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_cooldown > 0) //If cooldown is greater than 0 you cannot aim
        {
            Debug.Log("Estamos de cooldown");
            _cooldown -= Time.deltaTime;
            return;
        }

        if (!aiming) //If the player is not aiming
        {
            if (Input.GetMouseButtonDown(1))
            {
                aiming = true;
                marker.SetActive(true);
            }
        }
        else //If the player is aiming
        {
            RaycastHit hit;

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, levelLayerMask) && Vector3.Angle(hit.normal, Vector3.up) < 10) //Checks if the ray hits the level and the normal of the level point is pointing up
            {
                marker.SetActive(true); //Enable the marker
                Quaternion rotation = Quaternion.LookRotation(transform.forward); //Orientation of the player
                marker.transform.rotation = rotation;
                marker.transform.position = hit.point + hit.normal * 0.1f;

                if (Input.GetMouseButtonUp(1)) //When you release the button
                { 
                    //--Instantiate the wall--
                    Instantiate(iceWall, marker.transform.position, rotation);

                    //--Disabling the marker--
                    aiming = false;
                    marker.SetActive(false);

                    //--Setting the cooldown--
                    _cooldown = cooldown;
                }
            }
            else //--If the ray does not hit the level or the normal of the point is not pointing upwards
            {
                marker.SetActive(false);
                
                if (Input.GetMouseButtonUp(1))
                {
                    aiming = false;
                }
            }
        }
        
    }
}
