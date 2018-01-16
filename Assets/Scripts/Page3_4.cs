using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;
using DG.Tweening;

public class Page3_4 : MonoBehaviour
{
    // OUTLINE FOR PAGES 3 AND 4 // LIFESTYLE QUIZ

    // Player UI and game logic
    private string currentInkObject;
    private int numberOfCompletedInkObjects = 0, completedPageLimit = 5;
    private string chosenOption;
    private bool treeIsComplete, carIsComplete, trashIsComplete, cowIsComplete, airplaneIsComplete;
    public GameObject TreeUIDisplay1, TreeUIDisplay2, AirplaneUIDisplay, TrashUIDisplay, CowUIDisplay, CarUIDisplay;
    bool show = false;
    int treeRound = 0;

    // Airplane 
    public GameObject airplaneEffectsContintuously, airplaneEffectBurst_big, airplaneEffectBurst_mid, airplaneEffectBurst_small;



    // Ocean
    public GameObject ocean, ocean2, waterLight, waterLight2;
    public GameObject fish01, fish01Light, fish02, fish02Light, fish03, fish03Light, fish04, fish04Light, fish05, fish05Light;

    // Cloud parameters
    private float greenVarRain = 0.0f, greenVarClouds = 0.0f;
    private float increaseIntensity = 0.0f;
    // Cloud lights
    public GameObject cloudLightPage3, cloudLightPage4;
    // Cloud particle systems
    public ParticleSystem cloud01page3, cloud01page4;
    // StormCloud's rain particle systems
    public ParticleSystem psStormCloudRain01Page3, psStormCloudRain02Page3, psStormCloudRain01Page4, psStormCloudRain02Page4;
    // Boundary boxes not to render
    public GameObject top01, bottom01, left01, right01, top02, bottom02, left02, right02;

    // Trash 
    public GameObject trash1, trash2, trash3, trash4, trash5, trash6, trash7, trash8, trash9, trash10, trash11, trash12, trash13, trash14;
    private List<GameObject> trashList;
    private int trashToInstantiate = 0;
    public GameObject Landfill;
    public GameObject TrashSeaLight, TrashSeaLight2;

    // Intro title
    public GameObject intro1, intro2;
    bool introIsOn = false;


    void Start()
    {
        IntroScreenOn();

       


        // Make list of trash
        trashList = new List<GameObject>();
        trashList.Add(trash1);
        trashList.Add(trash2);
        trashList.Add(trash3);
        trashList.Add(trash11);
        trashList.Add(trash10);
        trashList.Add(trash9);
        trashList.Add(trash8);
        trashList.Add(trash7);
        trashList.Add(trash6);
        trashList.Add(trash5);
        trashList.Add(trash4);
        trashList.Add(trash12);
        trashList.Add(trash13);
        trashList.Add(trash14);
    }

