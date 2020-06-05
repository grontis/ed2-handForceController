using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;


public class SerialReader : MonoBehaviour
{
    private SerialPort serialPort;

    private byte[] readings = new byte[5];

    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort("/dev/ttyACM0",9600, Parity.None, 8, StopBits.One);
        serialPort.Open();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            string message = serialPort.ReadLine();
            Debug.Log(message);
        }
        catch (TimeoutException e)
        {
            Debug.Log(e);
        }
    }
}