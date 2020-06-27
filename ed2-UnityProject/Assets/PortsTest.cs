using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class PortsTest : MonoBehaviour
{
    private bool isDeviceConnected = false;
    
    // Start is called before the first frame update
    void Start()
    {
        FindDevicePort();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeviceConnected)
        {
            //Serial port reading logic can now take place because the correct port is opened
        }
    }


    private void FindDevicePort()
    {
        // Get a list of serial port names.
        string[] ports = SerialPort.GetPortNames();

        // Display each port name to the console.
        foreach(string port in ports)
        {
            SerialPort serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);
            serialPort.Open();
            
            serialPort.WriteLine("WakeUp");
            
            //Thread to wait for response for 250ms
            Thread.Sleep(250);

            if (serialPort.BytesToRead != 0)
            {
                string response = serialPort.ReadLine();

                if (response == "ArduinoUno")
                {
                    Debug.Log("Response message received. Connected to device on port: " + port);
                    isDeviceConnected = true;
                }
                else
                {
                    Debug.Log("Incorrect response message from device on port: " + port + ". Closing serialPort.");
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
}
