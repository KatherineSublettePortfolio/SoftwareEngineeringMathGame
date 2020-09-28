using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TeacherButton : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("TeacherLoginScene");
    }
}
