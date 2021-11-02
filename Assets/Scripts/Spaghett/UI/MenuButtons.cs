using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Spaghett
{
    public class MenuButtons : MonoBehaviour
    {
        public Button pauseButton, restartButton, quitButton;

        // Start is called before the first frame update
        void Awake()
        {
            //add listeners
            pauseButton.onClick.AddListener(Pause);
            restartButton.onClick.AddListener(Restart);
            quitButton.onClick.AddListener(Quit);

            
            
        }

        private void Start()
        {
            //If First play set "Pause" to "Start"
            if (Spaghett.UIManager.isRestart)
            {
                pauseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Pause";
                Time.timeScale = 1;
                Spaghett.GameManager.isGameRunning = true;
            }
            else
            {
                pauseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Start";
                Time.timeScale = 0;
                Spaghett.GameManager.isGameRunning = false;
            }
        }
        void Pause()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Unpause";
                Spaghett.GameManager.isGameRunning = false;
            }

            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Pause";
                Spaghett.GameManager.isGameRunning = true;
            }

        }
       
        void Restart()
        {
            Spaghett.GameManager.ResetStats();
            Spaghett.UIManager.isRestart = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void Quit()
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
