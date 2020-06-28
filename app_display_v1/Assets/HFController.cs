using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class HFController
{
    private SerialPort serialPort;
    private string serialMessage;

    private const int NUMBER_OF_SENSORS = 5;

    /*Array for sensor readings with one sensor per finger
        [0] -> thumb
        [1] -> pointer
        [2] -> middle
        [3] -> ring
        [4] -> pinky
    */
    private int[] readings = new int[NUMBER_OF_SENSORS];

    public HFController()
    {
        //TODO: dynamically assign serial port name. different on various machines
        serialPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
        serialPort.Open();

        //clear serial in buffer at start
        serialPort.DiscardInBuffer();
    }

    public int GetSensorValue(int sensorId)
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
                readings[i] = int.Parse(messageValue);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            //this increment will happen when current position in string is a comma
            j++;
        }
    }

    /*
     * Debug function used to print readings. 
     */
    public void PrintReadings()
    {
        string testPrint = "Readings: ";
        foreach (int reading in readings)
        {
            testPrint += "[" + reading + "] ";
        }
        Debug.Log(testPrint);
    }
}
