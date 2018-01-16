using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.Events;


public class ArduinoConnection : MonoBehaviour
{

    // Callback signature
    // Has: Return of type 'void' and 2 parameters
    public delegate void TouchCallback(int pin, bool touched);

    // Event declaration
    public event TouchCallback OnBookTouched;


    void Start()
    {

    }

    void Update()
    {

    }


    void OnSerialLine(string line)
    {
        // Parse line
        char[] splitChar = {' '};
        string[] splitLine = line.Split(splitChar);
        int pin = int.Parse(splitLine[0]);
        bool touched = int.Parse(splitLine[1]) != 0;

        // Invoke the event
        if (OnBookTouched != null)
            OnBookTouched(pin,touched);
    }
}
