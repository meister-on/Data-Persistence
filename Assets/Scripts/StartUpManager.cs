using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpManager : MonoBehaviour
{
    public static StartUpManager Instance;
    public string main_Name;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
