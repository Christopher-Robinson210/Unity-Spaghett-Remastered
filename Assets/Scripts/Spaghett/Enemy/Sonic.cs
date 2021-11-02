using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spaghett
{
    public class Sonic : Enemy
    {
        public AudioClip moveClip;
        public AudioClip noMoveClip;
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
                if (DoJump())
                {
                    if ((transform.position - target.transform.position).magnitude > 2.5f)
                    {
                        bool hasMoved = false;
                        int loops = 0;
                        while (!hasMoved)
                        {
                            if (loops < 25)
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
                    }
                }
                else
                {
                    audioSource.PlayOneShot(noMoveClip);
                }
            }
        }
    }
}