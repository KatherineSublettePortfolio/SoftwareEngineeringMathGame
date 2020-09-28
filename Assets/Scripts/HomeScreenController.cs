using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomeScreenController : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
