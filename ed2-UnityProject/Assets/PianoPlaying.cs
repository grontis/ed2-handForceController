using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPlaying : MonoBehaviour
{
    //Defining calibration values
    int[] cal = {140,140,140,140,140,140,120,440};

    float[] pos_x = { 0.1017f, 0.1393f, 0.1769f, 0.2153f, 0.2526f};

    //Defining key movement control values
    float max_height = 0.7557f;
    float min_height = 0.7252f;
    //float touch_key_height = 0.7437f;
    float hit_height = 0.735f;
    float release_height = 0.74f;
    float move_val = 0f;
    float speed = 0.00009f;
    float speed2 = 0.000005f;

    //Defining state variables

    //Booleans for if note is playing
    bool[] np1w = {false, false, false, false, false, false, false};
    bool[] np2w = { false, false, false, false, false, false, false };
    bool[] np3w = { false, false, false, false, false, false, false };
    bool[] np4w = { false, false, false, false, false, false, false };
    bool[] np5w = { false, false, false, false, false, false, false };
    bool[] np6w = { false, false, false, false, false, false, false };
    bool[] np7w = { false, false, false, false, false, false, false };
    bool[] np8w = {false, false, false};
    bool[] np1b = { false, false, false, false, false};
    bool[] np2b = { false, false, false, false, false };
    bool[] np3b = { false, false, false, false, false };
    bool[] np4b = { false, false, false, false, false };
    bool[] np5b = { false, false, false, false, false };
    bool[] np6b = { false, false, false, false, false };
    bool[] np7b = { false, false, false, false, false };
    bool np8b = false;

    //Finger positions

    //Records finger positions for y axis
    float[] pos = { 0.7557f, 0.7557f, 0.7557f, 0.7557f, 0.7557f };
    //Records position on piano for z axis
    float forward = 0;
    //Records position on piano for x axis
    float left_right = 0;

    //Sensor readings
    int sens_val = 0;

    //Defining key range values
    //Note positions for white keys are defined by the right-limits of the end of the note
    //Note positions for black keys are defined in pairs of left and right limits
    float[] o_total = {-0.7672f, 1.1594f};
    float[] o1wk = {-0.7396f, -0.7018f, -0.6646f, -0.6272f, -0.5895f, -0.5521f, -0.5147f};
    float[] o1bk = {-0.7514f, -0.7265f, -0.6767f, -0.6522f, -0.6394f, -0.6141f, -0.5642f, -0.5399f, -0.5276f, -0.5023f};
    float[] o2wk = {-0.4772f, -0.4403f, -0.4029f, -0.3655f, -0.3282f, -0.2909f, -0.2534f};
    float[] o2bk = {-0.49f, -0.4649f, -0.4151f, -0.3898f, -0.378f, -0.3526f, -0.3028f, -0.2782f, -0.2658f, -0.2403f};
    float[] o3wk = {-0.216f, -0.1788f, -0.1414f, -0.1043f, -0.0667f, -0.0295f, 0.0079f};
    float[] o3bk = {-0.2283f, -0.2035f, -0.1534f, -0.1287f, -0.116f, -0.0912f, -0.0409f, -0.0162f, -0.0037f, 0.0214f};
    float[] o4wk = {0.0453f, 0.0825f, 0.1202f, 0.1575f, 0.1951f, 0.2322f, 0.2698f};
    float[] o4bk = {0.0336f, 0.0584f, 0.1083f, 0.1328f, 0.1459f, 0.1707f, 0.2207f, 0.2456f, 0.2576f, 0.2827f};
    float[] o5wk = {0.3074f, 0.3447f, 0.3821f, 0.4195f, 0.4572f, 0.4941f, 0.5316f};
    float[] o5bk = {0.2952f, 0.3205f, 0.3701f, 0.3951f, 0.4072f, 0.4324f, 0.4826f, 0.5073f, 0.5195f, 0.5444f};
    float[] o6wk = {0.5691f, 0.6064f, 0.6437f, 0.681f, 0.7184f, 0.7561f, 0.7935f};
    float[] o6bk = {0.5568f, 0.5817f, 0.6319f, 0.657f, 0.6689f, 0.6942f, 0.7441f, 0.7691f, 0.7815f, 0.806f};
    float[] o7wk = {0.8307f, 0.8685f, 0.9055f, 0.9428f, 0.9804f, 1.0177f, 1.0548f};
    float[] o7bk = {0.8186f, 0.8433f, 0.8937f, 0.9186f, 0.9309f, 0.9558f, 1.0056f, 1.0302f, 1.043f, 1.0676f};
    float[] o8wk = {1.0925f, 1.1302f}; //Last is defined by o_total
    float[] o8bk = {1.0804f, 1.105f};
    float[] z_range = {-0.481f, -0.4455f, -0.3664f};

    //Defining object and class variables

    //Audio for notes
    AudioSource a1, bb1, b1, c1, db1, d1, eb1, e1, f1, gb1, g1, ab1;
    AudioSource a2, bb2, b2, c2, db2, d2, eb2, e2, f2, gb2, g2, ab2;
    AudioSource a3, bb3, b3, c3, db3, d3, eb3, e3, f3, gb3, g3, ab3;
    AudioSource a4, bb4, b4, c4, db4, d4, eb4, e4, f4, gb4, g4, ab4;
    AudioSource a5, bb5, b5, c5, db5, d5, eb5, e5, f5, gb5, g5, ab5;
    AudioSource a6, bb6, b6, c6, db6, d6, eb6, e6, f6, gb6, g6, ab6;
    AudioSource a7, bb7, b7, c7, db7, d7, eb7, e7, f7, gb7, g7, ab7;
    AudioSource a8, bb8, b8, c8;

    //White keys
    Transform wka1, wkb1, wkc1, wkd1, wke1, wkf1, wkg1;
    Transform wka2, wkb2, wkc2, wkd2, wke2, wkf2, wkg2;
    Transform wka3, wkb3, wkc3, wkd3, wke3, wkf3, wkg3;
    Transform wka4, wkb4, wkc4, wkd4, wke4, wkf4, wkg4;
    Transform wka5, wkb5, wkc5, wkd5, wke5, wkf5, wkg5;
    Transform wka6, wkb6, wkc6, wkd6, wke6, wkf6, wkg6;
    Transform wka7, wkb7, wkc7, wkd7, wke7, wkf7, wkg7;
    Transform wka8, wkb8, wkc8;

    //Black keys
    Transform bkbb1, bkdb1, bkeb1, bkgb1, bkab1;
    Transform bkbb2, bkdb2, bkeb2, bkgb2, bkab2;
    Transform bkbb3, bkdb3, bkeb3, bkgb3, bkab3;
    Transform bkbb4, bkdb4, bkeb4, bkgb4, bkab4;
    Transform bkbb5, bkdb5, bkeb5, bkgb5, bkab5;
    Transform bkbb6, bkdb6, bkeb6, bkgb6, bkab6;
    Transform bkbb7, bkdb7, bkeb7, bkgb7, bkab7;
    Transform bkbb8;

    //Finger objects
    public GameObject finger1, finger2, finger3, finger4, finger5;

    //Camera
    public GameObject cam;

    //Materials and renders for color changing notes
    public Material M_purple, M_black, M_pink, M_white;
    Renderer ra1, rbb1, rb1, rc1, rdb1, rd1, reb1, re1, rf1, rgb1, rg1, rab1;
    Renderer ra2, rbb2, rb2, rc2, rdb2, rd2, reb2, re2, rf2, rgb2, rg2, rab2;
    Renderer ra3, rbb3, rb3, rc3, rdb3, rd3, reb3, re3, rf3, rgb3, rg3, rab3;
    Renderer ra4, rbb4, rb4, rc4, rdb4, rd4, reb4, re4, rf4, rgb4, rg4, rab4;
    Renderer ra5, rbb5, rb5, rc5, rdb5, rd5, reb5, re5, rf5, rgb5, rg5, rab5;
    Renderer ra6, rbb6, rb6, rc6, rdb6, rd6, reb6, re6, rf6, rgb6, rg6, rab6;
    Renderer ra7, rbb7, rb7, rc7, rdb7, rd7, reb7, re7, rf7, rgb7, rg7, rab7;
    Renderer ra8, rbb8, rb8, rc8;

    //Records previous notes played by fingers so they can be canceled
    AudioSource prev_n1;
    AudioSource prev_n2;
    AudioSource prev_n3;
    AudioSource prev_n4;
    AudioSource prev_n5;

    //Holds the previous render
    Renderer prev_r1;
    Renderer prev_r2;
    Renderer prev_r3;
    Renderer prev_r4;
    Renderer prev_r5;

    //If the previous note is black (f) or white (t)
    bool prev_bw1 = true;
    bool prev_bw2 = true;
    bool prev_bw3 = true;
    bool prev_bw4 = true;
    bool prev_bw5 = true;

    //Holds the previous note octave
    int prev_o1 = 1;
    int prev_o2 = 1;
    int prev_o3 = 1;
    int prev_o4 = 1;
    int prev_o5 = 1;

    //Holds the previous note position in octave
    int prev_p1 = 0;
    int prev_p2 = 0;
    int prev_p3 = 0;
    int prev_p4 = 0;
    int prev_p5 = 0;

    //Controller class
    HFController controllerInput;

    void Start()
    {
        //If calibration is saved, new calibration is defined from PlayerPrefs
        if (PlayerPrefs.HasKey("cal_reading7"))
        {
            for(int i = 0; i < 8; i++)
            {
                cal[i] = PlayerPrefs.GetInt("cal_reading" + i);
            }
            Debug.Log("Using latest calibration settings");
        }
        else
        {
            Debug.Log("Could not find saved calibration settings");
        }
        
        //Load audio files, notes, and note renders
        load_objects();

        //Defining controller
        controllerInput = new HFController();

    }

    void Update()
    {
        //Checks if controller is giving signal
        if (controllerInput.IsConnected)
        {
            //Edits finger positions
            
            //Move fingers down and up
            pos[0] = translate_movement(pos[0], 0, finger1);
            pos[1] = translate_movement(pos[1], 1, finger2);
            pos[2] = translate_movement(pos[2], 2, finger3);
            pos[3] = translate_movement(pos[3], 3, finger4);
            pos[4] = translate_movement(pos[4], 4, finger5);

            //Move fingers left, right, foward, and backward

            left_right = read_left_right();
            forward = read_forward_back();
            if (left_right != 0 || forward != 0)
            {
                move_lr_fb(left_right, forward);
            }

            //Checks for note activation or stopping

            identify_note(finger1, 0);
            identify_note(finger2, 1);
            identify_note(finger3, 2);
            identify_note(finger4, 3);
            identify_note(finger5, 4);
        }
        else
        {
            Debug.Log("Not connected");
        }

    }

    //Moves fingers left, right, forward, and back based on last 3 sensor values
    void move_lr_fb(float move_val1, float move_val2)
    {
        Vector3 move_vec = new Vector3(move_val1, 0f, move_val2);
        Vector3 cam_vec = new Vector3(move_val1, 0f, 0f);
        finger1.transform.position += move_vec;
        finger2.transform.position += move_vec;
        finger3.transform.position += move_vec;
        finger4.transform.position += move_vec;
        finger5.transform.position += move_vec;
        cam.transform.position += cam_vec;
    }

    //Get amount to move forward or backward
    float read_forward_back()
    {
        float forward = 0f;
        int forward_val = controllerInput.GetSensorValue(7);
        forward = (forward_val - cal[7]) * speed2;
        //Ensure it does not go past the boundaries of the keyboard
        float z = finger1.transform.position.z;
        if (z + forward < z_range[0]) //If the fingers are too far back
        {
            return 0f;
        }
        else if (z + forward > z_range[2]) //If the fingers are too far forward
        {
            return 0f;
        }
        return forward;
    }

    //Get amount to move left or right
    float read_left_right()
    {
        float left = 0f;
        float right = 0f;
        int left_val = controllerInput.GetSensorValue(5);
        int right_val = controllerInput.GetSensorValue(6);
        if (left_val > cal[5])
        {
            left = (left_val - cal[5]) * speed2;
        }
        if(right_val > cal[6])
        {
            right = (right_val - cal[6]) * speed2;
        }
        //Ensure it does not go past the boundaries of the keyboard
        float x1 = finger1.transform.position.x;
        float x2 = finger5.transform.position.x;
        if (x1 + (right-left) < o_total[0]) //If the fingers are too far left
        {
            return 0f;
        }
        else if(x2 + (right-left) > o_total[1]) //If the fingers are too far right
        {
            return 0f;
        }
        return right - left;
    }

    //Moves keys up and down and saves the position
    float translate_movement(float current_pos, int sens_num, GameObject finger)
    {
        //Check what the movement is relative to

        sens_val = controllerInput.GetSensorValue(sens_num);
        float x = finger.transform.position.x;
        float z = finger.transform.position.z;
        if (sens_val < cal[sens_num])
        {
            //Move up
            move_val = (cal[sens_num] - sens_val) * speed;
            if (current_pos + move_val >= max_height)
            {
                //Translate to position of maximum height
                current_pos = max_height;
                Vector3 temp = new Vector3(x, current_pos, z);
                finger.transform.position = temp;
            }
            else
            {
                //Translate to position of added height
                current_pos = current_pos + move_val;
                Vector3 temp = new Vector3(x, current_pos, z);
                finger.transform.position = temp;
            }
        }
        else
        {
            //Move down
            move_val = (sens_val - cal[sens_num]) * speed;
            if (current_pos - move_val <= min_height)
            {
                //Translate to position of minimum height
                current_pos = min_height;
                Vector3 temp = new Vector3(x, current_pos, z);
                finger.transform.position = temp;
            }
            else
            {
                //Translate to posision of subtracted height
                current_pos = current_pos - move_val;
                Vector3 temp = new Vector3(x, current_pos, z);
                finger.transform.position = temp;
            }
        }
        return current_pos;
    }

    //Finds the note that a finger is on top of
    void identify_note(GameObject finger, int num)
    {
        float pos_x = finger.transform.position.x;
        float pos_z = finger.transform.position.z;
        int val;
        bool b_val;

        if (pos_z < z_range[1]) //If finger is on white keys only
        {
            //Check octaves, then note
            if(pos_x < o1wk[6]) //Octave 1
            {
                val = w_octave_check(np1w, o1wk, pos_x, num, 1, a1, b1, c1, d1, e1, f1, g1, ra1, rb1, rc1, rd1, re1, rf1, rg1);
                b_val = isPositive(val);
                val = ar_num(b_val, val);
                np1w[val] = b_val;
            }
            else if(pos_x < o2wk[6]) //Octave 2
            {
                val = w_octave_check(np2w, o2wk, pos_x, num, 2, a2, b2, c2, d2, e2, f2, g2, ra2, rb2, rc2, rd2, re2, rf2, rg2);
                b_val = isPositive(val);
                val = ar_num(b_val, val);
                np2w[val] = b_val;
            }
            else if(pos_x < o3wk[6]) //Octave 3
            {
                val = w_octave_check(np3w, o3wk, pos_x, num, 3, a3, b3, c3, d3, e3, f3, g3, ra3, rb3, rc3, rd3, re3, rf3, rg3);
                b_val = isPositive(val);
                val = ar_num(b_val, val);
                np3w[val] = b_val;
            }
            else if(pos_x < o4wk[6]) //Octave 4
            {
                val = w_octave_check(np4w, o4wk, pos_x, num, 4, a4, b4, c4, d4, e4, f4, g4, ra4, rb4, rc4, rd4, re4, rf4, rg4);
                b_val = isPositive(val);
                val = ar_num(b_val, val);
                np4w[val] = b_val;
            }
            else if(pos_x < o5wk[6]) //Octave 5
            {
                val = w_octave_check(np5w, o5wk, pos_x, num, 5, a5, b5, c5, d5, e5, f5, g5, ra5, rb5, rc5, rd5, re5, rf5, rg5);
                b_val = isPositive(val);
                val = ar_num(b_val, val);
                np5w[val] = b_val;
            }
            else if(pos_x < o6wk[6]) //Octave 6
            {
                val = w_octave_check(np6w, o6wk, pos_x, num, 6, a6, b6, c6, d6, e6, f6, g6, ra6, rb6, rc6, rd6, re6, rf6, rg6);
                b_val = isPositive(val);
                val = ar_num(b_val, val);
                np6w[val] = b_val;
            }
            else if(pos_x < o7wk[6]) //Octave 7
            {
                val = w_octave_check(np7w, o7wk, pos_x, num, 7, a7, b7, c7, d7, e7, f7, g7, ra7, rb7, rc7, rd7, re7, rf7, rg7);
                b_val = isPositive(val);
                val = ar_num(b_val, val);
                np7w[val] = b_val;
            }
            else //Octave 8
            {
                if(pos_x < o8wk[0])
                {
                    np8w[0] = note_check(pos[num], a8, np8w[0], ra8, 8, 0, num);
                }
                else if(pos_x < o8wk[1])
                {
                    np8w[1] = note_check(pos[num], b8, np8w[1], rb8, 8, 1, num);
                }
                else
                {
                    np8w[2] = note_check(pos[num], c8, np8w[2], rc8, 8, 2, num);
                }
            }
        }
        else //If finger is on white and black keys
        {
            //Check octaves, then note (but take black keys into consideration
            if(pos_x < o1bk[9]) //Octave 1
            {
                //Check if in range of black keys
                if (o1bk[0] < pos_x && pos_x < o1bk[1])
                {
                    np1b[0] = note_check(pos[num], bb1, np1b[0], rbb1, 1, 0, num, false);
                }
                else if (o1bk[2] < pos_x && pos_x < o1bk[3])
                {
                    np1b[1] = note_check(pos[num], db1, np1b[1], rdb1, 1, 1, num, false);
                }
                else if (o1bk[4] < pos_x && pos_x < o1bk[5])
                {
                    np1b[2] = note_check(pos[num], eb1, np1b[2], reb1, 1, 2, num, false);
                }
                else if (o1bk[6] < pos_x && pos_x < o1bk[7])
                {
                    np1b[3] = note_check(pos[num], gb1, np1b[3], rgb1, 1, 3, num, false);
                }
                else if (o1bk[8] < pos_x && pos_x < o1bk[9])
                {
                    np1b[4] = note_check(pos[num], ab1, np1b[4], rab1, 1, 4, num, false);
                }
                else //Must be a white key
                {
                    val = w_octave_check(np1w, o1wk, pos_x, num, 1, a1, b1, c1, d1, e1, f1, g1, ra1, rb1, rc1, rd1, re1, rf1, rg1);
                    b_val = isPositive(val);
                    val = ar_num(b_val, val);
                    np1w[val] = b_val;
                }
            }
            else if(pos_x < o2bk[9]) //Octave 2
            {
                //Check if in range of black keys
                if (o2bk[0] < pos_x && pos_x < o2bk[1])
                {
                    np2b[0] = note_check(pos[num], bb2, np2b[0], rbb2, 2, 0, num, false);
                }
                else if (o2bk[2] < pos_x && pos_x < o2bk[3])
                {
                    np2b[1] = note_check(pos[num], db2, np2b[1], rdb2, 2, 1, num, false);
                }
                else if (o2bk[4] < pos_x && pos_x < o2bk[5])
                {
                    np2b[2] = note_check(pos[num], eb2, np2b[2], reb2, 2, 2, num, false);
                }
                else if (o2bk[6] < pos_x && pos_x < o2bk[7])
                {
                    np2b[3] = note_check(pos[num], gb2, np2b[3], rgb2, 2, 3, num, false);
                }
                else if (o2bk[8] < pos_x && pos_x < o2bk[9])
                {
                    np2b[4] = note_check(pos[num], ab2, np2b[4], rab2, 2, 4, num, false);
                }
                else //Must be a white key
                {
                    val = w_octave_check(np2w, o2wk, pos_x, num, 2, a2, b2, c2, d2, e2, f2, g2, ra2, rb2, rc2, rd2, re2, rf2, rg2);
                    b_val = isPositive(val);
                    val = ar_num(b_val, val);
                    np2w[val] = b_val;
                }
            }
            else if(pos_x < o3bk[9]) //Octave 3
            {
                //Check if in range of black keys
                if (o3bk[0] < pos_x && pos_x < o3bk[1])
                {
                    np3b[0] = note_check(pos[num], bb3, np3b[0], rbb3, 3, 0, num, false);
                }
                else if (o3bk[2] < pos_x && pos_x < o3bk[3])
                {
                    np3b[1] = note_check(pos[num], db3, np3b[1], rdb3, 3, 1, num, false);
                }
                else if (o3bk[4] < pos_x && pos_x < o3bk[5])
                {
                    np3b[2] = note_check(pos[num], eb3, np3b[2], reb3, 3, 2, num, false);
                }
                else if (o3bk[6] < pos_x && pos_x < o3bk[7])
                {
                    np3b[3] = note_check(pos[num], gb3, np3b[3], rgb3, 3, 3, num, false);
                }
                else if (o3bk[8] < pos_x && pos_x < o3bk[9])
                {
                    np3b[4] = note_check(pos[num], ab3, np3b[4], rab3, 3, 4, num, false);
                }
                else //Must be a white key
                {
                    val = w_octave_check(np3w, o3wk, pos_x, num, 3, a3, b3, c3, d3, e3, f3, g3, ra3, rb3, rc3, rd3, re3, rf3, rg3);
                    b_val = isPositive(val);
                    val = ar_num(b_val, val);
                    np3w[val] = b_val;
                }
            }
            else if(pos_x < o4bk[9]) //Octave 4
            {
                //Check if in range of black keys
                if (o4bk[0] < pos_x && pos_x < o4bk[1])
                {
                    np4b[0] = note_check(pos[num], bb4, np4b[0], rbb4, 4, 0, num, false);
                }
                else if (o4bk[2] < pos_x && pos_x < o4bk[3])
                {
                    np4b[1] = note_check(pos[num], db4, np4b[1], rdb4, 4, 1, num, false);
                }
                else if (o4bk[4] < pos_x && pos_x < o4bk[5])
                {
                    np4b[2] = note_check(pos[num], eb4, np4b[2], reb4, 4, 2, num, false);
                }
                else if (o4bk[6] < pos_x && pos_x < o4bk[7])
                {
                    np4b[3] = note_check(pos[num], gb4, np4b[3], rgb4, 4, 3, num, false);
                }
                else if (o4bk[8] < pos_x && pos_x < o4bk[9])
                {
                    np4b[4] = note_check(pos[num], ab4, np4b[4], rab4, 4, 4, num, false);
                }
                else //Must be a white key
                {
                    val = w_octave_check(np4w, o4wk, pos_x, num, 4, a4, b4, c4, d4, e4, f4, g4, ra4, rb4, rc4, rd4, re4, rf4, rg4);
                    b_val = isPositive(val);
                    val = ar_num(b_val, val);
                    np4w[val] = b_val;
                }
            }
            else if (pos_x < o5bk[9]) //Octave 5
            {
                //Check if in range of black keys
                if (o5bk[0] < pos_x && pos_x < o5bk[1])
                {
                    np5b[0] = note_check(pos[num], bb5, np5b[0], rbb5, 5, 0, num, false);
                }
                else if (o5bk[2] < pos_x && pos_x < o5bk[3])
                {
                    np5b[1] = note_check(pos[num], db5, np5b[1], rdb5, 5, 1, num, false);
                }
                else if (o5bk[4] < pos_x && pos_x < o5bk[5])
                {
                    np5b[2] = note_check(pos[num], eb5, np5b[2], reb5, 5, 2, num, false);
                }
                else if (o5bk[6] < pos_x && pos_x < o5bk[7])
                {
                    np5b[3] = note_check(pos[num], gb5, np5b[3], rgb5, 5, 3, num, false);
                }
                else if (o5bk[8] < pos_x && pos_x < o5bk[9])
                {
                    np5b[4] = note_check(pos[num], ab5, np5b[4], rab5, 5, 4, num, false);
                }
                else //Must be a white key
                {
                    val = w_octave_check(np5w, o5wk, pos_x, num, 5, a5, b5, c5, d5, e5, f5, g5, ra5, rb5, rc5, rd5, re5, rf5, rg5);
                    b_val = isPositive(val);
                    val = ar_num(b_val, val);
                    np5w[val] = b_val;
                }
            }
            else if (pos_x < o6bk[9]) //Octave 6
            {
                //Check if in range of black keys
                if (o6bk[0] < pos_x && pos_x < o6bk[1])
                {
                    np6b[0] = note_check(pos[num], bb6, np6b[0], rbb6, 6, 0, num, false);
                }
                else if (o6bk[2] < pos_x && pos_x < o6bk[3])
                {
                    np6b[1] = note_check(pos[num], db6, np6b[1], rdb6, 6, 1, num, false);
                }
                else if (o6bk[4] < pos_x && pos_x < o6bk[5])
                {
                    np6b[2] = note_check(pos[num], eb6, np6b[2], reb6, 6, 2, num, false);
                }
                else if (o6bk[6] < pos_x && pos_x < o6bk[7])
                {
                    np6b[3] = note_check(pos[num], gb6, np6b[3], rgb6, 6, 3, num, false);
                }
                else if (o6bk[8] < pos_x && pos_x < o6bk[9])
                {
                    np6b[4] = note_check(pos[num], ab6, np6b[4], rab6, 6, 4, num, false);
                }
                else //Must be a white key
                {
                    val = w_octave_check(np6w, o6wk, pos_x, num, 6, a6, b6, c6, d6, e6, f6, g6, ra6, rb6, rc6, rd6, re6, rf6, rg6);
                    b_val = isPositive(val);
                    val = ar_num(b_val, val);
                    np6w[val] = b_val;
                }
            }
            else if (pos_x < o7bk[9]) //Octave 7
            {
                //Check if in range of black keys
                if (o7bk[0] < pos_x && pos_x < o7bk[1])
                {
                    np7b[0] = note_check(pos[num], bb7, np7b[0], rbb7, 7, 0, num, false);
                }
                else if (o7bk[2] < pos_x && pos_x < o7bk[3])
                {
                    np7b[1] = note_check(pos[num], db7, np7b[1], rdb7, 7, 1, num, false);
                }
                else if (o7bk[4] < pos_x && pos_x < o7bk[5])
                {
                    np7b[2] = note_check(pos[num], eb7, np7b[2], reb7, 7, 2, num, false);
                }
                else if (o7bk[6] < pos_x && pos_x < o7bk[7])
                {
                    np7b[3] = note_check(pos[num], gb7, np7b[3], rgb7, 7, 3, num, false);
                }
                else if (o7bk[8] < pos_x && pos_x < o7bk[9])
                {
                    np7b[4] = note_check(pos[num], ab7, np7b[4], rab7, 7, 4, num, false);
                }
                else //Must be a white key
                {
                    val = w_octave_check(np7w, o7wk, pos_x, num, 7, a7, b7, c7, d7, e7, f7, g7, ra7, rb7, rc7, rd7, re7, rf7, rg7);
                    b_val = isPositive(val);
                    val = ar_num(b_val, val);
                    np7w[val] = b_val;
                }
            }
            else //Octave 8
            {
                if (o8bk[0] < pos_x  && pos_x < o8bk[1]) //Only octave 8 black key
                {
                    np8b = note_check(pos[num], bb8, np8b, rbb8, 8, 0, num, false);
                }
                else if (pos_x < o8wk[0])
                {
                    np8w[0] = note_check(pos[num], a8, np8w[0], ra8, 8, 0, num);
                }
                else if (pos_x < o8wk[1])
                {
                    np8w[1] = note_check(pos[num], b8, np8w[1], rb8, 8, 1, num);
                }
                else
                {
                    np8w[2] = note_check(pos[num], c8, np8w[2], rc8, 8, 2, num);
                }
            }
        }
    }

    //Returns true for positive, false for negative
    bool isPositive(int n)
    {
        if(n % 2 == 0)
        {
            return true;
        }
        return false;
    }

    //Returns array number based on if positive or negative
    int ar_num(bool val, int n)
    {
        if(val == true)
        {
            return n / 2;
        }
        else
        {
            return (n+1)/2;
        }
    }

    //Goes through a white key octave to find the chosen white key of a finger
    int w_octave_check(bool[] np, float[] ranges, float pos_x, int num, int oct,
        AudioSource n1, AudioSource n2, AudioSource n3, AudioSource n4, AudioSource n5, AudioSource n6, AudioSource n7,
        Renderer r1, Renderer r2, Renderer r3, Renderer r4, Renderer r5, Renderer r6, Renderer r7)
    {
        int val;

        //For the correct white key note,
        //check what note should be played, stopped, or not played.
        if (pos_x < ranges[0])
        {
            np[0] = note_check(pos[num], n1, np[0], r1, oct, 0, num);
            val = get_return_val(np[0], 0);
        }
        else if (pos_x < ranges[1])
        {
            np[1] = note_check(pos[num], n2, np[1], r2, oct, 1, num);
            val = get_return_val(np[1], 1);
        }
        else if (pos_x < ranges[2])
        {
            np[2] = note_check(pos[num], n3, np[2], r3, oct, 2, num);
            val = get_return_val(np[2], 2);
        }
        else if (pos_x < ranges[3])
        {
            np[3] = note_check(pos[num], n4, np[3], r4, oct, 3, num);
            val = get_return_val(np[3], 3);
        }
        else if (pos_x < ranges[4])
        {
            np[4] = note_check(pos[num], n5, np[4], r5, oct, 4, num);
            val = get_return_val(np[4], 4);
        }
        else if (pos_x < ranges[5])
        {
            np[5] = note_check(pos[num], n6, np[5], r6, oct, 5, num);
            val = get_return_val(np[5], 5);
        }
        else
        {
            np[6] = note_check(pos[num], n7, np[6], r7, oct, 6, num);
            val = get_return_val(np[6], 6);
        }

        //Return value that gives boolean and index
        return val;
    }

    int get_return_val(bool val, int n)
    {
        //Gives integer based on number and boolean
        if (val == true) //Got note playing as true
        {
            //Make integer times 2
            return n * 2;
        }
        else //Got note playing as false
        {
            //Make integer times 2 minus 1 for odd value
            return n * 2 - 1;
        }
    }

    void stop_prev(int finger, AudioSource note, Renderer rend, int octave, int num, bool b_w)
    {
        //finger: for fingers 0-4
        //octave: values 1-8
        //num: values 0-6 for white keys, 0-4 for black keys
        //b_w: false for black key, true for white key
        if (finger == 0)
        {
            if (prev_n1 == null)
            {
                prev_n1 = note;
                prev_r1 = rend;
                prev_p1 = num;
                prev_r1 = rend;
                prev_n1 = note;
            }
            else if (prev_n1 != note) //If the current note is a new note
            {
                //Stop the previous note
                prev_n1.Stop();
                if (prev_bw1 == true)
                {
                    prev_r1.material = M_white;
                }
                else
                {
                    prev_r1.material = M_black;
                }
                //Updating previous variables for finger 1
                prev_bw1 = b_w;
                prev_o1 = octave;
                prev_p1 = num;
                prev_r1 = rend;
                prev_n1 = note;
                //Replacing the np (note playing) boolean of true to false
                update_np(b_w, octave, num);
            }
        }
        else if (finger == 1)
        {
            if (prev_n2 == null)
            {
                prev_n2 = note;
                prev_r2 = rend;
                prev_p2 = num;
                prev_r2 = rend;
                prev_n2 = note;
            }
            else if (prev_n2 != note)
            {
                prev_n2.Stop();
                if (prev_bw2 == true)
                {
                    prev_r2.material = M_white;
                }
                else
                {
                    prev_r2.material = M_black;
                }
                prev_bw2 = b_w;
                prev_o2 = octave;
                prev_p2 = num;
                prev_r2 = rend;
                prev_n2 = note;
                update_np(b_w, octave, num);
            }
        }
        else if (finger == 2)
        {
            if (prev_n3 == null)
            {
                prev_n3 = note;
                prev_r3 = rend;
                prev_p3 = num;
                prev_r3 = rend;
                prev_n3 = note;
            }
            else if (prev_n3 != note)
            {
                prev_n3.Stop();
                if (prev_bw3 == true)
                {
                    prev_r3.material = M_white;
                }
                else
                {
                    prev_r3.material = M_black;
                }
                prev_bw3 = b_w;
                prev_o3 = octave;
                prev_p3 = num;
                prev_r3 = rend;
                prev_n3 = note;
                update_np(b_w, octave, num);
            }
        }
        else if (finger == 3)
        {
            if (prev_n4 == null)
            {
                prev_n4 = note;
                prev_r4 = rend;
                prev_p4 = num;
                prev_r4 = rend;
                prev_n4 = note;
            }
            else if (prev_n4 != note)
            {
                prev_n4.Stop();
                if (prev_bw4 == true)
                {
                    prev_r4.material = M_white;
                }
                else
                {
                    prev_r4.material = M_black;
                }
                prev_bw4 = b_w;
                prev_o4 = octave;
                prev_p4 = num;
                prev_r4 = rend;
                prev_n4 = note;
                update_np(b_w, octave, num);
            }
        }
        else if (finger == 4)
        {
            if (prev_n5 == null)
            {
                prev_n5 = note;
                prev_r5 = rend;
                prev_p5 = num;
                prev_r5 = rend;
                prev_n5 = note;
            }
            else if (prev_n5 != note)
            {
                prev_n5.Stop();
                if (prev_bw5 == true)
                {
                    prev_r5.material = M_white;
                }
                else
                {
                    prev_r5.material = M_black;
                }
                prev_bw5 = b_w;
                prev_o5 = octave;
                prev_p5 = num;
                prev_r5 = rend;
                prev_n5 = note;
                update_np(b_w, octave, num);
            }
        }
    }

    void update_np(bool bw, int oct, int num)
    {
        if(oct == 1)
        {
            if (bw == false)
            {
                np1b[num] = false;
            }
            else
            {
                np1w[num] = false;
            }
        }
        else if(oct == 2)
        {
            if (bw == false)
            {
                np2b[num] = false;
            }
            else
            {
                np2w[num] = false;
            }
        }
        else if (oct == 3)
        {
            if (bw == false)
            {
                np3b[num] = false;
            }
            else
            {
                np3w[num] = false;
            }
        }
        else if (oct == 4)
        {
            if (bw == false)
            {
                np4b[num] = false;
            }
            else
            {
                np4w[num] = false;
            }
        }
        else if (oct == 5)
        {
            if (bw == false)
            {
                np5b[num] = false;
            }
            else
            {
                np5w[num] = false;
            }
        }
        else if (oct == 6)
        {
            if (bw == false)
            {
                np6b[num] = false;
            }
            else
            {
                np6w[num] = false;
            }
        }
        else if (oct == 7)
        {
            if (bw == false)
            {
                np7b[num] = false;
            }
            else
            {
                np7w[num] = false;
            }
        }
        else
        {
            if (bw == false)
            {
                np8b = false;
            }
            else
            {
                np8w[num] = false;
            }
        }
    }

    bool note_check(float pos, AudioSource note, bool note_playing, Renderer rend, int octave, int n, int finger = 0, bool bw = true)
    {
        //If current note does not equal previous note,
        //Stop playing the previous note and set previous equal to current
        //Stop any previous note
        
        stop_prev(finger, note, rend, octave, n, bw);
        
        //For note not being played
        if (note_playing == false)
        {
            //Check if note is at or under hit region
            if (pos <= hit_height)
            {
                //Play note
                note.Play();
                if (bw == true)
                {
                    rend.material = M_pink;
                }
                else
                {
                    rend.material = M_purple;
                }
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
                if (bw == true)
                {
                    rend.material = M_white;
                }
                else
                {
                    rend.material = M_black;
                }
                note_playing = false;
            }
        }
        return note_playing;
    }

    //Changes a black key to purple
    void clr_purple(Renderer rend)
    {
        rend.material = M_purple;
    }

    //Changes a black key to black
    void clr_black(Renderer rend)
    {
        rend.material = M_black;
    }

    //Changes a white key to pink
    void clr_pink(Renderer rend)
    {
        rend.material = M_pink;
    }

    //Changes a white key to white
    void clr_white(Renderer rend)
    {
        rend.material = M_white;
    }
    
    //Loads audio files when called
    void load_objects()
    {
        //Getting notes

        //White keys
        wka1 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(0);
        wkb1 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(1);
        wkc1 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(2);
        wkd1 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(3);
        wke1 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(4);
        wkf1 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(5);
        wkg1 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(6);
        wka2 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(7);
        wkb2 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(8);
        wkc2 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(9);
        wkd2 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(10);
        wke2 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(11);
        wkf2 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(12);
        wkg2 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(13);
        wka3 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(14);
        wkb3 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(15);
        wkc3 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(16);
        wkd3 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(17);
        wke3 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(18);
        wkf3 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(19);
        wkg3 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(20);
        wka4 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(21);
        wkb4 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(22);
        wkc4 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(23);
        wkd4 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(24);
        wke4 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(25);
        wkf4 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(26);
        wkg4 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(27);
        wka5 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(28);
        wkb5 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(29);
        wkc5 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(30);
        wkd5 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(31);
        wke5 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(32);
        wkf5 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(33);
        wkg5 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(34);
        wka6 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(35);
        wkb6 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(36);
        wkc6 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(37);
        wkd6 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(38);
        wke6 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(39);
        wkf6 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(40);
        wkg6 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(41);
        wka7 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(42);
        wkb7 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(43);
        wkc7 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(44);
        wkd7 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(45);
        wke7 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(46);
        wkf7 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(47);
        wkg7 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(48);
        wka8 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(49);
        wkb8 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(50);
        wkc8 = transform.GetChild(1).transform.GetChild(1).transform.GetChild(51);

        //Black keys
        bkbb1 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(0);
        bkdb1 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(1);
        bkeb1 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(2);
        bkgb1 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(3);
        bkab1 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(4);
        bkbb2 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(5);
        bkdb2 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(6);
        bkeb2 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(7);
        bkgb2 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(8);
        bkab2 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(9);
        bkbb3 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(10);
        bkdb3 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(11);
        bkeb3 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(12);
        bkgb3 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(13);
        bkab3 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(14);
        bkbb4 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(15);
        bkdb4 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(16);
        bkeb4 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(17);
        bkgb4 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(18);
        bkab4 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(19);
        bkbb5 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(20);
        bkdb5 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(21);
        bkeb5 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(22);
        bkgb5 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(23);
        bkab5 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(24);
        bkbb6 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(25);
        bkdb6 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(26);
        bkeb6 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(27);
        bkgb6 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(28);
        bkab6 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(29);
        bkbb7 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(30);
        bkdb7 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(31);
        bkeb7 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(32);
        bkgb7 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(33);
        bkab7 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(34);
        bkbb8 = transform.GetChild(1).transform.GetChild(0).transform.GetChild(35);

        //Getting audio

        //White keys
        a1 = wka1.GetComponent<AudioSource>();
        b1 = wkb1.GetComponent<AudioSource>();
        c1 = wkc1.GetComponent<AudioSource>();
        d1 = wkd1.GetComponent<AudioSource>();
        e1 = wke1.GetComponent<AudioSource>();
        f1 = wkf1.GetComponent<AudioSource>();
        g1 = wkg1.GetComponent<AudioSource>();
        a2 = wka2.GetComponent<AudioSource>();
        b2 = wkb2.GetComponent<AudioSource>();
        c2 = wkc2.GetComponent<AudioSource>();
        d2 = wkd2.GetComponent<AudioSource>();
        e2 = wke2.GetComponent<AudioSource>();
        f2 = wkf2.GetComponent<AudioSource>();
        g2 = wkg2.GetComponent<AudioSource>();
        a3 = wka3.GetComponent<AudioSource>();
        b3 = wkb3.GetComponent<AudioSource>();
        c3 = wkc3.GetComponent<AudioSource>();
        d3 = wkd3.GetComponent<AudioSource>();
        e3 = wke3.GetComponent<AudioSource>();
        f3 = wkf3.GetComponent<AudioSource>();
        g3 = wkg3.GetComponent<AudioSource>();
        a4 = wka4.GetComponent<AudioSource>();
        b4 = wkb4.GetComponent<AudioSource>();
        c4 = wkc4.GetComponent<AudioSource>();
        d4 = wkd4.GetComponent<AudioSource>();
        e4 = wke4.GetComponent<AudioSource>();
        f4 = wkf4.GetComponent<AudioSource>();
        g4 = wkg4.GetComponent<AudioSource>();
        a5 = wka5.GetComponent<AudioSource>();
        b5 = wkb5.GetComponent<AudioSource>();
        c5 = wkc5.GetComponent<AudioSource>();
        d5 = wkd5.GetComponent<AudioSource>();
        e5 = wke5.GetComponent<AudioSource>();
        f5 = wkf5.GetComponent<AudioSource>();
        g5 = wkg5.GetComponent<AudioSource>();
        a6 = wka6.GetComponent<AudioSource>();
        b6 = wkb6.GetComponent<AudioSource>();
        c6 = wkc6.GetComponent<AudioSource>();
        d6 = wkd6.GetComponent<AudioSource>();
        e6 = wke6.GetComponent<AudioSource>();
        f6 = wkf6.GetComponent<AudioSource>();
        g6 = wkg6.GetComponent<AudioSource>();
        a7 = wka7.GetComponent<AudioSource>();
        b7 = wkb7.GetComponent<AudioSource>();
        c7 = wkc7.GetComponent<AudioSource>();
        d7 = wkd7.GetComponent<AudioSource>();
        e7 = wke7.GetComponent<AudioSource>();
        f7 = wkf7.GetComponent<AudioSource>();
        g7 = wkg7.GetComponent<AudioSource>();
        a8 = wka8.GetComponent<AudioSource>();
        b8 = wkb8.GetComponent<AudioSource>();
        c8 = wkc8.GetComponent<AudioSource>();

        //Black keys
        bb1 = bkbb1.GetComponent<AudioSource>();
        db1 = bkdb1.GetComponent<AudioSource>();
        eb1 = bkeb1.GetComponent<AudioSource>();
        gb1 = bkgb1.GetComponent<AudioSource>();
        ab1 = bkab1.GetComponent<AudioSource>();
        bb2 = bkbb2.GetComponent<AudioSource>();
        db2 = bkdb2.GetComponent<AudioSource>();
        eb2 = bkeb2.GetComponent<AudioSource>();
        gb2 = bkgb2.GetComponent<AudioSource>();
        ab2 = bkab2.GetComponent<AudioSource>();
        bb3 = bkbb3.GetComponent<AudioSource>();
        db3 = bkdb3.GetComponent<AudioSource>();
        eb3 = bkeb3.GetComponent<AudioSource>();
        gb3 = bkgb3.GetComponent<AudioSource>();
        ab3 = bkab3.GetComponent<AudioSource>();
        bb4 = bkbb4.GetComponent<AudioSource>();
        db4 = bkdb4.GetComponent<AudioSource>();
        eb4 = bkeb4.GetComponent<AudioSource>();
        gb4 = bkgb4.GetComponent<AudioSource>();
        ab4 = bkab4.GetComponent<AudioSource>();
        bb5 = bkbb5.GetComponent<AudioSource>();
        db5 = bkdb5.GetComponent<AudioSource>();
        eb5 = bkeb5.GetComponent<AudioSource>();
        gb5 = bkgb5.GetComponent<AudioSource>();
        ab5 = bkab5.GetComponent<AudioSource>();
        bb6 = bkbb6.GetComponent<AudioSource>();
        db6 = bkdb6.GetComponent<AudioSource>();
        eb6 = bkeb6.GetComponent<AudioSource>();
        gb6 = bkgb6.GetComponent<AudioSource>();
        ab6 = bkab6.GetComponent<AudioSource>();
        bb7 = bkbb7.GetComponent<AudioSource>();
        db7 = bkdb7.GetComponent<AudioSource>();
        eb7 = bkeb7.GetComponent<AudioSource>();
        gb7 = bkgb7.GetComponent<AudioSource>();
        ab7 = bkab7.GetComponent<AudioSource>();
        bb8 = bkbb8.GetComponent<AudioSource>();

        //Getting rendering components

        //White keys
        ra1 = wka1.GetComponent<Renderer>();
        rb1 = wkb1.GetComponent<Renderer>();
        rc1 = wkc1.GetComponent<Renderer>();
        rd1 = wkd1.GetComponent<Renderer>();
        re1 = wke1.GetComponent<Renderer>();
        rf1 = wkf1.GetComponent<Renderer>();
        rg1 = wkg1.GetComponent<Renderer>();
        ra2 = wka2.GetComponent<Renderer>();
        rb2 = wkb2.GetComponent<Renderer>();
        rc2 = wkc2.GetComponent<Renderer>();
        rd2 = wkd2.GetComponent<Renderer>();
        re2 = wke2.GetComponent<Renderer>();
        rf2 = wkf2.GetComponent<Renderer>();
        rg2 = wkg2.GetComponent<Renderer>();
        ra3 = wka3.GetComponent<Renderer>();
        rb3 = wkb3.GetComponent<Renderer>();
        rc3 = wkc3.GetComponent<Renderer>();
        rd3 = wkd3.GetComponent<Renderer>();
        re3 = wke3.GetComponent<Renderer>();
        rf3 = wkf3.GetComponent<Renderer>();
        rg3 = wkg3.GetComponent<Renderer>();
        ra4 = wka4.GetComponent<Renderer>();
        rb4 = wkb4.GetComponent<Renderer>();
        rc4 = wkc4.GetComponent<Renderer>();
        rd4 = wkd4.GetComponent<Renderer>();
        re4 = wke4.GetComponent<Renderer>();
        rf4 = wkf4.GetComponent<Renderer>();
        rg4 = wkg4.GetComponent<Renderer>();
        ra5 = wka5.GetComponent<Renderer>();
        rb5 = wkb5.GetComponent<Renderer>();
        rc5 = wkc5.GetComponent<Renderer>();
        rd5 = wkd5.GetComponent<Renderer>();
        re5 = wke5.GetComponent<Renderer>();
        rf5 = wkf5.GetComponent<Renderer>();
        rg5 = wkg5.GetComponent<Renderer>();
        ra6 = wka6.GetComponent<Renderer>();
        rb6 = wkb6.GetComponent<Renderer>();
        rc6 = wkc6.GetComponent<Renderer>();
        rd6 = wkd6.GetComponent<Renderer>();
        re6 = wke6.GetComponent<Renderer>();
        rf6 = wkf6.GetComponent<Renderer>();
        rg6 = wkg6.GetComponent<Renderer>();
        ra7 = wka7.GetComponent<Renderer>();
        rb7 = wkb7.GetComponent<Renderer>();
        rc7 = wkc7.GetComponent<Renderer>();
        rd7 = wkd7.GetComponent<Renderer>();
        re7 = wke7.GetComponent<Renderer>();
        rf7 = wkf7.GetComponent<Renderer>();
        rg7 = wkg7.GetComponent<Renderer>();
        ra8 = wka8.GetComponent<Renderer>();
        rb8 = wkb8.GetComponent<Renderer>();
        rc8 = wkc8.GetComponent<Renderer>();

        //Black keys
        rbb1 = bkbb1.GetComponent<Renderer>();
        rdb1 = bkdb1.GetComponent<Renderer>();
        reb1 = bkeb1.GetComponent<Renderer>();
        rgb1 = bkgb1.GetComponent<Renderer>();
        rab1 = bkab1.GetComponent<Renderer>();
        rbb2 = bkbb2.GetComponent<Renderer>();
        rdb2 = bkdb2.GetComponent<Renderer>();
        reb2 = bkeb2.GetComponent<Renderer>();
        rgb2 = bkgb2.GetComponent<Renderer>();
        rab2 = bkab2.GetComponent<Renderer>();
        rbb3 = bkbb3.GetComponent<Renderer>();
        rdb3 = bkdb3.GetComponent<Renderer>();
        reb3 = bkeb3.GetComponent<Renderer>();
        rgb3 = bkgb3.GetComponent<Renderer>();
        rab3 = bkab3.GetComponent<Renderer>();
        rbb4 = bkbb4.GetComponent<Renderer>();
        rdb4 = bkdb4.GetComponent<Renderer>();
        reb4 = bkeb4.GetComponent<Renderer>();
        rgb4 = bkgb4.GetComponent<Renderer>();
        rab4 = bkab4.GetComponent<Renderer>();
        rbb5 = bkbb5.GetComponent<Renderer>();
        rdb5 = bkdb5.GetComponent<Renderer>();
        reb5 = bkeb5.GetComponent<Renderer>();
        rgb5 = bkgb5.GetComponent<Renderer>();
        rab5 = bkab5.GetComponent<Renderer>();
        rbb6 = bkbb6.GetComponent<Renderer>();
        rdb6 = bkdb6.GetComponent<Renderer>();
        reb6 = bkeb6.GetComponent<Renderer>();
        rgb6 = bkgb6.GetComponent<Renderer>();
        rab6 = bkab6.GetComponent<Renderer>();
        rbb7 = bkbb7.GetComponent<Renderer>();
        rdb7 = bkdb7.GetComponent<Renderer>();
        reb7 = bkeb7.GetComponent<Renderer>();
        rgb7 = bkgb7.GetComponent<Renderer>();
        rab7 = bkab7.GetComponent<Renderer>();
        rbb8 = bkbb8.GetComponent<Renderer>();
    }
}
