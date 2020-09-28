using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizFinishedController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text levelText;
    public Text totalScoreText;
    DatabaseController databaseController = new DatabaseController();
    void Start()
    {
        object level = databaseController.getLevel(PlayerPrefs.GetString("PlayerName"));
        levelText.text = "Congratulations! you finshed level " + level + "!";
        databaseController.updateLevel(PlayerPrefs.GetString("PlayerName"));
        object score = databaseController.getTotalScore(PlayerPrefs.GetString("PlayerName"));
        totalScoreText.text = "Your total score is " + score + "!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HomeButtonClick() {

        SceneManager.LoadScene("HomeScreen");
    }
    public void PlayAgainButtonClick()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
