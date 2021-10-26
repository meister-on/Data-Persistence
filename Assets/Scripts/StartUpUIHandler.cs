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
    // Start is called before the first frame update
    void Start()
    {
        //whenever the player writes something into the input field, the name is updated
        nameInput.text = StartUpManager.Instance.main_Name;
        nameInput.onValueChanged.AddListener(delegate { AssignName(); });
        startGameButton.onClick.AddListener(StartGame);
        hallOfFameButton.onClick.AddListener(StartHallOfFame);
    }

    void AssignName()
    {
        //updates the name in the StartUp Manager
        StartUpManager.Instance.main_Name = nameInput.text;
        Debug.Log(StartUpManager.Instance.main_Name);
    }

    void StartGame()
    {
        //loads the main scene
        SceneManager.LoadScene("main");
    }
    void StartHallOfFame()
    {
        //loads the Hall of Fame
        SceneManager.LoadScene("HallOfFame");
    }
}
