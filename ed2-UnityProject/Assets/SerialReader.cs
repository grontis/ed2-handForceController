using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class SerialReader : MonoBehaviour
{
    private SerialPort serialPort;
    private string serialMessage;

    private static int numberOfSensors = 5;
    
    /*Array for sensor readings with one sensor per finger
     [0] -> thumb
     [1] -> pointer
     [2] -> middle
     [3] -> ring
     [4] -> pinky
     */
    private int[] readings = new int[numberOfSensors];
    
    // Start is called before the first frame update
    //Used for initializations
    void Start()
    {
        //TODO: dynamically assign serial port name. different on various machines
        serialPort = new SerialPort("/dev/ttyACM0",9600, Parity.None, 8, StopBits.One);
        serialPort.Open();
        
        //clear serial in buffer at start
        serialPort.DiscardInBuffer();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            //Read serial message of values
            serialMessage = serialPort.ReadLine();
            
            //safecheck to avoid parsing empty string
            if (serialMessage.Length != 0)
            {
                ParseMessage();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    
    private void ParseMessage()
    {
        int j = 0; // this variable represents the current position in the string array
        
        //iterate for each sensor and assign values into readings[]
        for (int i = 0; i < numberOfSensors; i++)
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
                //Todo: further handling, if needed
                Debug.Log(e);
            }
            
            //this increment will happen when current position in string is a comma
            j++;
        }
        
        //DEBUG function to log readings[] to console
        PrintReadings();
    }

    private void PrintReadings()
    {
        string testPrint = "Readings: ";
        foreach (int reading in readings)
        {
            testPrint += "[" + reading + "] ";
        }
        Debug.Log(testPrint);
    }
}