    public void OnTouched(int pin, bool touched)
    {
        if (introIsOn == true)
        {
            introIsOn = false;
            intro1.SetActive(introIsOn);
            intro2.SetActive(introIsOn);


        }
        else
        {
            // nothing    
        }

        // What pin was touched?
        switch (pin)
        {
            case 0: // option A
                if (currentInkObject != null)
                {
                    chosenOption = "optionA";
                    CaseHandlerOptions(touched);
                }
                else
                {
                    print("Activate ink object before interacting with options");
                }
                break;
            case 1: // option B
                if (currentInkObject != null)
                {
                    chosenOption = "optionB";
                    CaseHandlerOptions(touched);
                }
                else
                {
                    print("Activate ink object before interacting with options");
                }
                break;
            case 2: // ink tree
                if (currentInkObject == null) // Check if another ink object alread is engaged
                {
                    if (treeIsComplete == false) // If tree question has not been completed yet
                    {
                        if (touched)
                        {
                            print("Tree touched");
                        }
                        else
                        {
                            currentInkObject = "tree";
                            TreeIsReleased();
                        }
                    }
                    else // Tree question has already been completed 
                    {
                        print("Tree is already completed");
                    }
                }
                else
                {
                    print("Non-option ink object '" + currentInkObject + "' already engaged");
                }
                break;
            case 5: // ink airplane
                if (currentInkObject == null)
                {
                    if (airplaneIsComplete == false)
                    {

                        if (touched)
                        {
                            print("Airplane touched");
                        }
                        else
                        {
                            currentInkObject = "airplane";
                            AirplaneIsReleased();
                        }
                    }
                    else
                    {
                        print("Airplane is already completed");
                    }
                }
                else
                {
                    print("Non-option ink object '" + currentInkObject + "' already engaged");
                }
                break;
            case 6: // option D
                if (currentInkObject != null)
                {
                    chosenOption = "optionD";
                    CaseHandlerOptions(touched);
                }
                else
                {
                    print("Activate ink object before interacting with options");
                }
                break;
            case 7: // option C

                if (currentInkObject != null)
                {
                    chosenOption = "optionC";
                    CaseHandlerOptions(touched);
                }
                else
                {
                    print("Activate ink object before interacting with options");
                }
                break;
            case 8: // ink Car
                if (currentInkObject == null)
                {
                    if (carIsComplete == false)
                    {

                        if (touched)
                        {
                            print("Car touched");
                        }
                        else
                        {
                            currentInkObject = "car";
                            CarIsReleased();
                        }
                    }
                    else
                    {
                        print("Car is already completed");
                    }
                }
                else
                {
                    print("Non-option ink object '" + currentInkObject + "' already engaged");
                }
                break;
            case 9: // ink Cow
                if (currentInkObject == null)
                {
                    if (cowIsComplete == false)
                    {

                        if (touched)
                        {
                            print("Cow touched");
                        }
                        else
                        {
                            currentInkObject = "cow";
                            CowIsReleased();
                        }
                    }
                    else
                    {
                        print("Cow is already completed");
                    }
                }
                else
                {
                    print("Non-option ink object '" + currentInkObject + "' already engaged");
                }
                break;
            case 10: // ink Trash can
                if (currentInkObject == null)
                {
                    if (trashIsComplete == false)
                    {

                        if (touched)
                        {
                            print("Trash touched");
                        }
                        else
                        {
                            currentInkObject = "trash";
                            TrashIsReleased();
                        }
                    }
                    else
                    {
                        print("Trash is already completed");
                    }
                }
                else
                {
                    print("Non-option ink object '" + currentInkObject + "' already engaged");
                }
                break;
        }

    }


    void IntroScreenOn()
    {
        introIsOn = true;
        print("Welcome to page 3-4!");
        intro1.SetActive(introIsOn);
        intro2.SetActive(introIsOn);
    }



    void CaseHandlerOptions(bool touched)
    {
        if (touched)
        {



            // Communicate to user that touch is recognized
            // Doesn't work if you choose the same option for a second ink object. Don't know why, but it is not necessary either. 
            //print(chosenOption + " touched");

        }
        else // Button is released. Will invoke visuals in corresponding 'option'-function
        {
            switch (currentInkObject)
            {
                case "tree": // tree is a special case since it consists of 2 questions. Solved it a bit messy, but it works
                    TreeOptionIsPressed();
                    break;

                case "car":
                    CarOptionIsPressed();
                    carIsComplete = true;
                    print("Car completed");
                    currentInkObject = null;
                    numberOfCompletedInkObjects += 1;

                    break;

                case "airplane":
                    AirplaneOptionIsPressed();
                    airplaneIsComplete = true;
                    print("Airplane completed");
                    currentInkObject = null;
                    numberOfCompletedInkObjects += 1;

                    break;

                case "cow":
                    CowOptionIsPressed();
                    cowIsComplete = true;
                    print("Cow completed");
                    currentInkObject = null;
                    numberOfCompletedInkObjects += 1;

                    break;

                case "trash":
                    TrashOptionIsPressed();
                    trashIsComplete = true;
                    print("Trash completed");
                    currentInkObject = null;
                    numberOfCompletedInkObjects += 1;

                    break;
            }
            // Clear currentInkObject and chosenOption when they are completed for a specific ink object
            chosenOption = "";
            if (numberOfCompletedInkObjects == completedPageLimit)
            {
                CompletedLifestyleQuiz();
            }

        }
    }




    void TreeIsReleased() // Is called if currentInkObject == "tree"
    {
        // Display tree UI stuff
        print("Tree activated");
        TreeDisplay1Toggle();
    }

