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
    //These are for the TypeLetter that types in the UI Text Area// for 30 combinations
    //public Button[] buttons; // combinations = {"Finger1", "Finger2", "Finger3", "Finger4", "Finger5", "Finger1&2", "Finger2&3", "Finger3&4", "Finger4&5", "Finger1&3", "10Finger2&4", "Finger3&5", "Finger1&4", "Finger2&5", "Finger1&5", "Finger1&2&3", "Finger2&3&4", "Finger3&4&5", "Finger1&2&4", "19Finger2&3&5", "Finger1&2&5", "Finger1&3&4", "22Finger2&4&5", "Finger1&3&5", "Finger1&4&5", "Finger1&2&3&4", "26Finger2&3&4&5", "Finger1&2&3&5", "Finger1&2&4&5", "Finger1&3&4&5", "Finger1&2&3&4&5"};
    public Text txt;
    public InputField[] Inputcmd = new InputField[31];
    private bool checkCmd = false;
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
        if (checkCmd == true)
        {
            checkCmd = false;
        }
    }

    public void checkCommands() //this is to check if all the gestures are assignes a key
    {
        bool checkCmd = false;
        controllerInput.closeport();// to stop using port so .py file can use it 
        //i made one function closeport that closes the port in HFController.cs 
        //so add a function to close port in new HFCntroller.cs and change this function call accordingly
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

            var engine = Python.CreateEngine();
            engine.ExecuteFile("Assets/executionfile.py"); //executes the .py file
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

        if (commands[num] != "")
        {
            //Debug.Log("the command " + commands[num] + " is assigned to " + FingerCombo[num] + " and is set to " + HT[num]);
        }
    }
    /*public void HoldToggle(int num) // this is for hold/toggle
    {
        string btcheck = HTbutton[num].GetComponentInChildren<Text>().text;
        if(btcheck == "Hold")
        {
            HTbutton[num].GetComponentInChildren<Text>().text = "Toggle";
            HT[num] = "T";
        }
        else if(btcheck == "Toggle")
        {
            HTbutton[num].GetComponentInChildren<Text>().text = "Hold";
            HT[num] = "H";
        }
        //Debug.Log("the command " + commands[num] + " is assigned to " + FingerCombo[num] + " and is set to " + HT[num]);

    }*/

    public void saveText()
    {
        string path = Application.dataPath + "/settings.txt";

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
            else
            {
                File.AppendAllText(path, "," + commands[i]);
            }
            
        }
        
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
            if (commands[num] != "")
            {
                //Debug.Log("the command " + commands[num] + " is assigned to " + FingerCombo[num] + " and is set to " + HT[num]);
            }
        }
        
    }
}
