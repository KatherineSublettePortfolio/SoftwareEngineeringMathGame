using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButtonGamePlay : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}


