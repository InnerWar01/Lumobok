using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverController : MonoBehaviour {

    public VisualsController visuals;

    public GameObject offVisuals;
    public GameObject onVisuals;

    public SwitchAnimation switchAnim;

    public AudioSource audioSource;

	void Start () {
        onVisuals.SetActive(false);
        offVisuals.SetActive(true);

        switchAnim.AnimEndEvent.AddListener(BookOn);
	}
	
	void Update () {
		
	}

    public void TurnOn(){
        audioSource.Play();
        offVisuals.SetActive(false);
        onVisuals.SetActive(true);
    }

    public void TurnOff(){
        audioSource.Play();
        onVisuals.SetActive(false);
        offVisuals.SetActive(true);
        visuals.bookON = false;
    }

    void BookOn(){
        // At the end of the on animation, turn the book on in the visualscontroller
        print("book is on!");
        visuals.bookON = true;
    }


}
