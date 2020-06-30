using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PortReceiver : MonoBehaviour
{

    public Text port_status;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Port", "None");
        FindDevicePort();
    }

    public void FindDevicePort()
    {
        // Get a list of serial port names.
        string[] ports = SerialPort.GetPortNames();

        // Display each port name to the console.
        foreach (string port in ports)
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
                    port_status.text = "Found Device.";
                    PlayerPrefs.SetString("Port", port);
                }
                else
                {
                    Debug.Log("Incorrect response message from device on port: " + port + ". Closing serialPort.");
                    port_status.text = "There are no correct port responses.";
                }
            }
            else
            {
                Debug.Log("No response from device on port: " + port + ". Closing serialPort.");
                port_status.text = "There are no port responses.";
            }
            serialPort.Close();
        }
    }
}
