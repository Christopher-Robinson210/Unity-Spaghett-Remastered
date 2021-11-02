using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public TMPro.TextMeshProUGUI wavesCompleted;
    public TMPro.TextMeshProUGUI kills;
    public Button button;

    private void Awake()
    {
        wavesCompleted.text = $"Waves Completed: {Spaghett.GameManager.waveCount}";
        kills.text = $"Kills: {Spaghett.GameManager.killCount}";
        button.onClick.AddListener(Exit);
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Exit();
    //    }
    //}

    void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        print("QUIT");
        Application.Quit();
    }
}
