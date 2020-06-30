using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{

    private SerialPort serialPort;
    private string serialMessage;
    private bool worked = true;

    private const int NUMBER_OF_SENSORS = 5;

    /*Array for sensor readings with one sensor per finger
        [0] -> thumb
        [1] -> pointer
        [2] -> middle
        [3] -> ring
        [4] -> pinky
    */
    public int[] cal1 = new int[NUMBER_OF_SENSORS];
    public Text instruct;
    

    public void calibration_rest()
    {

        serialPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
        serialPort.Open();

        //clear serial in buffer at start
        serialPort.DiscardInBuffer();

        //Calling calibration process

        calibrationProcess1();

    }

    // Need to repetitively call withouth update function
    void calibrationProcess1()
    {
        //Getting sensor values

        int i = 0;
        while (i < NUMBER_OF_SENSORS)
        {
            cal1[i] = GetSensorValue(i, cal1);
            PlayerPrefs.SetInt("cal1_reading" + i, cal1[i]);
            i += 1;
        }

        if (worked == true)
        {
            instruct.text = "Calibrated";
        }
        serialPort.Close();

    }

    public int GetSensorValue(int sensorId, int[] readings)
    {
        ReadFromSerial();

        try
        {
            return readings[sensorId];
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogException(e);
            return 0;
        }
    }


    private void ReadFromSerial()
    {
        //check if there is data in port buffer to read
        if (serialPort.BytesToRead != 0)
        {
            //Read serial message of values
            serialMessage = serialPort.ReadLine();

            //safecheck to avoid parsing empty string
            if (serialMessage.Length != 0)
            {
                ParseMessage();
            }
        }
    }

    private void ParseMessage()
    {
        int j = 0; // this variable represents the current position in the string array

        //iterate for each sensor and assign values into readings[]
        for (int i = 0; i < NUMBER_OF_SENSORS; i++)
        {
            string messageValue = "";

            //loop for each value between commas
            while (serialMessage[j] != ',')
            {
                messageValue += serialMessage[j];
                j++;
            }

            try
            {
                cal1[i] = int.Parse(messageValue);
            }
            catch (Exception e)
            {
                instruct.text = "Please try again";
                worked = false;
            }

            //this increment will happen when current position in string is a comma
            j++;
        }
    }
}
