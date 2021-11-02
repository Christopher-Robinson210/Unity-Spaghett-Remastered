using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spaghett
{

    public class Player : MonoBehaviour
    {
        public static int health = 100;
        private Spaghett.GameManager gameManager;
        private bool isAlive;

        private void Awake()
        {
            gameManager = gameObject.GetComponent<Spaghett.GameManager>();
            isAlive = true;
        }

        private void Update()
        {
            if(health <= 0 && isAlive)
            {
                print("YOU DIED! GAME OVER!");
                isAlive = false;
                gameManager.GameOver();
                Time.timeScale = 0;
            }
        }
    }
}