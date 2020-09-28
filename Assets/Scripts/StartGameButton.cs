using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public new Text name;
    public DataController dataController;
    private RoundData currentRoundData;
    public void Start()
    {
        name.text = PlayerPrefs.GetString("PlayerName");
    }
    public void NextScene()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void scoreboardButton() {
        SceneManager.LoadScene("StudentScoreboard");
    }

    public void backButton()
    {
        SceneManager.LoadScene("HomeScreen");
    }

    public void Update()
    {
        //PlayerPrefs.SetString("PlayerName", "no name selected");
        //Debug.Log(PlayerPrefs.GetString("PlayerName"));
        name.text = PlayerPrefs.GetString("PlayerName");
    }
 
}