    void TreeOptionIsPressed()
    {
        // Reset reusable lights etc for directing attention to certain changes
        ResetStates();

        if (treeRound == 0)
        {
            switch (chosenOption)
            {
                case "optionA":
                    // Remove tree UI stuff
                    // Change landscape
                    print("Tree: optionA activated");
                    break;
                case "optionB":
                    print("Tree: optionB activated");
                    break;
                case "optionC":
                    print("Tree: optionC activated");
                    break;
                case "optionD":
                    print("Tree: optionD activated");
                    break;
            }
            TreeDisplay1Toggle(); // hide Q1
            TreeDisplay2Toggle(); // show Q2 
            treeRound += 1;
        }
        else if (treeRound == 1)
        {
            switch (chosenOption)
            {
                case "optionA":
                    // Remove tree UI stuff
                    // Change landscape
                    print("Tree: optionA activated");
                    break;
                case "optionB":
                    print("Tree: optionB activated");
                    break;
                case "optionC":
                    print("Tree: optionC activated");
                    break;
                case "optionD":
                    print("Tree: optionD activated");
                    break;
            }
            TreeDisplay2Toggle();
            treeRound += 1;
            treeIsComplete = true;
            print("Tree completed");
            numberOfCompletedInkObjects += 1;
            currentInkObject = null;
        }


    }

    void TreeDisplay1Toggle()
    {
        show = !show;
        TreeUIDisplay1.SetActive(show);
        print("Will it show? " + show);
    }

    void TreeDisplay2Toggle()
    {
        show = !show;
        TreeUIDisplay2.SetActive(show);
        print("Will it show? " + show);
    }


    void CarIsReleased()
    {
        print("Car activated");
        show = !show;
        CarUIDisplay.SetActive(show);

    }

    void CarOptionIsPressed()
    {
        // Reset reusable lights etc for directing attention to certain changes
        ResetStates();

        switch (chosenOption)
        {
            case "optionA":
                print("Car: optionA activated");
                increaseIntensity = 3.2f;
                GreenerCloudsLight();
                KillFish04();
                ocean.transform.DOMove(new Vector3(ocean.transform.position.x, ocean.transform.position.y + 0.10f, ocean.transform.position.z), 3.0f);
                ocean2.transform.DOMove(new Vector3(ocean2.transform.position.x, ocean2.transform.position.y + 0.10f, ocean2.transform.position.z), 3.0f);

                MoveWaterLight2();
                break;
            case "optionB":
                print("Car: optionB activated");
                increaseIntensity = 1.0f;
                GreenerCloudsLight();
                break;
            case "optionC":
                print("Car: optionC activated");
                increaseIntensity = -2.0f;
                GreenerCloudsLight();
                KillFish03();
                break;
            case "optionD":
                print("Car: optionD activated");
                increaseIntensity = 3.0f;
                GreenerCloudsLight();

                break;
        }
        show = !show;
        CarUIDisplay.SetActive(show);
    }


    void AirplaneIsReleased()
    {
        print("Airplane activated");
        show = !show;
        AirplaneUIDisplay.SetActive(show);
    }

    void AirplaneOptionIsPressed()
    {
        // Reset reusable lights etc for directing attention to certain changes
        ResetStates();

        switch (chosenOption)
        {
            case "optionA":
                print("Airplane: optionA activated");
                airplaneEffectsContintuously.SetActive(false);
                ocean.transform.DOMove(new Vector3(ocean.transform.position.x, ocean.transform.position.y - 0.07f, ocean.transform.position.z), 3.0f);
                ocean2.transform.DOMove(new Vector3(ocean2.transform.position.x, ocean2.transform.position.y - 0.07f, ocean2.transform.position.z), 3.0f);

                MoveWaterLight();
                increaseIntensity = -2;
                GreenerCloudsLight();
                break;
            case "optionB":
                print("Airplane: optionB activated");
                airplaneEffectBurst_small.SetActive(true);
                ocean.transform.DOMove(new Vector3(ocean.transform.position.x, ocean.transform.position.y + 0.06f, ocean.transform.position.z), 3.0f);
                ocean2.transform.DOMove(new Vector3(ocean2.transform.position.x, ocean2.transform.position.y + 0.06f, ocean2.transform.position.z), 3.0f);

                MoveWaterLight();
                KillFish01();
                increaseIntensity = 1;
                GreenerCloudsLight();

                break;
            case "optionC":
                print("Airplane: optionC activated");
                airplaneEffectBurst_mid.SetActive(true);
                ocean.transform.DOMove(new Vector3(ocean.transform.position.x, ocean.transform.position.y + 0.08f, ocean.transform.position.z), 3.0f);
                ocean2.transform.DOMove(new Vector3(ocean2.transform.position.x, ocean2.transform.position.y + 0.08f, ocean2.transform.position.z), 3.0f);

                MoveWaterLight();
                increaseIntensity = 2.0f;
                GreenerCloudsLight();
                break;
            case "optionD":
                print("Airplane: optionD activated");
                airplaneEffectBurst_big.SetActive(true);
                ocean.transform.DOMove(new Vector3(ocean.transform.position.x, ocean.transform.position.y + 0.1f, ocean.transform.position.z), 3.0f);
                ocean2.transform.DOMove(new Vector3(ocean2.transform.position.x, ocean2.transform.position.y + 0.1f, ocean2.transform.position.z), 3.0f);

                MoveWaterLight();
                increaseIntensity = 2f;
                GreenerCloudsLight();
                KillFish05();
                break;
        }
        show = !show;
        AirplaneUIDisplay.SetActive(show);
    }

