using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Linq;

public class Page1_2 : MonoBehaviour
{
    //// OUTLINE FOR PAGES 1 AND 2 // +- LEVELS

    //public ArduinoConnection arduinoConnection;

    public GameObject mainCanvas;
    public Text answerP1, answerP2, scoreText_p1, scoreText_p2, startText1, startText2, 
        questionBig1, questionBig2, questionN1, questionN2;
    public List<Text> answers1, answers2;
    public List<Text> timer;
    public List<GameObject> gameCanvas, questionCanvas, startCanvas;
    public List<Transform> percentBars, percentBars2;

    //private List<TrackableBehaviour> activeTrackables;
    //private string currentPage;

    private int nquestion; // Index of the current question
    private int score_p1, score_p2;
    private float ans_p1, ans_p2;
    private int action1, action2; // Defines the state for answers 1:increasing, 2:decreasing, 0:stopped
    private int playing; // Playing/game over state
    private bool countdown; // State for the timer (counting down or stopped)
    private double timeLeft;
    private double counterTime;
    private int totalScore;
    private float speed;

    private string[] question;
    private float[] correctAnswer;

    private AudioSource gameSound; //, questionSound;
    private AudioClip countdownSound, popSound, popSolutionSound, ascendSound, multimediaSound, pointsSound, winSound, loseSound;

    private void Awake()
    {
        //arduinoConnection.OnBookTouched += OnTouched;

        countdownSound = Resources.Load("Sounds/countdown", typeof(AudioClip)) as AudioClip;
        popSound = Resources.Load("Sounds/pop", typeof(AudioClip)) as AudioClip;
        popSolutionSound = Resources.Load("Sounds/popSolution", typeof(AudioClip)) as AudioClip;
        ascendSound = Resources.Load("Sounds/ascend2", typeof(AudioClip)) as AudioClip;
        multimediaSound = Resources.Load("Sounds/multimedia1", typeof(AudioClip)) as AudioClip;
        pointsSound = Resources.Load("Sounds/points", typeof(AudioClip)) as AudioClip;
        winSound = Resources.Load("Sounds/quiz_passed", typeof(AudioClip)) as AudioClip;
        loseSound = Resources.Load("Sounds/quiz_failed", typeof(AudioClip)) as AudioClip;


    }

    void Start()
    {
        playing = 0; // The game will start after pressing a button (4)

        totalScore = 4; // This is the total score to win the game
        speed = 0.5f;
        counterTime = 15; // Time in seconds to answer the questions

        gameSound = mainCanvas.GetComponent<AudioSource>();
        //gameSound = questionCanvas[0].GetComponent<AudioSource>();


        DefineQuestions(); // Sets the questions and correct answers
    }

    void Update()
    {
        // Get list of active trackables
        //StateManager sm = TrackerManager.Instance.GetStateManager();
        //activeTrackables = sm.GetActiveTrackableBehaviours().ToList();

        if (countdown)
        {
            // Increase/decrease answer
            if (action1 == 1 & ans_p1 <= 100)
            {
                ans_p1 += speed; // speed of value increase/decrease
                answerP1.text = (int)ans_p1 + "%"; // Value rounded up to int
                percentBars[0].GetComponent<UnityEngine.UI.Image>().fillAmount = ans_p1 / 100;
            }
            else if (action1 == 2 & ans_p1 >= 0)
            {
                ans_p1 -= speed;
                answerP1.text = (int)ans_p1 + "%";
                percentBars[0].GetComponent<UnityEngine.UI.Image>().fillAmount = ans_p1 / 100;
            }

            if (action2 == 1 & ans_p2 <= 100)
            {
                ans_p2 += speed; // speed of value increase/decrease
                answerP2.text = (int)ans_p2 + "%"; // Value rounded up to int
                percentBars2[0].GetComponent<UnityEngine.UI.Image>().fillAmount = ans_p2 / 100;
            }
            else if (action2 == 2 & ans_p2 >= 0)
            {
                ans_p2 -= speed;
                answerP2.text = (int)ans_p2 + "%";
                percentBars2[0].GetComponent<UnityEngine.UI.Image>().fillAmount = ans_p2 / 100;
            }

            //Countdown
            timeLeft -= Time.deltaTime;
            if (timeLeft > 0)
            {
                timer[0].text = (int)timeLeft + "s";
                timer[1].text = (int)timeLeft + "s";
            }

            else
            {
                //timer[0].text = "Time's up!";
                //timer[1].text = "Time's up!";

                gameCanvas[0].GetComponent<Animator>().Play("timesup");
                gameCanvas[1].GetComponent<Animator>().Play("timesup");

                countdown = false;
                StartCoroutine(AnswerPopUp(true));
            }   
        }  
    }

