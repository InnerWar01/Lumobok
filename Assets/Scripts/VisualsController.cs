using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class VisualsController : MonoBehaviour
{

    public ArduinoConnection arduinoConnection;
    public Camera mainCam;
    //public List<GameObject> visuals;

    [Space(10f)]

    public CoverController cover;

    [Space(10f)]

    public bool bookON;

    private List<TrackableBehaviour> activeTrackables;
    private string lastPage;
    private string currentPage;

    [Space(10f)]

    public Page0 page0;
    public Page1_2 page1_2;
    public Page3_4 page3_4;


    private void Awake()
    {
        arduinoConnection.OnBookTouched += OnTouched;
        mainCam.backgroundColor = Color.white;
    }


    void Start()
    {
        // Start with all visuals deactivated
        //foreach (GameObject visual in visuals)
        //{
        //    visual.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // Get list of active trackables
        StateManager sm = TrackerManager.Instance.GetStateManager();
        activeTrackables = sm.GetActiveTrackableBehaviours().ToList();
        if(activeTrackables.Count>0){
            mainCam.backgroundColor = Color.black;
        }
    }

    public void OnTouched(int pin, bool touched)
    {

        // TODO: implement double pages!!!

        // If at least one page is detected
        if (activeTrackables.Count > 0)
        {

            // Assuming we only have one page
            TrackableBehaviour trackablePage = activeTrackables[0];

            if (touched)
                print("Touched pin " + pin + " on book page " + trackablePage.TrackableName);
            else
                print("Untouched pin " + pin + " on book page " + trackablePage.TrackableName);

            // Switch condition for each page / tracker ID
            currentPage = trackablePage.TrackableName;


            // TODO: stop pages that are lost
            //CheckIfPageChanged();


            // IF statements for each page (not else if, because two can happen at once)
            if (currentPage == "trackercover")
            {
                switch (pin)
                {

                    case 1: // pin that the "light switch" is connected to

                        if (touched)
                        {
                            // If book is off, turn it on
                            if (!bookON)
                            {
                                cover.TurnOn();
                            }
                            // If book is on, turn it off
                            else
                            {
                                cover.TurnOff();
                            }
                        }
                        break;
                }
            }


            // TODO: add bookON condition, decide what to do if book is off on other pages, are they locked, show "turn on" message? or simply dont work?

            if (bookON)
            {

                if (currentPage == "trackerpage0")
                {
                    page0.OnTouched(pin,touched);
                }
                if (currentPage == "trackerpage1" || currentPage == "trackerpage2")
                {
                    page1_2.OnTouched(pin, touched);
                }
                if (currentPage == "trackerpage3" || currentPage == "trackerpage4")
                {
                    page3_4.OnTouched(pin, touched);
                }
                if (currentPage == "trackerpage5")
                {
                    // Not implemented
                }

            }
        }

        //// If it is a pair of pages
        //else if (activeTrackables.Count == 2)
        //{
        //    TrackableBehaviour trackablePage0 = activeTrackables[0];
        //    TrackableBehaviour trackablePage1 = activeTrackables[1];

        //    if (touched)
        //        print("Touched pin " + pin + " on double book page number " + trackablePage0.Trackable.ID + " - " + trackablePage1.Trackable.ID);
        //    else
        //        print("Untouched pin " + pin + " on double book page number " + trackablePage0.Trackable.ID + " - " + trackablePage1.Trackable.ID);

        //}


    }


    // TODO: redo this

    void CheckIfPageChanged()
    {
        if (lastPage != currentPage)
        {
            // Page changed!
            lastPage = currentPage;
            StopAllVisuals();
        }
        else
        {
            // Still on same page
        }
    }

    void StopAllVisuals()
    {
        // Stop all visuals
        //foreach (GameObject visual in visuals)
        //{
        //    if (visual != null)
        //        visual.SetActive(false);
        //}
    }



}
