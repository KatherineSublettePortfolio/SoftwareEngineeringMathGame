using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class TeacherLoginSceneController : MonoBehaviour
{
    public Dropdown dropdown;
    DatabaseController databaseController = new DatabaseController();
    List<string> teachers = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        List<object> s = databaseController.getTeachers();
        for (int i = 0; i < s.Count; i += 2)
        {
            string teacher = s[i] + " " + s[i + 1];
            teachers.Add(teacher);
        }
        dropdown.AddOptions(teachers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveTeacherName()
    {
        PlayerPrefs.SetString("TeacherName", teachers[dropdown.value-1]);
    }
    public void NextScene()
    {
        SceneManager.LoadScene("TeacherScreen");
    }

    public void backButton()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
