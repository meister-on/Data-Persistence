using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartUpUIHandler : MonoBehaviour
{
    public InputField nameInput;
    public Button startGameButton;
    public Button hallOfFameButton;
    public Button exitButton;
    public Text fillInNameReminderText;
    // Start is called before the first frame update
    void Start()
    {
        //whenever the player writes something into the input field, the name is updated
        fillInNameReminderText.text = "";
        nameInput.text = StartUpManager.Instance.main_Name;
        nameInput.onValueChanged.AddListener(delegate { AssignName(); });
        startGameButton.onClick.AddListener(StartGame);
        hallOfFameButton.onClick.AddListener(StartHallOfFame);
        exitButton.onClick.AddListener(ExitGame);
    }

    void AssignName()
    {
        //updates the name in the StartUp Manager
        StartUpManager.Instance.main_Name = nameInput.text;
        Debug.Log(StartUpManager.Instance.main_Name);
    }

    void StartGame()
    {
        //loads the main scene when there is a name input
        if (nameInput.text != "")
        {
            SceneManager.LoadScene("main");
        }
        else
        {
            fillInNameReminderText.text = "Input a Name";
        }
        
    }
    void StartHallOfFame()
    {
        //loads the Hall of Fame
        SceneManager.LoadScene("HallOfFame");
    }
    void ExitGame()
    {
        //Application.Quit();
    }
}
