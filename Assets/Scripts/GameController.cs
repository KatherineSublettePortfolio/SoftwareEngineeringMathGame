using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    private DataController dataController;
    DatabaseController databaseController = new DatabaseController();
    private string[] questions;
    private int[] answers;
    private int questionIndex;
    private object totalScore;
    private bool[] hasAnimal = new bool[9];
    //text
    public Text question;
    public Text btn1;
    public Text btn2;
    public Text btn3;
    public Text score;
    public Text correctAnswer;

    //panels
    public GameObject panelCorrect;
    public GameObject panelIncorrect;
    public GameObject panelLevelComplete;

    //engines
    public GameObject engine;
    public GameObject engineOne;
    public GameObject engineTwo;
    public GameObject engineThree;
    public GameObject engineFour;
    public GameObject engineFive;
    public GameObject engineSix;
    public GameObject engineSeven;
    public GameObject engineEight;
    public GameObject engineNine;
    public GameObject caboose;

    //animals
    public GameObject toucan1;
    public GameObject toucan2;
    public GameObject toucan3;
    public GameObject tiger1;
    public GameObject tiger2;
    public GameObject tiger3;
    public GameObject sloth2;
    public GameObject sloth3;
    public GameObject bear1;
    public GameObject bear2;
    public GameObject bear3;
    public GameObject lion2;
    public GameObject lion3;
    public GameObject rhino2;
    public GameObject rhino3;
    public GameObject monkey3;
    public GameObject giraffe3;
    public GameObject elephant3;
    
    private List<int> answerOptions;
    private System.Random rng = new System.Random();
    private string[] bonusQuestions;
    private int[][] bonusOptions;
    private int[] bonusAnswers;
    private int bonusIndex;
    private int correctQuestionsCount;
    private Boolean isBonusQuestion;

    //game object background to move
    public GameObject background;
    public GameObject background1;
    public GameObject background2;
    Vector3 originalPos;


    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = (RectTransform)background.transform;

        float width = rt.rect.width;
        float height = rt.rect.height;
        // Debug.Log(width);
       // Debug.Log(height);
        background.transform.position = new Vector3(0, 384, 0);
        background1.transform.position = new Vector3(1024, 384, 0);
        background2.transform.position = new Vector3(2048, 384, 0);
        dataController = FindObjectOfType<DataController>();
        //dataController.generateData();
        dataController.generateBonus();
        questions = dataController.getQuestions();
        answers = dataController.getAnswers();
        bonusQuestions = dataController.getBonusQuestions();
        bonusOptions = dataController.getBonusOptions();
        bonusAnswers = dataController.getBonusAnswers();
        totalScore = databaseController.getTotalScore(PlayerPrefs.GetString("PlayerName"));
        score.text = "Score: " + totalScore;
        questionIndex = 0;
        bonusIndex = 0;
        correctQuestionsCount = 0;
        isBonusQuestion = false;
        checkPoints();
        bonusQuestionsCheck();
        showQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos = new Vector3(posX + t, 0, 0);
        // background.transform.position = pos;
        //Vector3 vectorB = background.transform.position;
        Vector3 vectorB = new Vector3(background.transform.position.x - 1, 384, 0);
        Vector3 vectorB1 = new Vector3(background1.transform.position.x - 1, 384, 0);
        Vector3 vectorB2 = new Vector3(background2.transform.position.x - 1, 384, 0);
        if (vectorB.x < -1024) {
            vectorB = new Vector3(2047, 384, 0);
            background.transform.position = vectorB;
        }
        if (vectorB1.x < -1024)
        {
            vectorB1 = new Vector3(2047, 384, 0);
            background1.transform.position = vectorB1;
        }
        if (vectorB2.x < -1024)
        {
            vectorB2 = new Vector3(2047, 384, 0);
            background2.transform.position = vectorB2;
        }
        //background.transform.Translate(-Vector3.right*200* Time.deltaTime);
        //background1.transform.Translate(-Vector3.right * 200 * Time.deltaTime);
        //background2.transform.Translate(-Vector3.right * 200 * Time.deltaTime);
        background.transform.position = vectorB;
        background1.transform.position = vectorB1;
        background2.transform.position = vectorB2;
    }

    private void showQuestions() {
        //make sure popups are inactive
        answerOptions = new List<int>();
        question.text = questions[questionIndex];
        answerOptions = getAnswerOptionList(answers[questionIndex]);
        Shuffle(answerOptions);
        btn1.text = "" + answerOptions[0];
        btn2.text = "" + answerOptions[1];
        btn3.text = "" + answerOptions[2];
    }

    public void Shuffle(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void buttonClicked(Text btnText) {
        //get answer from bonusAnswers if bonus question
        //otherwise not
        string answerString;
        if (this.isBonusQuestion)
        {
            answerString = bonusAnswers[bonusIndex].ToString();
        }
        else
        {
            answerString = answers[questionIndex].ToString();

        }
        if (btnText.text.Equals(answerString))
        {
            //toggle correct popup
            //add score
            //increase check question count
            correctQuestionsCount++;
            if (!this.isBonusQuestion)
            {
                object levelId = databaseController.getLevel(PlayerPrefs.GetString("PlayerName"));
                int level = Convert.ToInt32(levelId);
                if (level == 1)
                {
                    totalScore = databaseController.updateScore(10, PlayerPrefs.GetString("PlayerName"));

                }
                else if (level == 2)
                {
                    totalScore = databaseController.updateScore(100, PlayerPrefs.GetString("PlayerName"));
                }
                else {
                    totalScore = databaseController.updateScore(1000, PlayerPrefs.GetString("PlayerName"));
                }
            }
            else {
                databaseController.addAnimal(PlayerPrefs.GetString("PlayerName"));
                isBonusQuestion = false;
            }

            score.text = "Score " + totalScore;
            this.checkPoints();
            this.bonusQuestionsCheck();
  
            
                panelCorrect.gameObject.SetActive(true);
                //check question count
                //if it's true do bonus question
                if (this.checkCorrectQuestionCount())
                {
                    //toggle some kind of bonus questions message
                    isBonusQuestion = true;
                    correctQuestionsCount = 0;
                    showBonusQuestions();
                }
                else
                {
                    //if not procede as normal
                    questionIndex++;
                    showQuestions();
                }
        }
        else { 
            //toggle incorrect popup
            panelIncorrect.gameObject.SetActive(true);
            if (this.isBonusQuestion)
            {
                correctAnswer.text = "That is Incorrect!\nThe Correct answer is " + bonusAnswers[bonusIndex];
            }
            else
            {
                correctAnswer.text = "That is Incorrect!\nThe Correct answer is " + answers[questionIndex];
            }
                questionIndex++;
                showQuestions();
        }
    }

    public void CorrectClick() {
        panelCorrect.gameObject.SetActive(false);
        this.checkFinished();
    }

    public void IncorrectClick()
    {
        panelIncorrect.gameObject.SetActive(false);
        this.checkFinished();
    }

    public List<int> getAnswerOptionList(int answer) {
        List<int> temp = new List<int>();
        List <int> listNumber = new List<int>();
        //generate difference (random number between 0 & 3
        int difference = rng.Next(0, 3);
        //generate two random numbers between answer - difference and answer + difference
        
        
        
        int one = Math.Abs(rng.Next(answer-difference, answer+difference));
        int two = Math.Abs(rng.Next(answer - difference, answer + difference));
        Boolean isGood = false;
        while (!isGood)
        {
            if (one == answer)
            {
                one = (one + rng.Next(1, 3));
            }
            else if (two == answer)
            {
                two = (two + rng.Next(1, 3));
            }
            else if (one == two)
            {
                one = (one + rng.Next(1, 3));
            }
            else {
                isGood = true;
            }
        }
        temp.Add(answer);
        temp.Add(Math.Abs(one));
        temp.Add(Math.Abs(two));
        return temp;
    }
    public void checkPoints()
    {
        //set all engines to inactive
        engine.gameObject.SetActive(false);
        engineOne.gameObject.SetActive(false);
        engineTwo.gameObject.SetActive(false);
        engineThree.gameObject.SetActive(false);
        engineFour.gameObject.SetActive(false);
        engineFive.gameObject.SetActive(false);
        engineSix.gameObject.SetActive(false);
        engineSeven.gameObject.SetActive(false);
        engineEight.gameObject.SetActive(false);
        engineNine.gameObject.SetActive(false);
        caboose.gameObject.SetActive(false);
        int points = Convert.ToInt32(totalScore);
        if (points < 30)
        {
            engine.gameObject.SetActive(true);
        }
        else if (points >= 30 && points < 70)
        {
            engineOne.gameObject.SetActive(true);
        }
        else if (points >= 70 && points < 100)
        {
            engineTwo.gameObject.SetActive(true);
        }
        else if (points >= 100 && points < 300)
        {
            engineThree.gameObject.SetActive(true);
        }
        else if (points >= 300 && points < 700)
        {
            engineFour.gameObject.SetActive(true);
        }
        else if (points >= 700 && points < 1000)
        {
            engineSix.gameObject.SetActive(true);
        }
        else if (points >= 1000 && points < 3000)
        {
            engineSeven.gameObject.SetActive(true);
        }
        else if (points >= 3000 && points < 7000)
        {
            engineEight.gameObject.SetActive(true);
        }
        else if (points >= 7000 && points < 11100)
        {
            engineNine.gameObject.SetActive(true);
        }
        else if(points >= 11000)
        {
            caboose.gameObject.SetActive(true);
        }
    }

    public void levelComplete ()
    {
        panelLevelComplete.gameObject.SetActive(false);

        if (questionIndex == 7)
        {
            panelLevelComplete.gameObject.SetActive(true);
        }
    }

    public Boolean checkCorrectQuestionCount() {
        if (correctQuestionsCount == 4)
        {
            return true;
        }
        else {
            return false;
        }
    }

    private void showBonusQuestions()
    {
        System.Random rnd = new System.Random();
        bonusIndex = rnd.Next(0, 9);
        //make sure popups are inactive
        question.text = bonusQuestions[bonusIndex];
        btn1.text = "" + bonusOptions[bonusIndex][0];
        btn2.text = "" + bonusOptions[bonusIndex][1]; 
        btn3.text = "" + bonusOptions[bonusIndex][2];
    }

    public void bonusQuestionsCheck() {
        toucan1.SetActive(false);
        toucan2.SetActive(false);
        toucan3.SetActive(false);
        tiger1.SetActive(false);
        tiger2.SetActive(false);
        tiger3.SetActive(false);
        bear1.SetActive(false);
        bear2.SetActive(false);
        bear3.SetActive(false);
        sloth2.SetActive(false);
        sloth3.SetActive(false);
        lion2.SetActive(false);
        lion3.SetActive(false);
        rhino2.SetActive(false);
        rhino3.SetActive(false);
        monkey3.SetActive(false);
        giraffe3.SetActive(false);
        elephant3.SetActive(false);
        //see if we have animals
        //toucan
        int aninmalCount = Convert.ToInt32(databaseController.getAnimalCount(PlayerPrefs.GetString("PlayerName")));
        if (aninmalCount > 0) {
            //check what engine we have visible then enable the right toucan
            //if engine one - three toucan1
            if (engineOne.activeSelf || engineTwo.activeSelf || engineThree.activeSelf)
            {
                toucan1.SetActive(true);
            }
            //if engine four - six toucan2
            else if (engineFour.activeSelf || engineFive.activeSelf || engineSix.activeSelf)
            {
                toucan2.SetActive(true);
            }
            //else toucan3
            else {
                toucan3.SetActive(true);
            }
        }
        //tiger
        if(aninmalCount > 1)
        {
            //if engine one - three tiger1
            if (engineOne.activeSelf || engineTwo.activeSelf || engineThree.activeSelf)
            {
                tiger1.SetActive(true);
            }
            //if engine four - six tiger2
            else if (engineFour.activeSelf || engineFive.activeSelf || engineSix.activeSelf)
            {
                tiger2.SetActive(true);
            }
            //else tiger3
            else
            {
                tiger3.SetActive(true);
            }
        }
        //bear
        if (aninmalCount > 2) {
            //if engine one - three bear1
            if (engineOne.activeSelf || engineTwo.activeSelf || engineThree.activeSelf)
            {
                bear1.SetActive(true);
            }
            //if engine four - six bear2
            else if (engineFour.activeSelf || engineFive.activeSelf || engineSix.activeSelf)
            {
                bear2.SetActive(true);
            }
            //else bear3
            else
            {
                bear3.SetActive(true);
            }
        }
        //sloth
        if (aninmalCount > 3)
        {
            //if engine four - engine six sloth 2
            if (engineFour.activeSelf || engineFive.activeSelf || engineSix.activeSelf)
            {
                sloth2.SetActive(true);
            }
            //else sloth3
            else
            {
                sloth3.SetActive(true);
            }
        }
        //lion
        if (aninmalCount > 4)
        {
            //if engine four - engine six  lion2
            if (engineFour.activeSelf || engineFive.activeSelf || engineSix.activeSelf)
            {
                lion2.SetActive(true);
            }
            //else lion3
            else
            {
                lion3.SetActive(true);
            }
        }
        //rhino
        if (aninmalCount > 5)
        {
            //if engine four - engine six rhino2
            if (engineFour.activeSelf || engineFive.activeSelf || engineSix.activeSelf)
            {
                rhino2.SetActive(true);
            }
            //else rhino3
            else
            {
                rhino3.SetActive(true);
            }
        }
        //monkey
        if (aninmalCount > 6)
        {
            monkey3.SetActive(true);
        }
        //giraffe
        if (aninmalCount > 7)
        {
            giraffe3.SetActive(true);
        }
        //elephant
        if (aninmalCount > 8)
        {
            elephant3.SetActive(true);
        }
    }

    public void checkFinished()
    {
        if (questionIndex == questions.Length)
        {
            object levelId = databaseController.getLevel(PlayerPrefs.GetString("PlayerName"));
            int level = Convert.ToInt32(levelId);
            if (level == 3)
            {
                SceneManager.LoadScene("GameFinished");

            }
            else
            {
                SceneManager.LoadScene("QuizFinished");
            }
        }
    }

}
