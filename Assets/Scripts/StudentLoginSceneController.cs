using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class StudentLoginSceneController : MonoBehaviour
{


    public Dropdown dropdown;
    DatabaseController databaseController = new DatabaseController();
    List<string> students = new List<string>();

    public void Start()
    {
        List<object> s = databaseController.getStudents();
        for (int i = 0; i < s.Count; i += 2)
        {
            string student = s[i] + " " + s[i + 1];
            students.Add(student);
        }
        dropdown.AddOptions(students);
    }

    public void SaveName( )
    {
        PlayerPrefs.SetString("PlayerName", students[dropdown.value-1]);
    }
    public void NextScene()
    {
        SceneManager.LoadScene("StudentScene");
    }

    public void backButton()
    {
        SceneManager.LoadScene("HomeScreen");
    }

}
