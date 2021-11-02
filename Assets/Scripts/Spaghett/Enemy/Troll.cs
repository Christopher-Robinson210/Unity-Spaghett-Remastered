using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spaghett;

namespace Spaghett { 
    public class Troll : Spaghett.Enemy
    {
        public GameObject baby;
        public int babyCount = 2;
        public float babySpread = .4f;
        private Transform babyTransform;

        private void OnDestroy()
        {
            if (!isAlive)
            {
                SpawnBabies();
            }
            
        }

        private void SpawnBabies()
        {
            for (int i = 0; i < babyCount; i++)
            {
                bool babySpawned = false;
                while (!babySpawned)
                {
                    Vector3 babyPosition = new Vector3(Random.Range(transform.position.x - babySpread, transform.position.x + babySpread), Random.Range(transform.position.y - babySpread, transform.position.y + babySpread), transform.position.z);
                    if ((babyPosition - transform.position).magnitude < babySpread)
                    {
                        continue;
                    }
                    else
                    {
                        Instantiate(baby, babyPosition, Quaternion.identity, transform.parent);
                        print($"Spawn Baby [{i + 1}]");
                        babySpawned = true;
                    }
                }
            }
        }

    }
}
