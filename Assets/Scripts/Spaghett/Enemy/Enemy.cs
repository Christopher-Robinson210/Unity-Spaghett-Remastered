using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spaghett
{

    public class Enemy : MonoBehaviour
    {
        [HideInInspector]public AudioSource audioSource;
        public AudioClip audioClip;
        public int clicksToKill;
        public float speed = 1f;
        public int damage = 5;
        public float attackDelay = 2f;
        [HideInInspector]public GameObject target;
        [HideInInspector]public bool isCollide;
        [HideInInspector]public bool isAlive = true;
        private bool canDamage;

        private void Awake()
        {
            audioSource = transform.parent.GetComponent<AudioSource>();
            target = GameObject.Find("Spaghett");
            //print($"Name: {gameObject.name} Created!\nHealth: {health}");
            isCollide = false;
            canDamage = true;
            print($"{name} spawned at: {transform.position}");
        }

        private void FixedUpdate()
        {
            Move();
            IsAlive();
            if (name.Equals("Troll"))
            {
                print($"{name}: {transform.position}");
            }
 
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            isCollide = true;
            if (canDamage)
            {
                Attack();
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            isCollide = false;
        }
        public virtual void Move()
        {
            if (!isCollide)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }

        }

        public void SetMoveSpeed(float moveSpeed)
        {
            speed = moveSpeed;
        }

        //Attack Player. Is virtual in case I need to override in future.
        public virtual void Attack()
        {
            if (isCollide)
            {
                StartCoroutine(WaitForSeconds());
                Player.health -= damage;
                print($"Player Health: {Player.health}");
            }
        }

        IEnumerator WaitForSeconds()
        {
            canDamage = false;
            yield return new WaitForSecondsRealtime(1f);
            canDamage = true;
            
        }

        private void OnMouseDown()
        {
            if (Input.GetButton("Fire1"))
            {
                Hit();
            }
        }
        public virtual void Hit()
        {
            clicksToKill--;
            print($"HIT {gameObject.name}! \nHealth: {clicksToKill}");
            IsAlive();
        }


        //Check if alive. Is virtual in case I need to override in future.
        public virtual bool IsAlive()
        {
            if (clicksToKill > 0)
            {
                isAlive = true;
                return true;
            }
            else
            {
                isAlive = false;
                Spaghett.GameManager.killCount++;
                audioSource.PlayOneShot(audioClip);
                Destroy(gameObject);
                return false;
            }
        }
    }
}