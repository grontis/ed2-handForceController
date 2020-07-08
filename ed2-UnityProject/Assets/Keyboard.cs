using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    private HFController controllerInput;
    
    //These are for the TypeLetter that types in the UI Text Area
    private string word;
    private int index = -1;// because index is incremented at the start of the function.
    private string alpha = null;
    private string alpha2;
    private char[] nameChar = new char[300];
    public Text txt = null;

    public bool shift = false; // to check if the shift is toggeled

    private int buttonSize = 30;// for 30 combinations
    public Button[] buttons; // combinations = {"Finger1", "Finger2", "Finger3", "Finger4", "Finger5", "Finger1&2", "Finger2&3", "Finger3&4", "Finger4&5", "Finger1&3", "Finger2&4", "Finger3&5", "Finger1&4", "Finger2&5", "Finger1&5", "Finger1&2&3", "Finger2&3&4", "Finger3&4&5", "Finger1&2&4", "Finger2&3&5", "Finger1&2&5", "Finger1&3&4", "Finger2&4&5", "Finger1&3&5", "Finger1&4&5", "Finger1&2&3&4", "Finger2&3&4&5", "Finger1&2&3&5", "Finger1&2&4&5", "Finger1&3&4&5", "Finger1&2&3&4&5"};

    void Start()
    {
        controllerInput = new HFController();
        // initialisation 
        buttons = new Button[buttonSize];
        //InitializeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGesture();
        controllerInput.PrintReadings();
    }
    
    public void CheckGesture()
    {
        //onClick.Invok() will trigger a click on the button without having to click the button.
        //button[i].onClick.Invoke(); // we will get i from GetGesture function.
    }


    public void TypeLetter(string alphabet) //this function types the letter that was pressed
    {
        if (shift == false)
        {
            index++; //index is for backspace function that I havent done yet
            char[] keepchar = alphabet.ToCharArray();
            nameChar[index] = keepchar[0]; //nameChar is also for backspace function
            alpha = nameChar[index].ToString();
            word = word + alpha;
            txt.text = word;
        }
        else
        {
            index++;
            char[] keepchar = alphabet.ToUpper().ToCharArray();
            nameChar[index] = keepchar[0];
            alpha = nameChar[index].ToString();
            word = word + alpha;
            txt.text = word;
        }
    }

    public void backspaceFunction() //this function is called when the back button is presed
    {
        if (index >= 0)
        {
            index--;

            alpha2 = null;
            for (int i = 0; i < index + 1; i++)
            {
                alpha2 = alpha2 + nameChar[i].ToString();
            }

            word = alpha2;
            txt.text = word;
        }
    }

    public void enterFunction() // this funciton is called when enter is pressed 
    {
        index++;
        char[] enterChar;
        enterChar = Environment.NewLine.ToCharArray();
        for (int i = 0; i < enterChar.Length; i++)
        {
            nameChar[index + i] = enterChar[i];// index + i,  so that nameChar doesnt lose its previous inputs
        }
        alpha = nameChar[index].ToString();
        word = word + alpha;
        txt.text = word;
    }

    public void shiftFunction() // this is for shift toggle
    {
        if (shift == false)
        {
            shift = true;
        }
        else
        {
            shift = false;
        }
    }
    
}
//i have set an onClick event in the inspector for eac button that triggers the functions above.
// right now the UI keyboard works fine. You can type in Capital and have space and also use enter.
// i am working on the getgesture function.
// and i am also trying to have name attribute of the buttons in inspector as Finger1, Finger2, etc.
// So it would be easire to know which button[] cooresponds to which gesture.
// therefore each UI buttons can be set to which ever gesture(button[]) through inspector. 
//right now if you play the scene you have to use mouse to type.
