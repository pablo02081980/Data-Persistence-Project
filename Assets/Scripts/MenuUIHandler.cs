using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public Text NameText;
    public int GameScore;
    public Text bestPlayerScore;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadNameAndScore();
        bestPlayerScore.text = GameManager.Instance.nameBestScorePlayer + " / " + GameManager.Instance.BestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        //GameManager.Instance.BestScore = GameScore;
        GameManager.Instance.namePlayer = NameText.text;
        Debug.Log("SCENE0_GameManager.Instance.namePlayer: " + GameManager.Instance.namePlayer);
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }
}
