using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class TeacherProgressPageController : MonoBehaviour
{
    public Text name;
    public Text totalScore;
    public Text level;
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

    DatabaseController databaseController = new DatabaseController();
    string studentName;
    // Start is called before the first frame update
    void Start()
    {
        studentName = PlayerPrefs.GetString("CurrentStudent");
        name.text = studentName;
        totalScore.text = "" + databaseController.getTotalScore(studentName);
        level.text = "" + databaseController.getLevel(studentName);
        this.checkPoints();
        this.bonusQuestionsCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backButton() {
        SceneManager.LoadScene("TeacherScreen");
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
        int points = Convert.ToInt32(databaseController.getTotalScore(studentName));
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
        else if (points >= 11000)
        {
            caboose.gameObject.SetActive(true);
        }
    }

    public void bonusQuestionsCheck()
    {
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
        int aninmalCount = Convert.ToInt32(databaseController.getAnimalCount(studentName));
        if (aninmalCount > 0)
        {
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
            else
            {
                toucan3.SetActive(true);
            }
        }
        //tiger
        if (aninmalCount > 1)
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
        if (aninmalCount > 2)
        {
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
}
