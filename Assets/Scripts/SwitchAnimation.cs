using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchAnimation : MonoBehaviour {

    public UnityEvent AnimEndEvent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchOnEnd(){
        AnimEndEvent.Invoke();
    }
}
