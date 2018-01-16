using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class Page0 : MonoBehaviour
{
    // OUTLINE FOR PAGE 0 // ABCD QUIZ

    //public ArduinoConnection arduinoConnection;

    //private List<TrackableBehaviour> activeTrackables;

    private char[] answersKey = new char[] { 'a', 'a', 'c', 'c', 'b', 'a', 'd', 'b', 'c', 'd' };
    //A - 8; B - 6; C - 9; D - 7;
    private int[] answersPin = new int[] { 8, 8, 9, 9, 6, 8, 7, 6, 9, 7 };

    private int nbQuestion = 1;

    private int score = 0;

    private Sprite[] question1 = new Sprite[3];
    private Sprite[] question2 = new Sprite[3];
    private Sprite[] question3 = new Sprite[3];
    private Sprite[] question4 = new Sprite[3];
    private Sprite[] question5 = new Sprite[3];
    private Sprite[] question6 = new Sprite[3];
    private Sprite[] question7 = new Sprite[3];
    private Sprite[] question8 = new Sprite[3];
    private Sprite[] question9 = new Sprite[3];
    private Sprite[] question10 = new Sprite[3];
    private Sprite[] result = new Sprite[5];

    private Sprite[][] questionsSprites;

    private GameObject currentProjection;
    private GameObject fireworks;

    #region Sound
    private AudioSource answerSound;
    private AudioClip answerCorrect;
    private AudioClip answerIncorrect;
    private AudioClip quizPassed;
    private AudioClip quizFailed;
    #endregion

    private void Awake()
    {
        //arduinoConnection.OnBookTouched += OnTouched;

        // load all frames in question* array
        question1[0] = Resources.Load<Sprite>(path: "Sprites/page0_q1_1");
        question1[1] = Resources.Load<Sprite>(path: "Sprites/page0_q1_2");
        question1[2] = Resources.Load<Sprite>(path: "Sprites/page0_q1_3");

        question2[0] = Resources.Load<Sprite>(path: "Sprites/page0_q2_1");
        question2[1] = Resources.Load<Sprite>(path: "Sprites/page0_q2_2");
        question2[2] = Resources.Load<Sprite>(path: "Sprites/page0_q2_3");

        question3[0] = Resources.Load<Sprite>(path: "Sprites/page0_q3_1");
        question3[1] = Resources.Load<Sprite>(path: "Sprites/page0_q3_2");
        question3[2] = Resources.Load<Sprite>(path: "Sprites/page0_q3_3");

        question4[0] = Resources.Load<Sprite>(path: "Sprites/page0_q4_1");
        question4[1] = Resources.Load<Sprite>(path: "Sprites/page0_q4_2");
        question4[2] = Resources.Load<Sprite>(path: "Sprites/page0_q4_3");

        question5[0] = Resources.Load<Sprite>(path: "Sprites/page0_q5_1");
        question5[1] = Resources.Load<Sprite>(path: "Sprites/page0_q5_2");
        question5[2] = Resources.Load<Sprite>(path: "Sprites/page0_q5_3");

        question6[0] = Resources.Load<Sprite>(path: "Sprites/page0_q6_1");
        question6[1] = Resources.Load<Sprite>(path: "Sprites/page0_q6_2");
        question6[2] = Resources.Load<Sprite>(path: "Sprites/page0_q6_3");

        question7[0] = Resources.Load<Sprite>(path: "Sprites/page0_q7_1");
        question7[1] = Resources.Load<Sprite>(path: "Sprites/page0_q7_2");
        question7[2] = Resources.Load<Sprite>(path: "Sprites/page0_q7_3");

        question8[0] = Resources.Load<Sprite>(path: "Sprites/page0_q8_1");
        question8[1] = Resources.Load<Sprite>(path: "Sprites/page0_q8_2");
        question8[2] = Resources.Load<Sprite>(path: "Sprites/page0_q8_3");

        question9[0] = Resources.Load<Sprite>(path: "Sprites/page0_q9_1");
        question9[1] = Resources.Load<Sprite>(path: "Sprites/page0_q9_2");
        question9[2] = Resources.Load<Sprite>(path: "Sprites/page0_q9_3");

        question10[0] = Resources.Load<Sprite>(path: "Sprites/page0_q10_1");
        question10[1] = Resources.Load<Sprite>(path: "Sprites/page0_q10_2");
        question10[2] = Resources.Load<Sprite>(path: "Sprites/page0_q10_3");

        result[0] = Resources.Load<Sprite>(path: "Sprites/page0_score_very_good");
        result[1] = Resources.Load<Sprite>(path: "Sprites/page0_score_good");
        result[2] = Resources.Load<Sprite>(path: "Sprites/page0_score_pretty_good");
        result[3] = Resources.Load<Sprite>(path: "Sprites/page0_score_not_too_bad");
        result[4] = Resources.Load<Sprite>(path: "Sprites/page0_score_fail");

        questionsSprites = new Sprite[][] { question1, question2, question3, question4, question5, question6, question7, question8, question9, question10, result };

        answerCorrect = Resources.Load("Sounds/correct_answer", typeof(AudioClip)) as AudioClip;
        answerIncorrect = Resources.Load("Sounds/incorrect_answer", typeof(AudioClip)) as AudioClip;
        quizPassed = Resources.Load("Sounds/quiz_passed", typeof(AudioClip)) as AudioClip;
        quizFailed = Resources.Load("Sounds/quiz_failed", typeof(AudioClip)) as AudioClip;
    }


    void Start()
    {
        currentProjection = GameObject.Find("ImageTarget_0/Canvas/Projection");
        //var currentImage = currentProjection.GetComponent<UnityEngine.UI.Image>();
        answerSound = currentProjection.GetComponent<AudioSource>();

        fireworks = GameObject.Find("ImageTarget_0/Canvas/Fireworks");
        fireworks.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get list of active trackables
        //StateManager sm = TrackerManager.Instance.GetStateManager();
        //activeTrackables = sm.GetActiveTrackableBehaviours().ToList();

        if (Input.GetKeyDown("a"))
            StartCoroutine(GetNextSprite('a', nbQuestion));
        else if (Input.GetKeyDown("b"))
            StartCoroutine(GetNextSprite('b', nbQuestion));
        else if (Input.GetKeyDown("c"))
            StartCoroutine(GetNextSprite('c', nbQuestion));
        else if (Input.GetKeyDown("d"))
            StartCoroutine(GetNextSprite('d', nbQuestion));
    }

    public void OnTouched(int pin, bool touched)
    {

        // If it is a single page
        //if (activeTrackables.Count == 1)
        //{
            //TrackableBehaviour trackablePage = activeTrackables[0];
            //print("Touched event on book page number " + trackablePage.Trackable.ID);

            StartCoroutine(GetNextSprite(pin, nbQuestion));

            // What pin was touched?
            switch (pin)
            {
                // TODO: add only pins on page 0 (A, B, C, D quiz)
                case 0:
                    if (touched)
                    {
                        print("touched 0: now a visual will be activated");
                    }
                    else
                    {
                        print("UN touched 0: now a visual will be deactivated");
                    }
                    break;
                case 1:
                    if (touched)
                    {
                        print("touched 0: now a visual will be activated");
                    }
                    else
                    {
                        print("UN touched 0: now a visual will be deactivated");
                    }
                    break;
                case 2:
                    if (touched)
                    {
                        print("touched 0: now a visual will be activated");
                    }
                    else
                    {
                        print("UN touched 0: now a visual will be deactivated");
                    }
                    break;
                case 3:
                    if (touched)
                    {
                        print("touched 0: now a visual will be activated");
                    }
                    else
                    {
                        print("UN touched 0: now a visual will be deactivated");
                    }
                    break;
            }

        //}


    }

    IEnumerator GetNextSprite(char key, int question)
    {
        print("For question " + question + " you answered " + key);
        if (question < answersKey.Length)
        {
            if (key == answersKey[question - 1])
            {
                answerSound.clip = answerCorrect;
                answerSound.Play();

                print("You are correct");
                score++;
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question - 1][1];
                yield return new WaitForSeconds(5.0f);
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[nbQuestion][0];
            }
            else
            {
                answerSound.clip = answerIncorrect;
                answerSound.Play();

                print("You are incorrect");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question - 1][2];
                yield return new WaitForSeconds(5.0f);
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[nbQuestion][0];
            }
            nbQuestion++;
        }
        else if (question == answersKey.Length)
        {
            if (key == answersKey[question - 1])
            {
                answerSound.clip = answerCorrect;
                answerSound.Play();

                print("You are correct");
                score++;
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question - 1][1];
                yield return new WaitForSeconds(5.0f);
            }
            else
            {
                answerSound.clip = answerIncorrect;
                answerSound.Play();

                print("You are incorrect");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question - 1][2];
                yield return new WaitForSeconds(5.0f);
            }

            print("Your score is " + score);

            if (score >= 8 && score <= 10)
            {
                answerSound.clip = quizPassed;
                answerSound.Play();

                print("Very good");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][0];

                fireworks.SetActive(true);
            }
            else if (score == 7)
            {
                answerSound.clip = quizPassed;
                answerSound.Play();

                print("Good");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][1];

                fireworks.SetActive(true);
            }
            else if (score == 6)
            {
                answerSound.clip = quizPassed;
                answerSound.Play();

                print("Pretty good");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][2];

                fireworks.SetActive(true);
            }
            else if (score == 5)
            {
                answerSound.clip = quizPassed;
                answerSound.Play();

                print("Not too bad");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][3];

                fireworks.SetActive(true);
            }
            else if (score >= 0 && score < 5)
            {
                answerSound.clip = quizFailed;
                answerSound.Play();

                print("Fail");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][4];
            }

            nbQuestion++;
        }
        else
        {
            print("Error, no more questions.");
        }
    }

    IEnumerator GetNextSprite(int pin, int question)
    {
        print("For question " + question + " you answered " + pin);
        if (question < answersPin.Length)
        {
            if (pin == answersPin[question - 1])
            {
                answerSound.clip = answerCorrect;
                answerSound.Play();

                print("You are correct");
                score++;
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question - 1][1];
                yield return new WaitForSeconds(5.0f);
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[nbQuestion][0];
            }
            else
            {
                answerSound.clip = answerIncorrect;
                answerSound.Play();

                print("You are incorrect");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question - 1][2];
                yield return new WaitForSeconds(5.0f);
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[nbQuestion][0];
            }
            nbQuestion++;
        }
        else if (question == answersPin.Length)
        {
            if (pin == answersPin[question - 1])
            {
                answerSound.clip = answerCorrect;
                answerSound.Play();

                print("You are correct");
                score++;
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question - 1][1];
                yield return new WaitForSeconds(5.0f);
            }
            else
            {
                answerSound.clip = answerIncorrect;
                answerSound.Play();

                print("You are incorrect");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question - 1][2];
                yield return new WaitForSeconds(5.0f);
            }

            print("Your score is " + score);

            if (score >= 8 && score <= 10)
            {
                answerSound.clip = quizPassed;
                answerSound.Play();

                print("Very good");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][0];
            }
            else if (score == 7)
            {
                answerSound.clip = quizPassed;
                answerSound.Play();

                print("Good");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][1];
            }
            else if (score == 6)
            {
                answerSound.clip = quizPassed;
                answerSound.Play();

                print("Pretty good");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][2];
            }
            else if (score == 5)
            {
                answerSound.clip = quizPassed;
                answerSound.Play();

                print("Not too bad");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][3];
            }
            else if (score >= 0 && score < 5)
            {
                answerSound.clip = quizFailed;
                answerSound.Play();

                print("Fail");
                currentProjection.GetComponent<UnityEngine.UI.Image>().sprite = questionsSprites[question][4];
            }

            nbQuestion++;
        }
        else
        {
            print("Error, no more questions.");
        }
    }
}