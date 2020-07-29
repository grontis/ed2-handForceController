using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HFConfig : MonoBehaviour
{
    public int jump;
    public int fire;

    public int moveForward;
    public int moveBackward;
    public int moveLeft;
    public int moveRight;
    public int rotateLeft;
    public int rotateRight;

    public HFController controllerInput;
    public GameObject controllerAlertPanel;

    private void Start()
    {
        controllerInput = new HFController();
    }

    private void Update()
    {
        if (controllerInput.IsConnected)
        {
            //Actions for object/class to take when connected
            controllerAlertPanel.SetActive(false);
        }
        else
        {
            controllerAlertPanel.SetActive(true);
            controllerInput.ReconnectDevice();
        }
    }

}
