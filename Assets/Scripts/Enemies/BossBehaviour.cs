using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float startNextAttack;
    private float nextAttack;
    public GameObject bossGFX;
    private Animator anim;

    [Header("Attack 1")]
    public GameObject attack1;
    [HideInInspector] public float stateTimer = 10f;
    //public float timeBetweenAttack;

    [Header("Attack 2")]
    public GameObject attack2;

    [Header("Attack 3")]
    public GameObject attack3;
    //public float playerCheck;

    [Header("Entry Attack")]
    public GameObject entryAttack;

    [Header("Enraged Attack")]
    public GameObject enragedAttack;
    private bool isEnraged;

    private GameObject player;
    private Boss boss;

    private State state;
    private enum State
    {
        Normal,
        Entry,
        Attack1,
        Attack2,
        Attack3,
        Enraged,
    }

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Boss1Roar");
        state = State.Entry;
        nextAttack = startNextAttack;

        player = FindObjectOfType<PlayerController>().gameObject;
        boss = GetComponent<Boss>();
        anim = bossGFX.GetComponent<Animator>();
    }

    void Update()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        int rand = Random.Range(0, 6);

        //Debug.Log(dist);
        //Debug.Log(rand);

        Debug.Log("Current state is " + state);

        switch (state)
        {
            case State.Entry: // roar 1
                
                entryAttack.GetComponent<UbhShotCtrl>().StartShotRoutine(1);

                if (stateTimer <= 0)
                {
                    AttackEnd();
                }
                else
                {
                    stateTimer -= Time.deltaTime;
                }

                break;

            case State.Attack1: // mace slam

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                MaceSlam();

                if (stateTimer <= 0)
                {
                    AttackEnd();
                }
                else
                {
                    stateTimer -= Time.deltaTime;
                }

                break;

            case State.Attack2: // lighting wave

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                attack2.GetComponent<UbhShotCtrl>().StartShotRoutine(0);

                break;

            case State.Attack3: // slash attack

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                attack3.GetComponent<UbhShotCtrl>().StartShotRoutine(2);

                if (stateTimer <= 0)
                {
                    AttackEnd();
                }
                else
                {
                    stateTimer -= Time.deltaTime;
                }

                break;

            case State.Enraged: // roar 2

                enragedAttack.GetComponent<UbhShotCtrl>().StartShotRoutine(0);

                break;

            case State.Normal: 

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                if(nextAttack <= 0)
                {
                    if(isEnraged == true)
                    {
                        if (rand == 0 || rand == 1)
                        {
                            state = State.Enraged;

                            FindObjectOfType<AudioManager>().Play("Boss1Roar");
                        }
                        if (rand == 2)
                        {
                            state = State.Attack2;
                        }
                        if (rand == 3 || rand == 4)
                        {
                            state = State.Attack3;
                            stateTimer = 4f;
                        }
                        if (rand == 5)
                        {
                            state = State.Attack1;
                            stateTimer = 5f;
                        }
                    }
                    else
                    {
                        if (rand == 0 || rand == 1)
                        {
                            state = State.Attack1;
                            stateTimer = 10f;
                        }
                        if (rand == 2)
                        {
                            state = State.Attack2;
                        }
                        if (rand == 3 || rand == 4) // && dist < playerCheck
                        {
                            state = State.Attack3;
                            stateTimer = 8f;
                        }
                        if (rand == 5)
                        {
                            state = State.Entry;
                            stateTimer = 3f;

                            FindObjectOfType<AudioManager>().Play("Boss1Roar");
                        }
                    }

                }
                else
                {
                    nextAttack -= Time.deltaTime;
                }

                break;
        }

        if(boss.health <= boss.enrageThreshhold && isEnraged == false)
        {
            //UbhObjectPool.instance.RemoveAllPool();

            speed *= 1.5f;
            startNextAttack -= 1;

            FindObjectOfType<AudioManager>().Play("Boss1Roar");
            state = State.Enraged;
            isEnraged = true;
        }

        if (Input.GetKeyDown(KeyCode.N) && state != State.Attack3)
        {
            state = State.Attack3;
            stateTimer = 8f;
        }
        else if (Input.GetKeyDown(KeyCode.N) && state == State.Attack3)
        {
            state = State.Normal;
        }

        if(state == State.Entry || state == State.Enraged)
        {
            anim.SetBool("Roar", true);
        }
        else
        {
            anim.SetBool("Roar", false);
        }
    }

    void MaceSlam()
    {
        boss.sprite.GetComponent<Animator>().SetTrigger("Attack1");
        attack1.GetComponent<UbhShotCtrl>().StartShotRoutine(2);
    }

    IEnumerator Attack1(float timeBetween)
    {
        attack1.GetComponent<UbhShotCtrl>().StartShotRoutine(2);

        yield return new WaitForSeconds(timeBetween);

        attack1.GetComponent<UbhShotCtrl>().StartShotRoutine(0);

        yield return new WaitForSeconds(timeBetween);

        attack1.GetComponent<UbhShotCtrl>().StartShotRoutine(0);
    }

    public void AttackEnd()
    {
        state = State.Normal;
        nextAttack = startNextAttack;
    }
    public void RemoveBullets()
    {
        UbhObjectPool.instance.RemoveAllPool();
    }
    public void PlaySwing()
    {
        FindObjectOfType<AudioManager>().Play("Boss1Swing");
    }
    public void PlaySlam()
    {
        FindObjectOfType<AudioManager>().Play("BossSlam");
    }
    public void SlashAnim()
    {
        anim.SetTrigger("SlashAttack");
    }
}
