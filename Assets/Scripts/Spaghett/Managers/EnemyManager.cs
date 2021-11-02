using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spaghett
{
    public class EnemyManager : MonoBehaviour
    {

        public Spaghett.UIManager uiManager;
        private AudioSource audioSource;
        public AudioClip bossClip;
        public List<GameObject> enemies = new List<GameObject>();
        [Tooltip("How many seconds before spawning waves of enemies")]
        public float spawnTimer = 15f;
        public float spawnRadius = 3f;
        public float spawnX = 8f;
        public float spawnY = 4f;
        public int maxEnemies = 10;
        public int waveSizeIncrement = 2;
        public float enemyMoveSpeed = 0.5f;
        public static int waveCount = 0;
        private int bossWave = 1;
        private bool timerOn = false;

        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        private void Start()
        {
            
        }
        // Update is called once per frame
        void Update()
        {
            if (Spaghett.GameManager.isGameRunning) 
            { 
            //StartSpawnTimer();
            CheckForEnemies();
            }
            
        }

        //check if enemies need to be spawned in
        void CheckForEnemies()
        {

            if (transform.childCount == 0)
            {
                //spawn enemies
                SpawnWave();
            }
        }

        public void SpawnWave()
        {
            waveCount++;
            //DEBUG INFO
            print("SpawnEnemies Called");
            if (waveCount % 5 == 0)
            {
                SpawnEnemies(maxEnemies / 5);
                SpawnBoss();
            }
            else
            {
                SpawnEnemies(maxEnemies);
            }
            maxEnemies += waveSizeIncrement; 
        }

        public void SpawnEnemies(int enemyCount)
        {
            for (int i = 0; i <= enemyCount; i++)
            {
                bool enemySpawned = false;
                GameObject randomEnemy = enemies[RandomEnemy()];
                while (!enemySpawned)
                {
                    Vector3 enemyPosition = new Vector3(Random.Range(-spawnX, spawnX), Random.Range(-spawnY, spawnY), 0f);
                    if ((enemyPosition - transform.position).magnitude < spawnRadius)
                    {
                        continue;
                    }
                    else
                    {
                        Instantiate(randomEnemy, enemyPosition, Quaternion.identity, transform);
                        if (randomEnemy.name.Equals("Troll"))
                        {
                            randomEnemy.GetComponent<Troll>().SetMoveSpeed(enemyMoveSpeed);
                        }
                        else
                        {
                            randomEnemy.GetComponent<Enemy>().SetMoveSpeed(enemyMoveSpeed);
                        }

                        print($"Enemy Spawned [{i + 1}]");
                        enemySpawned = true;
                    }
                }
            }
        }

        public void SpawnBoss()
        {
            uiManager.ShowMessage($"Boss Wave {bossWave}!");
            int bossCount = waveCount / 5;
            print($"BOSS WAVE: BOSS COUNT {bossCount}");
            for (int i = 0; i < bossCount; i++)
            {
                bool bossSpawned = false;
                
                GameObject boss = enemies[enemies.Count-1];

                while (!bossSpawned)
                {
                    Vector3 enemyPosition = new Vector3(Random.Range(-spawnX, spawnX), Random.Range(-spawnY, spawnY), 0f);
                    if ((enemyPosition - transform.position).magnitude < spawnRadius)
                    {
                        continue;
                    }
                    else
                    {
                        Instantiate(boss, enemyPosition, Quaternion.identity, transform);
                        if (boss.name.Equals("Troll"))
                        {
                            boss.GetComponent<Troll>().SetMoveSpeed(enemyMoveSpeed);
                        }
                        else
                        {
                            boss.GetComponent<Enemy>().SetMoveSpeed(enemyMoveSpeed);
                        }

                        print($"Enemy Spawned [{i + 1}]");
                        bossSpawned = true;
                    }
                }
            }
            audioSource.PlayOneShot(bossClip);
            bossWave++;
        }

        private int RandomEnemy()
        {
            
            if (waveCount % 5 == 0) //Boss Waves
            {
                //random number
                return GetRandomValue("boss");
            }
            else  //Normal Wave
            {
                return GetRandomValue("normal");
            }
        }

        int GetRandomValue(string wave)
        {
            if (wave.Equals("normal"))
            {
                float rand = Random.value;
                if (rand <= .8f)
                    return Random.Range(0, 2);
                if (rand <= .9f)
                    return Random.Range(2, 4);
            }
            if (wave.Equals("boss"))
            {
                float rand = Random.value;
                if (rand <= .3f)
                    return Random.Range(0, 2);
                if (rand <= .6f)
                    return Random.Range(2, 4);

                return 4;
            }
            return Random.Range(0, 2);
        }
        public void StartSpawnTimer()
        {
            if (!timerOn)
            {
                StartCoroutine(WaitForSeconds());
                timerOn = true;
            }
        }

        IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(spawnTimer);
            SpawnWave();
            timerOn = false;
        }
    }
}
