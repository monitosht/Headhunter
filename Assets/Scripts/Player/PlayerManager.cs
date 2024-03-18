using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;
    GunController gunController;
    PlayerController playerController;
    private GunSwitch gunSwitch;

    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public PlayerFlash playerFlash;

    [Header("Player Health")]
    public float maxHealth;
    public float health;
    public Animator fill;
    public Slider healthBar;
    public TMP_Text healthBarText;

    public GameObject reloadingText;
    /*public float hearts;

    public Sprite heart;
    [HideInInspector] public Sprite emptyHeart;
    public Image[] heartImage;*/

    [HideInInspector] public float invulTimer;
    [HideInInspector] public bool invulnerable;

    [Header("Player UI")]
    public Slider reloadCooldown;
    //public Slider dodgeCooldown;
    public Text currentAmmo, maxAmmo, totalAmmo;
    public Text creditsText;

    public int maxEpicItems;
    public int maxRareItems;
    public int maxCommonItems;

    //[HideInInspector] public Inventory inventoryOld;
    //[SerializeField] private InventoryUI inventoryUI;

    public InventoryObject inventory;

    private void Awake()
    {
        //inventory = new Inventory();
        //inventoryUI.SetInventory(inventoryOld);
    }

    private void Start()
    {
        FindPlayer();
        health = maxHealth;

        //pauseScreen.SetActive(false);
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerFlash = player.transform.GetChild(0).GetComponent<PlayerFlash>();
        playerController = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();

        gunSwitch = FindObjectOfType<GunSwitch>();

        audioManager = FindObjectOfType<AudioManager>();
    }
    void FindGun()
    {
        gunController = FindObjectOfType<GunController>().gameObject.GetComponent<GunController>();
    }

    void Update()
    {
        FindGun();

        if (invulTimer <= 0)
        {
            invulnerable = false;
        }
        else
        {
            invulnerable = true;
            invulTimer -= Time.deltaTime;
        }

        //-------------------------------------------- UI

        reloadCooldown.maxValue = gunController.startReloadTime;
        reloadCooldown.value = gunController.reloadTime;

        if (reloadCooldown.value <= 0)
        {
            reloadCooldown.gameObject.SetActive(false);
            reloadingText.SetActive(false);
        }
        else
        {
            reloadCooldown.gameObject.SetActive(true);
            reloadingText.SetActive(true);
        }

        currentAmmo.text = gunController.currentAmmo.ToString();
        maxAmmo.text = gunController.maxAmmo.ToString();
        creditsText.text = ": " + PlayerStats.credits.ToString();

        if(gunSwitch.selectedGun == 0)
        {
            //totalAmmo.text = gunController.totalAmmo.ToString();
            totalAmmo.text = "XXX";
        }
        else
        {
            totalAmmo.text = gunController.totalAmmo.ToString();
        }

        //-------------------------------------------- Health

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        healthBarText.text = health + "/" + maxHealth;

        //-------------------------------------------- paused

        //PauseScreen();
    }

    void CheckOverheal()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void Heal(float heal)
    {
        health += heal;
        CheckOverheal();
        PlayerStats.healingDone += heal;
    }
    public void CheckDeath()
    {
        if(health <= 0)
        {
            health = maxHealth;
            PlayerStats.totalDeaths++;
            SceneManager.LoadScene("HH_GAMEOVER");
        }
    }
}
