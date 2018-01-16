using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTouch : MonoBehaviour {

    public VisualsController visuals;


	void Start () {
		
	}
	

	void Update () {

        // DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            visuals.OnTouched(0, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            visuals.OnTouched(0, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            visuals.OnTouched(1, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            visuals.OnTouched(1, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            visuals.OnTouched(2, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            visuals.OnTouched(2, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            visuals.OnTouched(3, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            visuals.OnTouched(3, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            visuals.OnTouched(4, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            visuals.OnTouched(4, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            visuals.OnTouched(5, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            visuals.OnTouched(5, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            visuals.OnTouched(6, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            visuals.OnTouched(6, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            visuals.OnTouched(7, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            visuals.OnTouched(7, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            visuals.OnTouched(8, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            visuals.OnTouched(8, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            visuals.OnTouched(9, true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            visuals.OnTouched(9, false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            visuals.OnTouched(10, true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            visuals.OnTouched(10, false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            visuals.OnTouched(11, true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            visuals.OnTouched(11, false);
        }
	}
}
