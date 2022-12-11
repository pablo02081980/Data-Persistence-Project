using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Unity.IO;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;

    public Text NameText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_BestScore;
    public string nameBS;
    
    private bool m_GameOver = false;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadNameAndScore();
            m_BestScore = GameManager.Instance.BestScore;
            nameBS = GameManager.Instance.nameBestScorePlayer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManager.Instance.BestScore: " + GameManager.Instance.BestScore);
        Debug.Log("GameManager.Instance.nameBestScorePlayer: " + GameManager.Instance.nameBestScorePlayer);
        Debug.Log("SCENE1_GameManager.Instance.namePlayer: " + GameManager.Instance.namePlayer);


        NameText.text = GameManager.Instance.namePlayer;
        BestScoreText.text = "Best Score: " + nameBS + " : " + m_BestScore;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.SaveNameAndScore(NameText.text, m_BestScore, nameBS);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        UpdateBestScore();
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void UpdateBestScore()
    {
        if (m_BestScore <= m_Points && m_Points!= 0)
        {
            m_BestScore = m_Points;
            nameBS = NameText.text;
            BestScoreText.text = "Best Score: " + nameBS + " : "+ m_BestScore;
            GameManager.Instance.SaveNameAndScore(NameText.text, m_BestScore, nameBS);
        }
    }
}
