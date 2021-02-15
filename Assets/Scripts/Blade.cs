using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject bladeTrailPrefab;
    public float minCuttingVelocity = .001f;
    private Vector2 previousPosition;
    private bool isCutting = false;
    private Rigidbody2D rb;
    private Camera cam;
    private GameObject currentBladeTrail;
    private CircleCollider2D circleCollider;
    
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
    }

    void UpdateCut()
    {
        Vector2 newPosition =cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude*Time.deltaTime;
        if (velocity > minCuttingVelocity) //если скорость достаточно велика
        {
            circleCollider.enabled = true;
        }
        else
        {
            circleCollider.enabled = false;
        }

        previousPosition = newPosition;
    }

    void StartCutting()
    {
        isCutting = true;
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        circleCollider.enabled = false; 
        //чтобы коллайдер не включался при первом касании, у нас есть проверка скорости для этого
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void StopCutting()
    {
        isCutting = false;
        currentBladeTrail.transform.SetParent(null); 
        Destroy(currentBladeTrail,2f);
        circleCollider.enabled = false;
    }
}
