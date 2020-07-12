using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class HFController
{
    private SerialPort serialPort;
    private string serialMessage;

    private bool isConnected = false;
    private string[] portNames;

    private const int NUMBER_OF_SENSORS = 5;

    private bool debugMode = false;

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
        FindDevicePort();
    }

    public HFController(bool debugMode)
    {
        this.debugMode = debugMode;

        FindDevicePort();
    }

    public void ReconnectDevice()
    {
        FindDevicePort();
    }

    private void FindDevicePort()
    {
        // Get a list of serial port names.
        portNames = SerialPort.GetPortNames();

        try
        {
            // Display each port name to the console.
            foreach (string port in portNames)
            {
                serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
                serialPort.Open();
                serialPort.DiscardInBuffer();

                serialPort.WriteLine("WakeUp");

                //Thread to wait for response in ms
                Thread.Sleep(100);

                if (serialPort.BytesToRead != 0)
                {
                    string response = serialPort.ReadLine();

                    if (response == "ArduinoUno")
                    {
                        Debug.Log("Response message received. Connected to device on port: " + port);
                        isConnected = true;
                        serialPort.DiscardInBuffer();
                        Thread.Sleep(50); //sleep thread to make sure there will be data coming from arduino
                    }
                    else
                    {
                        Debug.Log("Incorrect response message from device on port: " + port + ". Closing serialPort. Message: " + response);
                        serialPort.Close();
                    }
                }
                else
                {
                    Debug.Log("No response from device on port: " + port + ". Closing serialPort.");
                    serialPort.Close();
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public string[] PortNames
    {
        get => portNames;
    }

    public bool IsConnected
    {
        get => isConnected;
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
        try
        {
            //check if there is data in port buffer to read
            if (serialPort.BytesToRead != 0)
            {
                //Read serial message of values
                serialMessage = serialPort.ReadLine();

                serialPort.WriteLine("reading");

                //safecheck to avoid parsing empty string
                if (serialMessage.Length != 0)
                {
                    ParseMessage();
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            isConnected = false;
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