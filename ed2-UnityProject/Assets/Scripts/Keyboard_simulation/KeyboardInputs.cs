using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;

public class KeyboardInputs : MonoBehaviour
{
    private HFController controllerInput;
    // combinations = {"Finger1", "Finger2", "Finger3", "Finger4", "Finger5", "Finger1&2", "Finger2&3", "Finger3&4", "Finger4&5", "Finger1&3", "10Finger2&4", "Finger3&5", "Finger1&4", "Finger2&5", "Finger1&5", "Finger1&2&3", "Finger2&3&4", "Finger3&4&5", "Finger1&2&4", "19Finger2&3&5", "Finger1&2&5", "Finger1&3&4", "22Finger2&4&5", "Finger1&3&5", "Finger1&4&5", "Finger1&2&3&4", "26Finger2&3&4&5", "Finger1&2&3&5", "Finger1&2&4&5", "Finger1&3&4&5", "Finger1&2&3&4&5"};
    public Text txt;
    public InputField[] Inputcmd = new InputField[31];
    public Dropdown[] dp = new Dropdown[31];
    public string[] commands = new string[31];
    private string[] FingerCombo = new string[] {
        "Finger1", "Finger2", "Finger3", "Finger4", "Finger5", "Finger1&2", "Finger2&3", "Finger3&4", "Finger4&5", "Finger1&3", "Finger2&4", "Finger3&5", "Finger1&4", "Finger2&5", "Finger1&5", "Finger1&2&3", "Finger2&3&4", "Finger3&4&5", "Finger1&2&4", "Finger2&3&5", "Finger1&2&5", "Finger1&3&4", "Finger2&4&5", "Finger1&3&5", "Finger1&4&5", "Finger1&2&3&4", "Finger2&3&4&5", "Finger1&2&3&5", "Finger1&2&4&5", "Finger1&3&4&5", "Finger1&2&3&4&5"
    };

    

    void Start()
    {
        controllerInput = new HFController();
        // initialisation 
        //buttons = new Button[buttonSize];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkCommands() //this is to check if the individual five sensors are assigned a key
    {
        bool checkCmd = false;
        for (int i = 0; i < 5; i++)
        {
            if (commands[i] == "")
            {
                txt.text = "Minimum Requirement: Set Commands for Finger1, Finger2, Finger3, Finger4, Finger5";

            }
            else
            {
                if(i == 4)
                {
                    checkCmd = true; // So the .py file would only execute once
                }
            }
        }
        if (checkCmd == true)
        {
            saveText(); //saves the inputs to a text file
            controllerInput.ClosePort();// to stop using port so .py file can use it 
            var engine = Python.CreateEngine();
            engine.ExecuteFile("Assets/Python/executionfile.py"); //executes the .py file
        }
    }
    public void setCommand(int num)//this would get the user input and assign it to commands
    {
        txt.text = "";
        for (int i = 0; i < 31; i++)
        {
            if (i == num)
            {
                continue;
            }

            if (Inputcmd[num].text == Inputcmd[i].text)
            {
                txt.text = "Please Enter a Different Command for " + FingerCombo[num];
                Inputcmd[num].text = "";
            }
            else
            {
                if (Inputcmd[num].text == " ")
                {
                    commands[num] = "Space";
                }
                else
                {
                    commands[num] = Inputcmd[num].text;
                }
            }
        }
    }
    

    public void saveText()
    {
        string path = Application.dataPath + "/Python/settings.txt";

        if (!File.Exists(path))
        {
            File.OpenWrite(path);
        }
        File.WriteAllText(path, commands[0]);
        for (int i = 1; i < 31; i++)
        {
            if(commands[i] == "")
            {
                File.AppendAllText(path, "," + "");
            }
            else if(commands[i] == ",")
            {
                File.AppendAllText(path, "," + "comma");
            }
            else
            {
                File.AppendAllText(path, "," + commands[i]);
            }
        }
        File.AppendAllText(path, "," + controllerInput.ConnectedPortName);
    }
    
    public void dpSetCommands(int num)
    {
        int index = dp[num].GetComponent<Dropdown>().value;
        if(index == 0)
        {
            txt.text = "Invalid Option";
        }
        else
        {
            txt.text = "";
            List<Dropdown.OptionData> options = dp[num].GetComponent<Dropdown>().options;
            Inputcmd[num].characterLimit = 7;
            Inputcmd[num].text = options[index].text;
            commands[num] = Inputcmd[num].text;
            for (int i = 0; i < 31; i++)
            {
                if (i == num)
                {
                    continue;
                }

                if (commands[num] == commands[i])
                {
                    txt.text = "Please Enter a Different Command for " + FingerCombo[num];
                    Inputcmd[num].text = "";
                    commands[num] = "";
                }
            }
            Inputcmd[num].characterLimit = 1;
        }
        
    }
}
