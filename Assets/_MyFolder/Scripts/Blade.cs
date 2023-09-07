using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    private bool cutting;

    public Vector3 direction { get; private set; }
    public float cutForce = 5f;
    public float minCutVelocity = 0.25f;


    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopCutting();
    }
    private void OnDisable()
    {
        StopCutting();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))    //When the butten gets pressed it will detect that the mouse is supposed to start cutting
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))     //When the butten gets pressed it will detect that the mouse is supposed to stop cutting
        {
            StopCutting();
        }
        else if (cutting)       //When the butten gets pressed it will detect that the mouse is supposed to continue cutting
        {
            ContinueCutting();
        }
    }
    private void StartCutting()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        cutting = true;
        bladeCollider.enabled = true;       //makes sure the collider is turned on
        bladeTrail.enabled = true;      // turns on motion trail
        bladeTrail.Clear();
    }
    private void StopCutting()
    {
        cutting = false;
        bladeCollider.enabled = false;      //makes sure the collider is turned off
        bladeTrail.enabled = false;     // turns off motion trail
    }
    private void ContinueCutting()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);       
        newPosition.z = 0f;

        direction = newPosition - transform.position;       //where the blade spawns to where the blade is whithin the space
        
        float velocity = direction.magnitude / Time.deltaTime;    // how fast the blade is moving
        bladeCollider.enabled = velocity > minCutVelocity;      // if velocity is bigger than the min velocity it is enabled

        transform.position = newPosition;
    }
}

