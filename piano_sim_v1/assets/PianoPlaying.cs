using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPlaying : MonoBehaviour
{
    //Defining calibration values
    int cal1 = 200;
    int cal2 = 700;

    float pos_x = 0.1017f;
    float pos_z = -0.4797f;

    //Defining key movement control values
    //float max_turn = 0f;
    //float min_turn = 0f;
    //float hit_turn = 0f;
    float max_height = 0.7557f;
    float min_height = 0.729f;
    float hit_height = 0.735f;
    float release_height = 0.74f;
    float move_val = 0f;
    float speed = 0.000002f;

    //Defining state variables
    bool[] note_playing = {false, false, false, false, false};
    //float cur_pos = 0.7517f;
    float[] pos = { 0.7517f, 0.7517f, 0.7517f, 0.7517f, 0.7517f };
    int sens_val = 0;
    
    //Defining object and class variables
    AudioSource c4, d4, e4, f4, g4;
    AudioSource[] notes;
    public GameObject hit_main, hit_o5, hit_c4, hit_d4, hit_e4, hit_f4, hit_g4;
    public GameObject j_c4, j_d4, j_e4, j_f4, j_g4;
    public GameObject finger1, finger2, finger3, finger4, finger5;
    HFController controllerInput;


    void Start()
    {
        //Load audio files
        load_audio();

        

        //Defining controller
        controllerInput = new HFController();

    }

    void Update()
    {
        //Checks if controller is giving signal
        if (controllerInput.IsConnected)
        {
            //Edits finger positions
            for (int i = 0; i < 5; i++)
            {
                translate_movement(pos[i], i);
            }

            //Checks for note activation or stopping
            for (int i = 0; i < 5; i++)
            {
                note_check(pos[i], notes[i], note_playing[i]);
            }

        }
        else
        {
            Debug.Log("Not connected");
        }

    }

    void note_check(float pos, AudioSource note, bool note_playing)
    {
        //For note not being played
        if (note_playing == false)
        {
            //Check if note is at or under hit region
            if (pos <= hit_height)
            {
                //Play note
                note.Play();
                note_playing = true;
            }
        }
        //For note being played
        else
        {
            //Check if note is at or above release position
            if (pos >= release_height)
            {
                //Stop note
                note.Stop();
                note_playing = false;
            }
        }
    }

    void translate_movement(float current_pos, int sens_num)
    {
        //Check what the movement is relative to

        sens_val = controllerInput.GetSensorValue(sens_num);
        if (sens_val<cal1)
        {
            //Move up
            move_val = (cal1-sens_val) * speed;
            if (current_pos + move_val >= max_height)
            {
                //Translate to position of maximum height
                current_pos = max_height;
                Vector3 temp = new Vector3(pos_x, current_pos, pos_z);
                finger1.transform.position = temp;
            }
            else {
                //Translate to position of added height
                current_pos = current_pos + move_val;
                Vector3 temp = new Vector3(pos_x, current_pos, pos_z);
                finger1.transform.position = temp;
            }
        }
        else
        {
            //Move down
            move_val = (sens_val-cal1) * speed;
            if (current_pos - move_val <= min_height)
            {
                //Translate to position of minimum height
                current_pos = min_height;
                Vector3 temp = new Vector3(pos_x, current_pos, pos_z);
                finger1.transform.position = temp;
            }
            else
            {
                //Translate to posision of subtracted height
                current_pos = current_pos - move_val;
                Vector3 temp = new Vector3(pos_x, current_pos, pos_z);
                finger1.transform.position = temp;
            }
        }
    }

    //Loads audio files when called
    void load_audio()
    {
        c4 = transform.GetChild(4).transform.GetChild(23).GetComponent<AudioSource>();
        d4 = transform.GetChild(4).transform.GetChild(24).GetComponent<AudioSource>();
        e4 = transform.GetChild(4).transform.GetChild(25).GetComponent<AudioSource>();
        f4 = transform.GetChild(4).transform.GetChild(26).GetComponent<AudioSource>();
        g4 = transform.GetChild(4).transform.GetChild(27).GetComponent<AudioSource>();

        
    }


}