    public void OnTouched(int pin, bool touched)
    {
        // Double page
        //if (activeTrackables.Count == 1) // CHANGE TO 2!
        //{
            //TrackableBehaviour trackablePage1 = activeTrackables[0];
            //TrackableBehaviour trackablePage2 = activeTrackables[1];

            // Check we are on pages 1 or 2
            //currentPage = trackablePage1.TrackableName;

            //if (currentPage == "trackerpage1" || currentPage == "trackerpage2")
            //{
                // What pin was touched?
                switch (pin)
                {
                    // TODO: check which pins are connected to each object

                    case 0: // Player1, +
                        if (touched)
                        {
                            if(playing == 0)
                            {
                                // Wait for player 2
                                startText1.text = "Wait for player 2 to start the game";
                                playing = 1;
                            }
                            else if(playing == 2) //Player2 waiting
                            {
                                StartGame();
                            }
                            print("touched 0");
                            action1 = 1; // Increase player1 answer
                        }
                        else
                        {
                            print("UN touched 0");
                            action1 = 0; // Stop increase/decrease
                        }
                        break;
                    case 1: // Player1, -
                        if (touched)
                        {
                            print("touched 1");
                            action1 = 2; // Decrease player1 answer
                        }
                        else
                        {
                            print("UN touched 1");
                            action1 = 0; // Stop increase/decrease
                        }
                        break;
                    case 9: // Player2, +
                        if (touched)
                        {
                            if (playing == 0)
                            {
                                // Wait for player 2
                                startText2.text = "Wait for player 1 to start the game";
                                playing = 2;
                            }
                            else if (playing == 1) //Player 1 waiting
                            {
                                StartGame();
                            }
                            print("touched 2");
                            action2 = 1;
                        }
                        else
                        {
                            print("UN touched 2");
                            action2 = 0;
                        }
                        break;
                    case 8: // Player2, -
                        if (touched)
                        {
                            print("touched 3");
                            action2 = 2;
                        }
                        else
                        {
                            print("UN touched 3");
                            action2 = 0;
                        }
                        break;

                    case 4: // Start Game
                        if(touched && playing!=3) // Only when the game has finished or hasn't started
                        {
                            print("Starting game...");
                            StartGame();
                        }
                        break;

                    default:
                        //Instructions for computer testing
                        print("Press 4 to start the game. \n Player1 controls: keys 0 and 1. Player2 controls: keys 2 and 3");
                        break;

                        // ...
                }
            //}
        //}
    

    }

    void StartGame()
    {
        // Press a button (4) to start the game

        score_p1 = 0;
        score_p2 = 0;
        scoreText_p1.text = "Score:" + 0 + "/" + totalScore; // Score display
        scoreText_p2.text = "Score:" + 0 + "/" + totalScore;

        countdown = false;
        timeLeft = counterTime;
        timer[0].text = (int)timeLeft + "s"; // timer display
        timer[1].text = (int)timeLeft + "s";

        nquestion = 0;
        print("starting question...");
        playing = 3; // This avoids restarting the game by pressing again the button (4)

        StartCoroutine(QuestionPopUp(true));
    }

    IEnumerator QuestionPopUp(bool active)
    {
        startCanvas[0].SetActive(!active);
        startCanvas[1].SetActive(!active);

        questionN1.text = "Question " + (nquestion + 1);
        questionBig1.fontSize = 20;
        questionBig1.text = question[nquestion];
        questionN2.text = "Question " + (nquestion + 1);
        questionBig2.fontSize = 20;
        questionBig2.text = question[nquestion];

        gameSound.clip = multimediaSound;
        gameSound.Play();

        questionCanvas[0].SetActive(active);
        questionCanvas[1].SetActive(active);

        yield return new WaitForSeconds(6);

        questionCanvas[0].GetComponent<Animator>().Play("question_reduction");
        questionCanvas[1].GetComponent<Animator>().Play("question_reduction");

        yield return new WaitForSeconds(0.05f);

        gameSound.clip = ascendSound;
        gameSound.Play();

        yield return new WaitForSeconds(1);

        ans_p1 = 0;
        ans_p2 = 0;
        answerP1.text = ans_p1 + "%"; // Answer display reset
        answerP2.text = ans_p2 + "%";

        percentBars[0].GetComponent<UnityEngine.UI.Image>().fillAmount = ans_p1 / 100;
        percentBars2[0].GetComponent<UnityEngine.UI.Image>().fillAmount = ans_p2 / 100;

        // Hide start text and show question, answer, score and timer text objects.
        GameDisplay(true);

        // Start timer
        timeLeft = counterTime; // Reset timer
        countdown = true;
        yield return new WaitForSeconds(1);

        gameSound.clip = countdownSound;
        gameSound.Play();
    }

