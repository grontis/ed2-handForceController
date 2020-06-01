using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;


public class SerialReader : MonoBehaviour
{
    private SerialPort serialPort;

    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort("COM4",9600, Parity.None, 8, StopBits.One);
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