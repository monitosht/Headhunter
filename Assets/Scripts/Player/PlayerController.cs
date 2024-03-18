using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerManager playerManager;
    private Rigidbody2D rb;

    private Vector3 moveDir;
    public float moveSpeed;
    public float slowSpeed;
    [HideInInspector] public bool slowed;
    [HideInInspector] public float startSpeed;

    private Vector3 rollDir;
    private float rollSpeed;
    public float startRollSpeed;
    public float rollSpeedMin;
    public float rollSpeedMultiplier;

    private bool rolled = false;
    [HideInInspector]public bool rolling;
    public float knockbackStrength = 10;
    private bool isKnocked;

    [HideInInspector]
    public float rollTimer = 0;

    public Animator DashTrail;
    public GameObject HitBox;
    public GameObject sprite;
    public Transform activeItems;
    private GameObject ghosts;
    public GameObject pressE;
    private bool canPickUp;

    public GameObject notification;

    private State state;
    private enum State
    {
        Normal,
        Rolling,
        CantMove,
    }

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
        startSpeed = moveSpeed;        
    }

    private void OnEnable()
    {
        pressEOn = false;
        pressE.SetActive(false);
    }
    private void Start()
    {
        pressEOn = false;
        pressE.SetActive(false);
    }

    private void Update()
    {
        if(rollTimer <= 0 && rolled == true) //dodge roll cooldown
        {
            rolled = false;
        }
        else
        {
            rollTimer -= Time.deltaTime;
        }

        switch (state)
        {
            case State.Normal: //player movement 

                rolling = false;

                float moveX = 0f;
                float moveY = 0f;

                if (Input.GetKey(KeyCode.W))
                {
                    moveY = +1f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    moveX = +1f;
                }

                moveDir = new Vector3(moveX, moveY).normalized;

                if(moveDir != Vector3.zero)
                {
                    sprite.GetComponent<Animator>().SetBool("Running", true);
                }
                else
                {
                    sprite.GetComponent<Animator>().SetBool("Running", false);
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    slowed = true;
                    moveSpeed = slowSpeed;
                    HitBox.SetActive(true);
                    sprite.GetComponent<GhostSprites>().enabled = true;
                    rolled = true;
                }
                else
                {
                    slowed = false;
                    moveSpeed = startSpeed;
                    HitBox.SetActive(false);
                    sprite.GetComponent<GhostSprites>().enabled = false;
                    rolled = false;
                }

                if ((Input.GetMouseButtonDown(1) && rolled == false && (rb.velocity.magnitude > 0)) || ((Input.GetKeyDown(KeyCode.Space)) && rolled == false && (rb.velocity.magnitude > 0)))
                {
                    rollDir = moveDir;
                    rollSpeed = startRollSpeed;
                    state = State.Rolling;
                    DashTrail.SetTrigger("Dash");
                }

                if(playerManager.invulnerable == true)
                {
                    gameObject.layer = 15;
                }
                else
                {
                    gameObject.layer = 8;
                }

                break;

            case State.Rolling: //dodge roll

                rolling = true;
                gameObject.layer = 13;

                rollSpeed -= rollSpeed * rollSpeedMultiplier * Time.deltaTime;
                
                if(rollSpeed < rollSpeedMin)
                {
                    state = State.Normal;
                    rolled = true;
                    rollTimer = 0.25f;
                }

                break;

            case State.CantMove:

                break;
        }

        if(state == State.Rolling)
        {
            sprite.GetComponent<GhostSprites>().enabled = true;
        }
        else if(slowed == false)
        {
            sprite.GetComponent<GhostSprites>().enabled = false;

            ghosts = GameObject.Find("CharSprite - GhostSprite");

            if (ghosts != null)
            {
                Destroy(ghosts);
            }
        }

        //------------------------------------------------

        if (Input.GetKeyDown(KeyCode.E) && item && canPickUp == true)
        {
            if (item.item.Rarity.Name == "Common")
            {
                if(PlayerStats.commonItems < playerManager.maxCommonItems)
                {
                    playerManager.inventory.AddItem(item.item);

                    if(playerManager.inventory.canDestroy == true)
                    {                        
                        Notification noti = Instantiate(notification).GetComponent<Notification>();
                        noti.item = item.item;

                        Instantiate(item.item.itemScript, activeItems);
                        FindObjectOfType<AudioManager>().Play("ItemPickup");
                        Destroy(item.gameObject);
                    }
                }
            }
            if (item.item.Rarity.Name == "Rare")
            {
                if (PlayerStats.rareItems < playerManager.maxRareItems)
                {
                    playerManager.inventory.AddItem(item.item);

                    if (playerManager.inventory.canDestroy == true)
                    {
                        Notification noti = Instantiate(notification).GetComponent<Notification>();
                        noti.item = item.item;

                        Instantiate(item.item.itemScript, activeItems);
                        FindObjectOfType<AudioManager>().Play("ItemPickup");
                        Destroy(item.gameObject);
                    }
                }
            }
            if (item.item.Rarity.Name == "Epic")
            {
                if (PlayerStats.epicItems < playerManager.maxEpicItems)
                {
                    playerManager.inventory.AddItem(item.item);

                    if (playerManager.inventory.canDestroy == true)
                    {
                        Notification noti = Instantiate(notification).GetComponent<Notification>();
                        noti.item = item.item;

                        Instantiate(item.item.itemScript, activeItems);
                        FindObjectOfType<AudioManager>().Play("ItemPickup");
                        Destroy(item.gameObject);
                    }
                }
            }
        }

        if(pressEOn == true)
        {
            pressE.SetActive(true);
        }
        else
        {
            pressE.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:

                rb.velocity = moveDir * moveSpeed;

                break;

            case State.Rolling:

                rb.velocity = rollDir * rollSpeed;

                break;
        }
    }

   [HideInInspector] public float invulTimer = 1;

    public void TakeDamage(int damage)
    {
        if (rolling == false)
        {
            if (playerManager.invulnerable == false)
            {
                playerManager.health -= damage;
                playerManager.CheckDeath();
                playerManager.invulTimer = invulTimer;

                int i = Random.Range(0, 3);
                Debug.Log(i);
                if(i == 0) { FindObjectOfType<AudioManager>().Play("PlayerGrunt1"); }
                if (i == 1) { FindObjectOfType<AudioManager>().Play("PlayerGrunt2"); }
                if (i == 2) { FindObjectOfType<AudioManager>().Play("PlayerGrunt3"); }

                CameraShake cam = FindObjectOfType<CameraShake>();
                cam.SingleShake(10f, 0.1f, 0.5f);
                playerManager.fill.SetTrigger("Flash");

                StartCoroutine(playerManager.playerFlash.FlashMaterial(playerManager.invulTimer / 4));
                FindObjectOfType<ScreenFlash>().StartFlash(0.1f, 0.75f, Color.red);

                PlayerStats.damageTaken += damage;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && rolling == false)
        {
            if(isKnocked == false)
            {
                TakeDamage(1);
                StartCoroutine(KnockedBack());
            }
        }

        IEnumerator KnockedBack()
        {
            state = State.CantMove;
            isKnocked = true;

            Vector3 dir = (collision.transform.position - transform.position) * -1;
            rb.AddForce(dir.normalized * knockbackStrength, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.5f);

            state = State.Normal;
            isKnocked = false;
        }
    }

    private Item item;

    private bool pressEOn;

    private void OnTriggerStay2D(Collider2D collision)
    {
        item = collision.GetComponent<Item>();
        canPickUp = true;

        if (collision.CompareTag("Interactable"))
        {
            pressEOn = true;
        }
        else
        {
            pressEOn = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPickUp = false;

        if (collision.CompareTag("Interactable"))
        {
            pressEOn = false;
        }
    }

    private void OnApplicationQuit()
    {
        playerManager.inventory.Container.Clear();
    }

    void Tenacious()
    {

    }
}

/*playerManager.inventory.AddItem(itemWorld.GetItem());

if (playerManager.inventory.itemInInventory == true)
{
    Debug.Log("true");                
}
else
{
    Debug.Log("false");
    GameObject script = Instantiate(itemWorld.script, transform.position, Quaternion.identity);
    script.transform.parent = GameObject.Find("Upgrades").transform;
    itemWorld.DestroySelf();
}*/
