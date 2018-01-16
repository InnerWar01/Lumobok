using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class VuVisualController : MonoBehaviour
{

    public ArduinoConnection arduinoConnection;
    public List<GameObject> visuals;

    private List<VuMarkBehaviour> activeVuMarks;
    private int lastPage = 0;
    private int currentPage = 0;


    private void Awake()
    {
        arduinoConnection.OnBookTouched += OnTouched;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get list of active trackables
        StateManager sm = TrackerManager.Instance.GetStateManager();
        VuMarkManager vm = sm.GetVuMarkManager();
        activeVuMarks = vm.GetActiveBehaviours().ToList();
    }

    void OnTouched(int pin, bool touched)
    {

        // If any pages in the book are tracked
        if (activeVuMarks.Count > 0)
        {
            // There are only one or two pages tracked at once
            VuMarkBehaviour trackablePage0 = activeVuMarks[0];
            VuMarkBehaviour trackablePage1 = activeVuMarks[1];


            // TODO: implement double pages

            print("Touched event on book page number " + trackablePage0.Trackable.ID);

            // Switch condition for each page / tracker ID
            currentPage = trackablePage0.Trackable.ID;
            CheckIfPageChanged();

            // Switch between the detected page
            switch (currentPage)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3: // We're on page 3

                    // What pin was touched?
                    switch (pin)
                    {
                        case 0:
                            if (touched)
                            {
                                print("touched 0: now a visual will be activated");
                                visuals[2].SetActive(true);
                            }
                            else
                            {
                                print("UN touched 0: now a visual will be deactivated");
                                visuals[2].SetActive(false);
                            }
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                    }
                    break;
                case 4: // We're on page 4

                    // What pin was touched?
                    switch (pin)
                    {
                        case 0:
                            if (touched)
                            {
                                print("touched 0: now a visual will be activated");
                                visuals[3].SetActive(true);
                            }
                            else
                            {
                                print("UN touched 0: now a visual will be deactivated");
                                visuals[3].SetActive(false);
                            }
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                    }
                    break;


            }




        }

    }

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
        foreach (GameObject visual in visuals)
        {
            if (visual != null)
                visual.SetActive(false);
        }
    }



}
