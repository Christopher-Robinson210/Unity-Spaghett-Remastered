using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spaghett
{

    public class UIManager : MonoBehaviour
    {
        [Header("UI")]
        public TMPro.TextMeshProUGUI waveCounter;
        public TMPro.TextMeshProUGUI killCounter;
        public TMPro.TextMeshProUGUI health;
        public GameObject panelMessage;
        public TMPro.TextMeshProUGUI textMessage;
        //public Spaghett.MenuButtons menuButtons;
        public static bool isRestart = false;

        // Start is called before the first frame update
        void Awake()
        {
            panelMessage.SetActive(false);
            waveCounter.text = $"Wave: {Spaghett.EnemyManager.waveCount}";
            killCounter.text = $"Kills: {Spaghett.GameManager.killCount}";
            health.text = $"Health: {Spaghett.Player.health}";
        }

        // Update is called once per frame
        void Update()
        {
            waveCounter.text = $"Wave: {Spaghett.EnemyManager.waveCount}";
            killCounter.text = $"Kills: {Spaghett.GameManager.killCount}";
            health.text = $"Health: {Spaghett.Player.health}";
        }

        public void ShowMessage(string message)
        {
            textMessage.text = message;
            StartCoroutine(DelayBeforeClear());
        }

        IEnumerator DelayBeforeClear()
        {
            //show panel
            panelMessage.SetActive(true);
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(2f);
            Time.timeScale = 1;
            //hide panel
            panelMessage.SetActive(false);
        }
    }
}
