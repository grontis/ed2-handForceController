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
    
    public int[] cal = {140,140,140,140,140,140,120,440};

    public HFController controllerInput;
    public GameObject controllerAlertPanel;
    
    

    private void Start()
    {
        //If calibration is saved, new calibration is defined from PlayerPrefs
        if (PlayerPrefs.HasKey("cal_reading7"))
        {
            for(int i = 0; i < 8; i++)
            {
                cal[i] = PlayerPrefs.GetInt("cal_reading" + i);
            }
            Debug.Log("Using latest calibration settings");
        }
        else
        {
            Debug.Log("Could not find saved calibration settings");
        }
        
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
