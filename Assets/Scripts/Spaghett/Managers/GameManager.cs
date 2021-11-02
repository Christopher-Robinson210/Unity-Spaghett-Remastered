using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Spaghett
{

    public class GameManager : MonoBehaviour
    {

        public Spaghett.UIManager uiManager;
        private static AudioSource audioSource;
        [Header("Audio Clips")]
        public AudioClip audioClip;
        public AudioClip deathClip;

        public static int killCount = 0;
        public static int waveCount = 0;
        public static bool isGameRunning = false;

        // Start is called before the first frame update
        void Awake()
        {
            Player.health = 100;
            EnemyManager.waveCount = 0;
            if (gameObject.GetComponent<AudioSource>() != null)
            {
                audioSource = GetComponent<AudioSource>();

                audioSource.clip = audioClip;
                audioSource.Play();
            }
            
        }

        private void Update()
        {
            waveCount = EnemyManager.waveCount;

        }

        public static void ResetStats()
        {
            EnemyManager.waveCount = 0;
            killCount = 0;
            Player.health = 100;
        }

        public void GameOver()
        {
            uiManager.ShowMessage("You Died!");
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.PlayOneShot(deathClip, 1f);
            StartCoroutine(LoadGameOverScene());
        }

        IEnumerator LoadGameOverScene()
        {
            yield return new WaitForSecondsRealtime(1.1f);
            SceneManager.LoadScene("GameOver");
        }

    }
}