    IEnumerator AnswerPopUp(bool active)
    {
        yield return new WaitForSeconds(2);

        gameCanvas[0].GetComponent<Animator>().Play("counter");
        gameCanvas[1].GetComponent<Animator>().Play("counter");

        questionN1.text = "";
        questionBig1.text = "";
        questionN2.text = "";
        questionBig2.text = "";

        questionCanvas[0].GetComponent<Animator>().Play("pop up");
        questionCanvas[1].GetComponent<Animator>().Play("pop up");

        yield return new WaitForSeconds(0.1f); // Necessary in order to fix a bug

        //gameCanvas[0].SetActive(!active);
        //gameCanvas[1].SetActive(!active);

        answers1[0].gameObject.SetActive(false);
        answers2[0].gameObject.SetActive(false);


        questionCanvas[0].SetActive(false);
        questionCanvas[1].SetActive(false);

        gameSound.clip = popSound;
        gameSound.Play();

        answers1[1].text = "You: " + Mathf.RoundToInt(ans_p1) + "%";
        answers1[1].gameObject.SetActive(true);
        answers2[1].text = "You: " + Mathf.RoundToInt(ans_p2) + "%";
        answers2[1].gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        gameSound.clip = popSound;
        gameSound.Play();

        percentBars[1].GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
        percentBars[1].gameObject.SetActive(true);
        percentBars[1].GetComponent<UnityEngine.UI.Image>().fillAmount = ans_p2 / 100; // P2
        answers1[2].text = "Player 2: " + Mathf.RoundToInt(ans_p2) + "%";
        answers1[2].gameObject.SetActive(true);

        percentBars2[1].GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
        percentBars2[1].gameObject.SetActive(true);
        percentBars2[1].GetComponent<UnityEngine.UI.Image>().fillAmount = ans_p1 / 100; // P2
        answers2[2].text = "Player 1: " + Mathf.RoundToInt(ans_p1) + "%";
        answers2[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        gameSound.clip = popSolutionSound;
        gameSound.Play();

        answers1[3].text = "Correct answer:";
        answers1[3].gameObject.SetActive(true);
        answers2[3].text = "Correct answer:";
        answers2[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        //gameSound.clip = popSolutionSound;
        //gameSound.Play();

        percentBars[2].GetComponent<UnityEngine.UI.Image>().fillAmount = 0; 
        percentBars[2].gameObject.SetActive(true);
        percentBars[2].GetComponent<UnityEngine.UI.Image>().fillAmount = correctAnswer[nquestion] / 100;
        answers1[0].text = correctAnswer[nquestion] + "%";
        answers1[0].color = new Color(0.1f, 0.6f, 0.3f, 1);
        answers1[0].gameObject.SetActive(true);

        percentBars2[2].GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
        percentBars2[2].gameObject.SetActive(true);
        percentBars2[2].GetComponent<UnityEngine.UI.Image>().fillAmount = correctAnswer[nquestion] / 100;
        answers2[0].text = correctAnswer[nquestion] + "%";
        answers2[0].color = new Color(0.1f, 0.6f, 0.3f, 1);
        answers2[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(4);

        percentBars[1].gameObject.SetActive(false);
        percentBars[2].gameObject.SetActive(false);

        percentBars2[1].gameObject.SetActive(false);
        percentBars2[2].gameObject.SetActive(false);

        foreach (Text answer in answers1)
        {
            answer.gameObject.SetActive(false);
        }
        foreach (Text answer in answers2)
        {
            answer.gameObject.SetActive(false);
        }

        StartCoroutine(CheckAnswers());
    }


    IEnumerator CheckAnswers ()
    {
        GameDisplay(false);

        // TODO: answers should be reduced to the same decimals as when projected

        if (Mathf.Abs(ans_p1-correctAnswer[nquestion]) < Mathf.Abs(ans_p2 - correctAnswer[nquestion]))
        {
            // P1 wins 1 point
            score_p1 += 1;
            scoreText_p1.text = "Score:" + score_p1 + "/" + totalScore;

            startText1.text = "Good work! You win 1 point";
            startText2.text = "Ops! You lose this one";
        }
        else if (Mathf.Abs(ans_p1 - correctAnswer[nquestion]) == Mathf.Abs(ans_p2 - correctAnswer[nquestion]))
        {
            // Tie. Both P1 and P2 win 1 point
            score_p1 += 1;
            score_p2 += 1;

            scoreText_p1.text = "Score:" + score_p1 + "/" + totalScore;
            scoreText_p2.text = "Score:" + score_p2 + "/" + totalScore;

            startText1.text = "A tie! You both win 1 point";
            startText2.text = "A tie! You both win 1 point";
        }
        else
        {
            // P2 wins 1 point
            score_p2 += 1;
            scoreText_p2.text = "Score:" + score_p2 + "/" + totalScore;
            startText2.text = "Good work! You win 1 point";
            startText1.text = "Ops! You lose this one";
        }

        gameSound.clip = pointsSound;
        gameSound.Play();

        yield return new WaitForSeconds(4);

        StartCoroutine(CheckScores());
    }

    IEnumerator CheckScores()
    {
        if (score_p1 == totalScore && score_p2 == totalScore)
        {
            // Both P1 and P2 wins the game!
            startText1.text = "Game over! \n" +
                "Whoa! You both won the game!";
            startText2.text = "Game over! \n" +
                 "Whoa! You both won the game!";

            gameSound.clip = winSound;
            gameSound.Play();

            playing = 0;
        }
        else if (score_p1 == totalScore)
        {
            // P1 wins
            startText1.text = "Game over! \n" +
              "You won the game :)";
            gameSound.clip = winSound;
            gameSound.Play();
            yield return new WaitForSeconds(4);

            startText2.text = "Game over! \nYou lost :(";

            gameSound.clip = loseSound;
            gameSound.Play();

            playing = 0;
        }
        else if (score_p2 == totalScore)
        {
            // P2 wins
            startText2.text = "Game over! \n" +
             "Congratulations! You won the game";
            gameSound.clip = winSound;
            gameSound.Play();
            yield return new WaitForSeconds(4);

            startText1.text = "Game over! \nYou lost :(";

            gameSound.clip = loseSound;
            gameSound.Play();

            playing = 0;
        }
        else
        {
            // Next question
            nquestion += 1;
            StartCoroutine(QuestionPopUp(true));
        }
        yield return new WaitForSeconds(4);

        if(playing == 0)
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        playing = 0;
        startText1.text = "Touch + to restart the game";
        startText2.text = "Touch + to restart the game";
    }

    void GameDisplay(bool active)
    {
        // Hide start text and show question, answer, score, timer text objects.

        startCanvas[0].SetActive(!active);
        startCanvas[1].SetActive(!active);

        questionCanvas[0].SetActive(active);
        questionCanvas[1].SetActive(active);

        answers1[0].color = new Color(0.2f, 0.2f, 0.2f, 1);
        answers1[0].gameObject.SetActive(active);
        answers2[0].color = new Color(0.2f, 0.2f, 0.2f, 1);
        answers2[0].gameObject.SetActive(active);

        gameCanvas[0].SetActive(active);
        gameCanvas[1].SetActive(active);
    }

    void DefineQuestions()
    {
        // Complete up to 10 questions

        question = new string[10];

        question[0] = "What percentage of tropical forests has disappeared due to human activities since 1750?";
        question[1] = "How much has human population increased in percentage since 1970?";
        question[2] = "What amount of the world's food production goes uneaten?";
        question[3] = "How much energy can we save by making a recycled plastic bottle compared to not recycled?";
        question[4] = "In 2007, 140.3 million cellphones were retired. How much of it was recycled?";
        question[5] = "What’s the proportion of mammals at threat of extinction?";
        question[6] = "What percentage of the global energy consumption comes from renewable energies?";
    

        correctAnswer = new float[10];

        correctAnswer[0] = 28;
        correctAnswer[1] = 95;
        correctAnswer[2] = 33;
        correctAnswer[3] = 75;
        correctAnswer[4] = 10;
        correctAnswer[5] = 25;
        correctAnswer[6] = 19;

    }
}
