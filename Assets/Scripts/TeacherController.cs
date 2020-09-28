using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeacherController : MonoBehaviour
{
    DatabaseController databaseController = new DatabaseController();
    List<string> students = new List<string>();
    // Start is called before the first frame update
    public Dropdown dropdown;
    void Start()
    {
        //Dropdown dropdown = GetComponent<Dropdown>();
        List<object> s = databaseController.getClassroomStudents(PlayerPrefs.GetString("TeacherName"));
        for (int i = 0; i < s.Count; i+= 2)
        {
            string student = s[i] + " " + s[i + 1];
            students.Add(student);
        }
        dropdown.AddOptions(students);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropdownStudent() {
        PlayerPrefs.SetString("CurrentStudent", students[dropdown.value - 1]);
    }

    public void NextScene()
    {
        SceneManager.LoadScene("AddStudentScene");
    }

    public void seeProgress() {
        SceneManager.LoadScene("TeacherProgressPage");
    }

    public void backButton() {
        SceneManager.LoadScene("HomeScreen");
    }
}
