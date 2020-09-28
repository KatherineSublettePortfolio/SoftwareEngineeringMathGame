using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AddStudentSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    string fname;
    string lname;
    int totalScore;
    int levelId;
    public InputField fNameInput;
    public InputField lNameInput;
    public InputField totalScoreInput;
    public Dropdown levelIdDropdown;
    public GameObject panelAdded;
    DatabaseController databaseController = new DatabaseController();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFName() {
        fname = fNameInput.text;
    }

    public void setLName() {
        lname = lNameInput.text;
    }

    public void setTotalScore() {
        totalScore = Int32.Parse(totalScoreInput.text);
    }

    public void setLevelId() {
        Debug.Log(levelIdDropdown.value);
        levelId = levelIdDropdown.value;
    }

    public void NextScene()
    {
        panelAdded.SetActive(false);
        SceneManager.LoadScene("TeacherScreen");
    }

    public void addStudent() {
        panelAdded.SetActive(true);
        databaseController.addStudent(fname, lname, totalScore, levelId, PlayerPrefs.GetString("TeacherName"));
    }

    public void backButton() {
        SceneManager.LoadScene("TeacherScreen");
    }

}