    void CowIsReleased()
    {
        print("Cow activated");
        show = !show;
        CowUIDisplay.SetActive(show);
    }

    void CowOptionIsPressed()
    {
        // Reset reusable lights etc for directing attention to certain changes
        ResetStates();

        switch (chosenOption)
        {
            case "optionA":
                print("Cow: optionA activated");
                break;
            case "optionB":
                print("Cow: optionB activated");
                break;
            case "optionC":
                print("Cow: optionC activated");
                break;
            case "optionD":
                print("Cow: optionD activated");
                break;
        }
        show = !show;
        CowUIDisplay.SetActive(show);
    }


    void TrashIsReleased()
    {
        print("Trash activated");
        show = !show;
        TrashUIDisplay.SetActive(show);
    }

    void TrashOptionIsPressed()
    {
        // Reset reusable lights etc for directing attention to certain changes
        ResetStates();

        switch (chosenOption)
        {
            case "optionA":
                print("Trash: optionA activated");
                increaseIntensity = -3f;
                GreenerCloudsLight();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();

                break;
            case "optionB":
                print("Trash: optionB activated");
                increaseIntensity = 2.2f;
                GreenerCloudsLight();
                InstantiateTrashAtSea();
                InstantiateTrashAtSea();
                InstantiateTrashAtSea();
                InstantiateTrashAtSea();
                InstantiateTrashAtSea();
                InstantiateTrashAtSea();
                InstantiateTrashAtSea();

                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();

                KillFish01();
                KillFish02();
                KillFish03();

                TrashSeaLight.SetActive(true);
                TrashSeaLight.transform.DOMoveY(12.72f, 10.0f);

                TrashSeaLight2.SetActive(true);
                TrashSeaLight2.transform.DOMoveY(12.72f, 10.0f);


                break;
            case "optionC":
                print("Trash: optionC activated");
                increaseIntensity = 0.8f;
                GreenerCloudsLight();
                KillFish02();

                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashInLandfill();
                InstantiateTrashAtSea();
                InstantiateTrashAtSea();
                InstantiateTrashAtSea();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();
                InstantiateTrashAtLand();



                break;
            case "optionD":
                print("Trash: optionD doesn't exist");
                break;
        }
        show = !show;
        TrashUIDisplay.SetActive(show);
    }


    void MoveWaterLight()
    {
        waterLight.SetActive(true);
        waterLight.transform.localPosition = new Vector3(-0.822f, 5.057f, 1.175f);
        waterLight.transform.DOMoveY(waterLight.transform.position.y + 20.0f, 10.0f);
    }

    void MoveWaterLight2()
    {
        waterLight2.SetActive(true);
        waterLight2.transform.localPosition = new Vector3(-0.822f, 5.057f, 1.175f);
        waterLight2.transform.DOMoveY(waterLight.transform.position.y + 20.0f, 10.0f);
    }


    void KillFish01()
    {
        fish01Light.SetActive(true);
        fish01.transform.DOMove(new Vector3(fish01.transform.position.x + 1.1f, fish01.transform.position.y + 0.454f, fish01.transform.position.z + 0.19f), 5.0f);
        fish01.transform.DORotate(new Vector3(-0.267f, 90.771f, 198.536f), 10.0f);
    }
    void KillFish02()
    {
        fish02Light.SetActive(true);
        fish02.transform.DOMove(new Vector3(fish02.transform.position.x, fish02.transform.position.y + 0.258f, fish02.transform.position.z), 5.0f);
        fish02.transform.DORotate(new Vector3(-0.267f, 90.771f, 198.536f), 10.0f);
    }
    void KillFish03()
    {
        fish03Light.SetActive(true);
        fish03.transform.DOMove(new Vector3(fish01.transform.position.x, fish03.transform.position.y + 0.188f, fish03.transform.position.z), 5.0f);
        fish03.transform.DORotate(new Vector3(-0.267f, 90.771f, 198.536f), 10.0f);
    }
    void KillFish04()
    {
        fish04Light.SetActive(true);
        fish04.transform.DOMove(new Vector3(fish01.transform.position.x, fish04.transform.position.y + 0.49f, fish04.transform.position.z), 5.0f);
        fish04.transform.DORotate(new Vector3(-0.267f, 90.771f, 198.536f), 10.0f);
    }
    void KillFish05()
    {
        fish05Light.SetActive(true);
        fish05.transform.DOMove(new Vector3(fish01.transform.position.x, fish05.transform.position.y + 0.365f, fish05.transform.position.z), 5.0f);
        fish05.transform.DORotate(new Vector3(-0.267f, 90.771f, 198.536f), 10.0f);
    }


