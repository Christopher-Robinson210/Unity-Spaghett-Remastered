using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spaghett
{
    public class Sonic : Enemy
    {
        public AudioClip moveClip;
        public AudioClip noMoveClip;
        private bool doJump;
        private void Start()
        {
            doJump = DoJump();
        }
        public override void Move()
        {
            if (!isCollide)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }
        }

        public bool DoJump()
        {
            return (Random.Range(0, 2) == 0);
        }

        public override void Hit()
        {
            clicksToKill--;
            IsAlive();
            if (isAlive)
            {
                if (doJump)
                {
                    StartCoroutine(DelayTP());
                    // decide how to teleport
                    //DoTeleport();
                }
                else
                {
                    SonicFlip();
                    audioSource.PlayOneShot(noMoveClip);
                }
            }
        }

        void SonicFlip()
        {
            StartCoroutine(DelayFlip());
        }

        void DoTeleport()
        {
            if (doJump)
            {
                if ((transform.position - target.transform.position).magnitude > 2.5f)
                {
                    bool hasMoved = false;
                    int loops = 0;
                    while (!hasMoved)
                    {
                        if (loops < 50)
                        {
                            Vector3 sonicPosition = new Vector3(Random.Range((Mathf.Abs(transform.position.x) * -1), Mathf.Abs(transform.position.x)), Random.Range((Mathf.Abs(transform.position.y) * -1), Mathf.Abs(transform.position.y)), 0f);
                            if ((sonicPosition - target.transform.position).magnitude < 2f)
                            {
                                loops++;
                                continue;
                            }
                            else
                            {
                                transform.position = sonicPosition;
                                audioSource.PlayOneShot(moveClip);
                                hasMoved = true;
                                doJump = false;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    transform.position = new Vector3((transform.position.x * -1), (transform.position.y * -1), 0f);
                    audioSource.PlayOneShot(moveClip);
                    doJump = false;
                }
            }
        }

        IEnumerator DelayFlip()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.flipX = false;
        }

        IEnumerator DelayTP()
        {
            Vector3 temp = transform.localScale;
            transform.localScale = new Vector3(transform.localScale.x + .03f, transform.localScale.y + .03f, 0f);
            yield return new WaitForSecondsRealtime(0.35f);
            transform.localScale = temp;
            DoTeleport();
        }
    }
}