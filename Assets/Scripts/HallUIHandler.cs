using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class HallUIHandler : MonoBehaviour
{
    public Button backToMain;
    public Text nameText;
    public Text highScoreText;
    private string nameHallOfFame;
    private int highScoreHallOfFame;
    private void Start()
    {
        LoadHighScore();
        Debug.Log(nameText.text + " " + highScoreText.text);
        nameText.text = nameHallOfFame;
        highScoreText.text =highScoreHallOfFame.ToString();
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("StartUp");
    }
    [System.Serializable]
    class SaveData
    {
        public string name_HighScore;
        public int high_Score;
    }
    
    public void LoadHighScore()
    {
        
        string path = Application.persistentDataPath + "/high_score.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            nameHallOfFame = data.name_HighScore;
            highScoreHallOfFame = data.high_Score;
            
        }
    }
}
