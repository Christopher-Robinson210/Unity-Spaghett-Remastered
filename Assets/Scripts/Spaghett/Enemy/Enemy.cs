using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spaghett
{

    public class Enemy : MonoBehaviour
    {
        private ParticleSystem ps;
        [HideInInspector]public AudioSource audioSource;
        public AudioClip audioClip;
        public AudioClip hitClip;
        public int clicksToKill;
        public float speed = 1f;
        public int damage = 5;
        public float attackDelay = 2f;
        [HideInInspector]public GameObject target;
        [HideInInspector]public bool isCollide;
        [HideInInspector]public bool isAlive = true;
        private bool canDamage;
        private bool shake = false;
        private bool isBoom = false;
        private float y = 0f;

        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
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
            Shake();
            Death();
 
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
            shake = true;
            IsAlive();
            if (isAlive)
            {
                audioSource.PlayOneShot(hitClip);
            }

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
                
                //Destroy(gameObject);
                return false;
            }
        }

        public void Shake()
        {
            if (shake)
            {
                StartCoroutine(DoShake());
            }
        }

        public void Death()
        {
            if (!isAlive && !isBoom)
            {
                StartCoroutine(Boom());
            }
        }

        IEnumerator Boom()
        {
            isBoom = true;
            ps.Play(true);
            GetComponent<SpriteRenderer>().forceRenderingOff = true;
            audioSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(.25f);
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Destroy(gameObject);
            Spaghett.GameManager.killCount++;

        }

        IEnumerator DoShake()
        {
            y = transform.localPosition.y;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - .01f, 0);
            //ps.Play();
            yield return new WaitForSeconds(.05f);
            transform.localPosition = new Vector3(transform.localPosition.x, y + .01f, 0);
            //ps.Stop(true,ParticleSystemStopBehavior.StopEmitting);
            shake = false;
        }
    }
}