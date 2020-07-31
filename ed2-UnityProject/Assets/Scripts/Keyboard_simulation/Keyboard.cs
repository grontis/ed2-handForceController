using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IronPython.Hosting;
//using System.Windows.Forms;
//using Microsoft.VisualBasic.Devices;

public class Keyboard : MonoBehaviour
{
    private HFController controllerInput;
    //These are for the TypeLetter that types in the UI Text Area
    private int buttonSize = 5;// for 30 combinations
    private string[] HT = new string[] { "Hold", "Hold", "Hold", "Hold", "Hold" };//default value
    public UnityEngine.UI.Button[] HTbutton = new UnityEngine.UI.Button[5]; // for hold/toggle buttons
    //public Button[] buttons; // combinations = {"Finger1", "Finger2", "Finger3", "Finger4", "Finger5", "Finger1&2", "Finger2&3", "Finger3&4", "Finger4&5", "Finger1&3", "Finger2&4", "Finger3&5", "Finger1&4", "Finger2&5", "Finger1&5", "Finger1&2&3", "Finger2&3&4", "Finger3&4&5", "Finger1&2&4", "Finger2&3&5", "Finger1&2&5", "Finger1&3&4", "Finger2&4&5", "Finger1&3&5", "Finger1&4&5", "Finger1&2&3&4", "Finger2&3&4&5", "Finger1&2&3&5", "Finger1&2&4&5", "Finger1&3&4&5", "Finger1&2&3&4&5"};
    public Text txt;
    public InputField[] Inputcmd = new InputField[5];
    private bool checkCmd = false;

    public string[] commands = new string[5];
    private string[] FingerCombo = new string[] {
        "Finger1", "Finger2", "Finger3", "Finger4", "Finger5"
    };

    dynamic control;

    void Start()
    {
        controllerInput = new HFController();
        // initialisation 
        //buttons = new Button[buttonSize];


        var engine = Python.CreateEngine();

        dynamic py = engine.ExecuteFile("Assets/control.py");

        control = py.control();

    }

    // Update is called once per frame
    void Update()
    {
        if (checkCmd == true)
        {
            useCommands();
            
        }
    }

    public void checkCommands() //this is to check if all the gestures are assignes a key
    {
        for (int i = 0; i < buttonSize; i++)
        {
            if (commands[i] == "")
            {
                txt.text = "Please Set Commands for All the Gestures";
                
            }
            else
            {
                checkCmd = true;
            }
        }
    }
    public void getCommand(int num)//this would get the user input and assign it to commands
    {
        commands[num] = Inputcmd[num].text;
        for (int i = 0; i < buttonSize; i++)
        {
            if (i == num) 
            {
                i++;
                continue;
            }

            else if (commands[num] == commands[i])
            {
                txt.text = "Please Enter a Different Command for " + FingerCombo[num];
                Inputcmd[num].text = "";
                commands[num] = "";
            }
        }

        if (commands[num] != "")
        {
            Debug.Log("the command " + commands[num] + " is assigned to " + FingerCombo[num] + " and is set to " + HT[num]);
        }
    }
    public void HoldToggle(int num) // this is for hold/toggle
    {
        string btcheck = HTbutton[num].GetComponentInChildren<Text>().text;
        if(btcheck == "Hold")
        {
            HTbutton[num].GetComponentInChildren<Text>().text = "Toggle";
            HT[num] = "Toggle";
        }
        else if(btcheck == "Toggle")
        {
            HTbutton[num].GetComponentInChildren<Text>().text = "Hold";
            HT[num] = "Hold";
        }
        Debug.Log("the command " + commands[num] + " is assigned to " + FingerCombo[num] + " and is set to " + HT[num]);

    }

    
    public void useCommands() //this would use commands to send key strokes.
    {
        if (controllerInput.GetSensorValue(0) > 500)
        {
            control.keyboardPress("w");
            //SendKeys.SendWait(commands[0]);
            //My.Computer.Keyboard.SendKeys(commands[0]);
            if (Input.GetKeyDown(commands[0]))
            {
                Debug.Log("It Worked!!!!!");
            }
        }
        if (controllerInput.GetSensorValue(1) > 500)
        {
            //My.Computer.Keyboard.SendKeys(commands[1]);
            if (Input.GetKeyDown(commands[1]))
            {
                Debug.Log("It Worked!!!!!");
            }
        }
        if (controllerInput.GetSensorValue(2) > 500)
        {
            //My.Computer.Keyboard.SendKeys(commands[2]);
            if (Input.GetKeyDown(commands[2]))
            {
                Debug.Log("It Worked!!!!!");
            }
        }
        if (controllerInput.GetSensorValue(3) > 500)
        {
           // My.Computer.Keyboard.SendKeys(commands[3]);
            if (Input.GetKeyDown(commands[3]))
            {
                Debug.Log("It Worked!!!!!");
            }
        }
        if (controllerInput.GetSensorValue(4) > 500)
        {
            //My.Computer.Keyboard.SendKeys(commands[4]);
            if (Input.GetKeyDown(commands[4]))
            {
                Debug.Log("It Worked!!!!!");
            }
        }
    }
    
    
    
}