using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private RigidbodyFirstPersonController firstPersonController;
    [FormerlySerializedAs("zoomedIn")] [SerializeField] private float zoomedInFOV = 20f;
    [FormerlySerializedAs("zoomedOut")] [SerializeField] private float zoomedOutFOV = 60f;
    [SerializeField] private float zoomedInSensitivity = 1;
    [SerializeField] private float zoomedOutSensitivity = 2;

    private bool isZoomedIn = false;

    private void OnDisable()
    {
        ZoomOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isZoomedIn)
            {
                ZoomOut();
            }
            else
            {
                ZoomIn();
            }
        }
    }

    private void ZoomIn()
    {
        isZoomedIn = true;
        cam.fieldOfView = zoomedInFOV;
        firstPersonController.mouseLook.XSensitivity = zoomedInSensitivity;
        firstPersonController.mouseLook.YSensitivity = zoomedInSensitivity;
    }

    private void ZoomOut()
    {
        isZoomedIn = false;
        cam.fieldOfView = zoomedOutFOV;
        firstPersonController.mouseLook.XSensitivity = zoomedOutSensitivity;
        firstPersonController.mouseLook.YSensitivity = zoomedOutSensitivity;
    }
}
