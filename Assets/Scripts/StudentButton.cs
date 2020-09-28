using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StudentButton : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("StudentLoginScene");
    }
}
