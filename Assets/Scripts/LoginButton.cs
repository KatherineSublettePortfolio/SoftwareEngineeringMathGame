using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginButton : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("StudentScene");
    }
}
