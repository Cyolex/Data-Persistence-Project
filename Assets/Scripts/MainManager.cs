using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        // set HighScoreText to Best Score : playerName
        var highScoreText = GameObject.Find("HighScoreText");
        if (highScoreText != null)
        {
            var highScoreTextComponent = highScoreText.GetComponent<Text>();
            if (highScoreTextComponent != null)
            {
                highScoreTextComponent.text = $"Best Score : {Tracker.Instance.playerName} : {Tracker.Instance.highScore}";
            }
            else
            {
                Debug.LogError("HighScoreText component not found!");
            }
        }
        else
        {
            Debug.LogError("HighScoreText GameObject not found!");
        }


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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        if (Tracker.Instance.highScore < m_Points)
        {
            Tracker.Instance.highScore = m_Points;
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    // Method that takes you to the menu scene
    public void GoToMenu()
    {
        // Save the high score before going to the menu
        Tracker.Instance.SaveHighScore();
        
        // Load the menu scene
        SceneManager.LoadScene("menu");
    }
}
