using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DataController : MonoBehaviour
{
    public static DataController Instance;
    //public RoundData[] allRoundData;
    public string playerName;
    // Use this for initialization
    public string[] questions;
    public int[] randomOne = new int[10];
    public int[] randomTwo = new int[10];
    public int[] answers = new int[10];
    DatabaseController databaseController = new DatabaseController();
    public string[] bonusQuestions;
    public int[][] bonusOptions;
    public int[] bonusAnswers;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        //this.generateData();
        PlayerPrefs.SetString("PlayerName", "no name selected");
        PlayerPrefs.SetString("TeacherName", "no name selected");
        SceneManager.LoadScene("HomeScreen");
    }

    //public RoundData GetCurrentRoundData()
    //{
        //return allRoundData[0];
    //}

    // Update is called once per frame
    // WHAT DOES THIS MEAN???? UPDATE SCORE? UPDATE QUESTIONS? WAT
    void Update()
    {

    }

    public void generateData()
    {
        //check level id
        //produce random numbers according to level id

        questions = new string[10];
        object levelId = databaseController.getLevel(PlayerPrefs.GetString("PlayerName"));
        int level = Convert.ToInt32(levelId);
            //= Convert.ToInt32(databaseController.getLevel(PlayerPrefs.GetString("PlayerName")));
        System.Random rnd = new System.Random();
        //generate ten questions worth of first num and two num
        if (level == 1) { 
            for (int i = 0; i < randomOne.Length; i++)
            {
                randomOne[i] = rnd.Next(1, 10);
                randomTwo[i] = rnd.Next(1, 10);
            }
         }

        if(level == 2) {
            for (int i = 0; i < randomOne.Length; i++)
            {
                randomOne[i] = rnd.Next(10, 99);
                randomTwo[i] = rnd.Next(10, 99);
            }
        }
        
        if (level == 3)
        {
            for (int i = 0; i < randomOne.Length; i++)
            {
                randomOne[i] = rnd.Next(100, 999);
                randomTwo[i] = rnd.Next(100, 999);
            }
        }

        //iterate over two nums, decide if + or -, save questions, save answers
        System.Random random = new System.Random();
        for (int i = 0; i < questions.Length; i++)
        {
            string sign;


            /*
            CONVERTING THE STUPID NUMBERS TO STRINGS UGH
            */

            if (random.Next(1, 10) % 2 == 0)
            {
                sign = "+";
                answers[i] = randomOne[i] + randomTwo[i];
                // r1 = Convert.ToString(randomOne[i]);
                //r1 = (randomOne[i]).ToString();
                // r2 = Convert.ToString(randomTwo[i]);
                //r2 = (randomTwo[i]).ToString();
                questions[i] = randomOne[i] + sign + randomTwo[i];
            }
            else
            {
                if ((randomOne[i] - randomTwo[i]) > 0)
                {
                    sign = "-";
                    answers[i] = randomOne[i] - randomTwo[i];
                    // r1 = Convert.ToString(randomOne[i]);
                    //r1 = (randomOne[i]).ToString();
                    // r2 = Convert.ToString(randomTwo[i]);
                    //r2 = (randomTwo[i]).ToString();
                    questions[i] = randomOne[i] + sign + randomTwo[i];
                }
                else
                {
                    sign = "-";
                    answers[i] = randomTwo[i] - randomOne[i];
                    // r1 = Convert.ToString(randomOne[i]);
                    //r1 = (randomOne[i]).ToString()
                    // r2 = Convert.ToString(randomTwo[i]);
                    //r2 = (randomTwo[i]).ToString()
                    questions[i] = randomTwo[i] + sign + randomOne[i];
                }
            }
        }
    }

   

    

    public string[] getQuestions()
    {
        generateData();
        return this.questions;
    }
    public int[] getAnswers()
    {
        return this.answers;
    }

    public void generateBonus() {
        bonusQuestions = new string[] { "What number is in the ones place of 452", "What number is in the tens place of 753",
            "What number is in the hundreds place of 168", "What number is in the ones place of 891", "What number is in the tens place of 631",
            "What number is in the hundreds place of 254", "What number is in the ones place of 395", "What number is in the tens place of 572",
            "What number is in the hundreds place of 947" };

        bonusOptions = new int[][]
            {
               new int[] { 4, 5, 2 },
               new int[] { 7, 5, 3 },
               new int[] { 1, 6, 8 },
               new int[] { 8, 9, 1 },
               new int[] { 6, 3, 1 },
               new int[] { 2, 5, 4 },
               new int[] { 3, 9, 5 },
               new int[] { 5, 7, 2 },
               new int[] { 9, 4, 7 }
            };
        bonusAnswers = new int[] { 2, 5, 1, 1, 3, 2, 5, 7, 9};
    }

    public string[] getBonusQuestions() {
        return this.bonusQuestions;
    }

    public int[][] getBonusOptions() {
        return this.bonusOptions;
    }

    public int[] getBonusAnswers() {
        return this.bonusAnswers;
    }
}