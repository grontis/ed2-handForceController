using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gestures
{
    private string[] FingersCombo;
    
    
    private HFController controllerInput;

    public Gestures()
    {
        controllerInput = new HFController();
        FingersCombo = new string[] {
            "Finger1", "Finger2", "Finger3", "Finger4", "Finger5", "Finger1&2", "Finger2&3", "Finger3&4", "Finger4&5", "Finger1&3", "Finger2&4", "Finger3&5", "Finger1&4", "Finger2&5", "Finger1&5", "Finger1&2&3", "Finger2&3&4", "Finger3&4&5", "Finger1&2&4", "Finger2&3&5", "Finger1&2&5", "Finger1&3&4", "Finger2&4&5", "Finger1&3&5", "Finger1&4&5", "Finger1&2&3&4", "Finger2&3&4&5", "Finger1&2&3&5", "Finger1&2&4&5", "Finger1&3&4&5", "Finger1&2&3&4&5"
        }; 
    }

    /*public int GetGestures()
    {
        //if statements to check if the user did the gesture.
        //there are a lot of if statements so i am trying to figure out a better way
    }*/
}
