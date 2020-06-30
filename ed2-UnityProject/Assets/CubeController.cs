using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private HFController controllerInput;
    
    //Cube game objects currently being used for movement testing
    //It is assumed that these are child objects of the GameObject this script is a component of
    public GameObject cube0;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    
    
    // Start is called before the first frame update
    void Start()
    {
        controllerInput = new HFController();
        
        //Find test cube game Objects in hierarchy
        cube0 = transform.Find("0").gameObject;
        cube1 = transform.Find("1").gameObject;
        cube2 = transform.Find("2").gameObject;
        cube3 = transform.Find("3").gameObject;
        cube4 = transform.Find("4").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerInput.IsConnected)
        {
            ProcessMovement();
            controllerInput.PrintReadings();
        }
        else
        {
            
        }
    }
    
    private void ProcessMovement()
    {
        if (controllerInput.GetSensorValue(0) > 800)
        {
            cube0.transform.position = cube0.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
        if (controllerInput.GetSensorValue(1) > 800)
        {
            cube1.transform.position = cube1.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
        if (controllerInput.GetSensorValue(2) > 800)
        {
            cube2.transform.position = cube2.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
        if (controllerInput.GetSensorValue(3) > 800)
        {
            cube3.transform.position = cube3.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
        if (controllerInput.GetSensorValue(4) > 800)
        {
            cube4.transform.position = cube4.transform.position + new Vector3(5f * Time.deltaTime, 0, 0);
        }
    }
}