    void GreenerCloudsLight()
    {
        // Increase light intensity
        cloudLightPage3.GetComponent<Light>().intensity += 0.15f * increaseIntensity;
        cloudLightPage4.GetComponent<Light>().intensity += 0.15f * increaseIntensity;

        // Rain color 
        greenVarRain = 10.0f / 255.0f * increaseIntensity;

        var mainModule = psStormCloudRain01Page3.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(0.8f + (-1) * greenVarRain, 0.8f, 0.8f + (-1) * greenVarRain, 1.0f));

        mainModule = psStormCloudRain02Page3.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(0.8f + (-1) * greenVarRain, 0.8f, 0.8f + (-1) * greenVarRain, 1.0f));

        mainModule = psStormCloudRain01Page4.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(0.8f + (-1) * greenVarRain, 0.8f, 0.8f + (-1) * greenVarRain, 1.0f));

        mainModule = psStormCloudRain02Page4.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(0.8f + (-1) * greenVarRain, 0.8f, 0.8f + (-1) * greenVarRain, 1.0f));


        // Cloud color
        greenVarClouds = 30.0f / 255.0f * increaseIntensity;
        float newColorComponent = 206.0f / 255.0f + (-1) * greenVarClouds;
        if (newColorComponent > 206.0f * 255.0f)
        {
            newColorComponent = 206.0f * 255.0f;
        }

        mainModule = cloud01page3.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(newColorComponent, 206.0f / 255.0f, newColorComponent, 1.0f));

        mainModule = cloud01page4.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(newColorComponent, 206.0f / 255.0f, newColorComponent, 1.0f));


    }


    void InstantiateTrashInLandfill()
    {
        GameObject clone = (GameObject)Instantiate(trashList[trashToInstantiate], new Vector3(0, 0, 0), Quaternion.identity);
        clone.transform.SetParent(Landfill.transform);
        clone.transform.localPosition = new Vector3(Random.Range(-3.94f, -1.58f), 1.52f, Random.Range(7.56f, 10.69f));
        clone.transform.localScale = new Vector3(2, 2, 2);
        clone.transform.rotation = Random.rotation;
        trashToInstantiate = (int)Random.Range(0, trashList.Count());
    }

    void InstantiateTrashAtSea()
    {
        GameObject clone = (GameObject)Instantiate(trashList[trashToInstantiate], new Vector3(0, 0, 0), Quaternion.identity);
        clone.transform.SetParent(ocean.transform);
        clone.transform.localPosition = new Vector3(Random.Range(-1.117f, -0.747f), 2.282f, Random.Range(0.103f, 0.409f));
        clone.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        clone.transform.rotation = Random.rotation;
        trashToInstantiate = (int)Random.Range(0, trashList.Count());
    }

    void InstantiateTrashAtLand()
    {
        GameObject clone = (GameObject)Instantiate(trashList[trashToInstantiate], new Vector3(0, 0, 0), Quaternion.identity);
        clone.transform.SetParent(Landfill.transform);
        clone.transform.localPosition = new Vector3(Random.Range(4.5f, 8.47f), -7.45f, Random.Range(-8.56f, -6.08f));
        clone.transform.localScale = new Vector3(1, 1, 1);
        clone.transform.rotation = Random.rotation;
        trashToInstantiate = (int)Random.Range(0, trashList.Count());
    }


    void ResetStates()
    {
        // WaterLight
        waterLight.SetActive(false);
        waterLight.transform.position = new Vector3(-0.822f, 4.638f, 1.175f);

        // Turn off fishLight
        fish01Light.SetActive(false);
        fish02Light.SetActive(false);
        fish03Light.SetActive(false);
        fish04Light.SetActive(false);
        fish05Light.SetActive(false);

    }

    void CompletedLifestyleQuiz() // When the game is completed
    {
        // Show something
        print("Congrats! Game is over! Take a look at yourself, you ruin earth! Thanks!");
    }

}
