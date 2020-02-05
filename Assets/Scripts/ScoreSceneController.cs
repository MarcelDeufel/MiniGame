using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreSceneController : MonoBehaviour
{
    private TextMeshProUGUI yourScoreText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI levelText;
    private Button nextLevelButton;
    private Button quitButton;
    private int multiplicator;
    private int level;
    private int scoreForStars;
    private int earnedStars;

    public Sprite noStars;
    public Sprite oneStars;
    public Sprite twoStars;
    public Sprite threeStars;
    public Sprite fourStars;
    public Sprite fiveStars;

    private SpriteRenderer starsPic;

    // Start is called before the first frame update
    void Start()
    {
        yourScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        multiplicator = PlayerPrefs.GetInt("BonusMultiplicator");
        level = PlayerPrefs.GetInt("BonusMultiplicator") + 1;

        starsPic = GameObject.Find("StarPic").GetComponent<SpriteRenderer>();
        starsPic.sprite = null;

        //set and manipulate scoreForStars Prefab
        scoreForStars = PlayerPrefs.GetInt("scoreForStars");
        scoreForStars += PlayerPrefs.GetInt("PlayerScore");
        PlayerPrefs.SetInt("scoreForStars", scoreForStars);
        Debug.Log(PlayerPrefs.GetInt("scoreForStars"));

        scoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
        levelText.text = "Level " + level;

        if (PlayerPrefs.GetInt("PlayerScore") >= 5 && level <= 4)
        {
            nextLevelButton.interactable = true;
            multiplicator++;
        }
        else
        {
            nextLevelButton.interactable = false;
            levelText.text = "Game Over";
            yourScoreText.text = "YOUR FINAL SCORE";
            scoreText.text = PlayerPrefs.GetInt("scoreForStars").ToString();
            if (PlayerPrefs.GetInt("scoreForStars") < 10)
            {
                earnedStars = 0;
                Debug.Log("0Stars");
            }

            else if (PlayerPrefs.GetInt("scoreForStars") <= 15)
            {
                earnedStars = 1;
                Debug.Log("1Stars");
            }
            else if (PlayerPrefs.GetInt("scoreForStars") <= 20)
            {
                earnedStars = 2;
                Debug.Log("2Stars");
            }
            else if (PlayerPrefs.GetInt("scoreForStars") <= 25)
            {
                earnedStars = 3;
                Debug.Log("3Stars");
            }
            else if (PlayerPrefs.GetInt("scoreForStars") <= 35)
            {
                earnedStars = 4;
                Debug.Log("4Stars");
            }
            else
            {
                earnedStars = 5;
                Debug.Log("5Stars");
            }

            switch (earnedStars)
            {
                case 0:
                    Debug.Log("Reached 0 Stars");
                    starsPic.sprite = noStars;
                    break;
                case 1:
                    Debug.Log("Reached 1 Stars");
                    starsPic.sprite = oneStars;
                    break;
                case 2:
                    Debug.Log("Reached 2 Stars");
                    starsPic.sprite = twoStars;
                    break;
                case 3:
                    Debug.Log("Reached 3 Stars");
                    starsPic.sprite = threeStars;
                    break;
                case 4:
                    Debug.Log("SReached 4 Stars");
                    starsPic.sprite = fourStars;
                    break;
                case 5:
                    Debug.Log("Reached 5 Stars");
                    starsPic.sprite = fiveStars;
                    break;
            }
        }
    }
    public void onNextLevelButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetInt("BonusMultiplicator", multiplicator);
    }
    public void onQuitButtonClicked()
    {
        SceneManager.LoadScene("StartMenu");
        PlayerPrefs.SetInt("BonusMultiplicator", 0);
        PlayerPrefs.SetInt("scoreForStars", 0);
        Debug.Log(PlayerPrefs.GetInt("BonusMultiplicator"));

    }

}
