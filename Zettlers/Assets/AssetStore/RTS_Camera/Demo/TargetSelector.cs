﻿using UnityEngine;
using System.Collections;
using RTS_Cam;

[RequireComponent(typeof(RTS_Camera))]
public class TargetSelector : MonoBehaviour 
{
    private RTS_Camera cam;
    private Camera _camera;
    public string targetsTag;

    private void Start()
    {
        cam = gameObject.GetComponent<RTS_Camera>();
        _camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(targetsTag))
                    cam.SetTarget(hit.transform);
                else
                    cam.ResetTarget();
            }
        }
    }
}
