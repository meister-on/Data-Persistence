using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public Button backToStart;
    public GameObject GameOverText;
    public GameObject startGameText;
    private int highScore;
    private string nameHighScore;


    private bool m_Started = false;
    private int m_Points;
    private int m_HighScore;
    private string n_name;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        TextInitialise();
        BricksInitialise();

    
    }
   

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                startGameText.SetActive(false);

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
    void BricksInitialise()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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
    void TextInitialise()
    {
        m_HighScore = 0;
        n_name = StartUpManager.Instance.main_Name;

        LoadHighScore();
        //if there is a high score, choose the hig scor from there
        string path = Application.persistentDataPath + "/high_score.json";
        if (File.Exists(path))
        {
            BestScoreText.text = "Best Score : " + nameHighScore + " : " + highScore;
        }
        //if there is no high score yet take the high score from the current game
        else
        {

            BestScoreText.text = "Best Score : " + n_name + " : " + m_HighScore;
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        BestScore();
        if (m_Points == 96 || m_Points == 192 || m_Points == 288) { BricksInitialise(); }
    }
    void BestScore()
    {
        string path = Application.persistentDataPath + "/high_score.json";
        if (File.Exists(path))
        {
            if (highScore < m_Points)
            {
                highScore = m_Points;
                nameHighScore = n_name;
                BestScoreText.text = "Best Score : " + nameHighScore + " : " + highScore;
            }
            else 
            { 
                BestScoreText.text = "Best Score : " + nameHighScore + " : " + highScore; 
            }
        }

        else 
        {
           if (m_HighScore < m_Points)
            {
                m_HighScore = m_Points;
                highScore = m_HighScore;
                nameHighScore = n_name;
                BestScoreText.text = "Best Score : " + nameHighScore + " : " + highScore;
            }
            
        }


    }
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SaveHighScore();
    }
    public void BackToStart()
    {
        SaveHighScore();
        SceneManager.LoadScene("StartUp");
    }
    [System.Serializable]
    class SaveData
    {
        public string name_HighScore;
        public int high_Score;
    }

    public void SaveHighScore()
    {
        SaveData highScoreData = new SaveData();
        highScoreData.name_HighScore = nameHighScore;
        highScoreData.high_Score = highScore;
        string json = JsonUtility.ToJson(highScoreData);
        File.WriteAllText(Application.persistentDataPath + "/high_score.json", json);
    }
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/high_score.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            nameHighScore = data.name_HighScore;
            highScore = data.high_Score;
        }
    }
   
}
