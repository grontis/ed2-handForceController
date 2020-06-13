using System;
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
    
    //Cube game objects currently being used for movement testing
    //It is assumed that these are child objects of the GameObject this script is a component of
    public GameObject cube0;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    
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
        
        
        //Find test cube game Objects in hierarchy
        cube0 = transform.Find("0").gameObject;
        cube1 = transform.Find("1").gameObject;
        cube2 = transform.Find("2").gameObject;
        cube3 = transform.Find("3").gameObject;
        cube4 = transform.Find("4").gameObject;
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
     This function currently moves the 5 cube gameObjects that are children to the gameobject
     in which this script is a component of. A cube simply moves in the x direction if the coresponding
     sensor value is above 800.
    */
    private void ProcessMovement()
    {
        if (readings[0] > 800)
        {
            cube0.transform.position = cube0.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
        if (readings[1] > 800)
        {
            cube1.transform.position = cube1.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
        if (readings[2] > 800)
        {
            cube2.transform.position = cube2.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
        if (readings[3] > 800)
        {
            cube3.transform.position = cube3.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
        if (readings[4] > 800)
        {
            cube4.transform.position = cube4.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
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