using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionSystem : MonoBehaviour
{
    [Header("Potions")]
    public int currentPots;
    public int totalPots;
    public float healAmount;

    private bool canUse;
    public float startCooldown;
    [HideInInspector] public float cooldown;

    [Header("UI")]
    public Text currentPotsText;
    public Text totalPotsText;
    public Image sprite;

    [Header("Flash")]
    private Material matWhite;
    private Material matDefault;
    private bool canFlash;
    public ParticleSystem particle;


    PlayerManager playerManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();

        //currentPots = totalPots;
        cooldown = 0;

        matWhite = Resources.Load("FlashWhite", typeof(Material)) as Material;
        matDefault = sprite.material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(canUse == true)
            {
                if (currentPots > 0 && playerManager.health != playerManager.maxHealth)
                {
                    canUse = false;
                    canFlash = true;
                    cooldown = startCooldown;

                    playerManager.Heal(healAmount);
                    currentPots -= 1;

                    playerManager.fill.SetTrigger("RedFlash");
                    FindObjectOfType<AudioManager>().Play("Swallow");
                    FindObjectOfType<AudioManager>().Play("GlassClank");
                }
            }
        }

        if (cooldown <= 0)
        {
            cooldown = 0;
        }
        if (cooldown <= 0)
        {
            canUse = true;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
        if(currentPots > totalPots)
        {
            currentPots = totalPots;
        }

        if(currentPots == 0)
        {
            sprite.gameObject.SetActive(false);
            canFlash = false;
        }
        else
        {
            sprite.gameObject.SetActive(true);
        }

        //-------------------------------------------------

        currentPotsText.text = currentPots.ToString();
        totalPotsText.text = totalPots.ToString();

        if(cooldown == 0)
        {
            sprite.fillAmount = 1;
        }
        else
        {
            sprite.fillAmount =  1 - cooldown/startCooldown;
        }

        if(cooldown == 0 && canFlash == true)
        {
            canFlash = false;
            particle.Play();
            StartCoroutine(FlashMaterial(0.1f));
        }
    }

    public IEnumerator FlashMaterial(float timeBetweenFlash)
    {
        sprite.material = matWhite;
        yield return new WaitForSeconds(timeBetweenFlash);
        sprite.material = matDefault;
    }

    public void Refill()
    {
        currentPots = totalPots;
        cooldown = 0;
    }
}
