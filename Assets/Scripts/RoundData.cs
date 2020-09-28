using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RoundData
{
    public string name;
    public string[] questions;
    public int[] randomOne = new int[10];
    public int[] randomTwo = new int[10];
    public int[] answers = new int[10];
    public void generateData()
    {
        questions = new string[10];
        //generate ten questions worth of first num and two num
        System.Random rnd = new System.Random();
        for (int i = 0; i < randomOne.Length; i++)
        {
            randomOne[i] = rnd.Next(1, 10);
            randomTwo[i] = rnd.Next(1, 10);
        }

        //iterate over two nums, decide if + or -, save questions, save answers
        System.Random random = new System.Random();
        for (int i = 0; i < randomOne.Length; i++)
        {

            if (random.Next(1, 10) % 2 == 0)
            {

                answers[i] = randomOne[i] + randomTwo[i];
                questions[i] = randomOne[i] + " + " + randomTwo[i];
            }
            else
            {
                if ((randomOne[i] - randomTwo[i]) > 0)
                {

                    answers[i] = randomOne[i] - randomTwo[i];
                    questions[i] = randomOne[i] + " - " + randomTwo[i];
                }
                else
                {

                    answers[i] = randomTwo[i] - randomOne[i];
                    questions[i] = randomTwo[i] + " - " + randomOne[i];
                }
            }
        }
    }

    public string[] getQuestions() {
        generateData();
        return this.questions;
    }
    public int[] getAnswers() {
        return this.answers;
    }
}