using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Calibration : MonoBehaviour
{
    private const int NUMBER_OF_SENSORS = 8;

    /*Array for sensor readings with one sensor per finger
        [0] -> thumb
        [1] -> pointer
        [2] -> middle
        [3] -> ring
        [4] -> pinky
        [5] -> left of palm
        [6] -> right of palm
        [7] -> top of hand
    */
    public int[] cal = new int[NUMBER_OF_SENSORS];
    public Text instruct;
    HFController controllerInput;

    public void calibration_rest()
    {
        controllerInput = new HFController();

        //Calling calibration process
        calibrationProcess();

    }

    // Need to repetitively call withouth update function
    void calibrationProcess()
    {
        //Getting sensor values
        if (controllerInput.IsConnected)
        {
            int i = 0;
            while (i < NUMBER_OF_SENSORS)
            {
                cal[i] = controllerInput.GetSensorValue(i);
                PlayerPrefs.SetInt("cal_reading" + i, cal[i]);
                i += 1;
            }

            instruct.text = "Calibrated!";
        }
        else
        {
            instruct.text = "Please connect device before calibrating";
        }
        //serialPort.Close();
    }

    //Functions for loading other scenes

    public void load_piano_sim()
    {
        SceneManager.LoadScene("PianoSimulation", LoadSceneMode.Single);
    }

    public void load_fps_sim()
    {
        SceneManager.LoadScene("FPSLevel1", LoadSceneMode.Single);
    }

    public void load_keyboard()
    {
        SceneManager.LoadScene("Keyboard", LoadSceneMode.Single);
    }
}
