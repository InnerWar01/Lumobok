using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuPage : MonoBehaviour, ITrackableEventHandler {
    
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
