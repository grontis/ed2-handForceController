﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Serialization;


public class SerialReader : MonoBehaviour
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
        //StartCoroutine("ReadSerial");

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

        ProcessMovement();
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
                //Todo: further handling, if needed
                Debug.Log(e);
            }
            
            //this increment will happen when current position in string is a comma
            j++;
        }
        
        //DEBUG function to log readings[] to console
        PrintReadings();
    }

    /*
     This function is currently moving the transform of the object this script is attached to
     based on values from sensor 0.
    */
    //TODO design functionality to control multiple objects. for example 1 unity gameobject per sensor
    private void ProcessMovement()
    {
        //transform.position = new Vector3((float)readings[0] / 100, transform.position.y, transform.position.z);
        if (readings[0] > 800)
        {
            transform.position = transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
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