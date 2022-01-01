using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    public Inventory playerInventory;
    private PlayerHealthList healthList; //health list of the player.
    private PlayerManaList manaList; //mana list of th eplayer.
    [SerializeField] private Inventory_UI inventoryUi;
    [SerializeField] private Health_Bar_UI healthBarUI;
    [SerializeField] private Mana_Bar_UI manaBarUI;
    public Animator animator;

    public int mana; //Player's current mana.
    public int maxMana; //player's max mana.

    [HideInInspector]
    public Loot currentWeapon;

    private void Awake()
    {
        // Load the player resources
        if (GameManager.Instance.playerInventory != null)
        {
            playerInventory = GameManager.Instance.playerInventory;
        }
        else 
        {
            playerInventory = new Inventory();
        }
        gameObject.GetComponent<GoldAmount>().goldAmount = GameManager.Instance.goldAmount;
        hitpoint = GameManager.Instance.hitpoint;
        mana = GameManager.Instance.mana; //Loads the current mana from the GameManager.
        healthList = new PlayerHealthList();
        manaList = new PlayerManaList(); //Initializes the manaList.

        // Testing: print list of items
        List<Loot> listOfItems = GameObject.Find("Player").GetComponent<Player>().playerInventory.GetListOfItems();
        for (int i = 0; i < listOfItems.Count; i++)
        {
            Debug.Log(listOfItems[i].lootType + ", " + listOfItems[i].lootAmount);
        }

        // Initialise inventory UI
        if (inventoryUi != null)
        {
            inventoryUi.SetInventory(playerInventory);
        }
        if (healthBarUI != null)
        {
            //Sets the initial values of current health and max health.
            healthList.GetHealthList().Add(hitpoint);
            healthList.GetHealthList().Add(maxHitpoint);
            healthList.ChangeHealth(hitpoint, maxHitpoint);
            healthBarUI.SetHealthStats(healthList);
        }
        if (manaBarUI != null) {
            //Sets the initial values of the current mana and max mana.
            manaList.GetManaList().Add(mana);
            manaList.GetManaList().Add(maxMana);
            manaList.ChangeMana(mana, maxMana);
            manaBarUI.SetManaStats(manaList);
        }

        //Load the player's current weapon.
        if (GameObject.Find("Player_Knife") != null) {
            //Find the player knife object.
            GameObject playerWeapon = GameObject.Find("Player_Knife");
            //If the GameManager currentWeapon instance is not null
            if(GameManager.Instance.currentWeapon != null)
            {
                //Set the stats and other info of the weapon to be the one from the inventory.
                playerWeapon.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.currentWeapon.sprite;
                playerWeapon.GetComponent<Weapon>().damagePoint = GameManager.Instance.currentWeapon.dmg;
                playerWeapon.GetComponent<Weapon>().pushForce = GameManager.Instance.currentWeapon.push;
                playerWeapon.GetComponent<Weapon>().SetCooldown(GameManager.Instance.currentWeapon.cooldown);
                playerWeapon.GetComponent<BoxCollider2D>().offset = GameManager.Instance.currentWeapon.colliderOffset;
                playerWeapon.GetComponent<BoxCollider2D>().size = GameManager.Instance.currentWeapon.colliderSize;
            }
        }

        //Disable the audio source at the beginning, so no sound plays.
        GetComponent<AudioSource>().enabled = false;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed / 10;
        float y = Input.GetAxisRaw("Vertical") * speed / 10;

        //If the animator is active, set the float parameter to be the current speed maginitude of the player, to trigger animations.
        if (animator.gameObject.activeSelf)
        {
            float magnitudeSpeed = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            animator.SetFloat("Speed", magnitudeSpeed);

            //Plays running sound if the player has a decent speed.
            if (magnitudeSpeed > 0.1)
            {
                //If the audio source is disabled, enable it.
                if (GetComponent<AudioSource>().isActiveAndEnabled == false)
                {
                    GetComponent<AudioSource>().enabled = true;
                }
                //Unpause the sound.
                GetComponent<AudioSource>().UnPause();
            }
            //If the speed is less that 0.1, pause the running sound.
            else 
            {
                GetComponent<AudioSource>().Pause();
            }
        }

        UpdateMotor(new Vector3(x, y, 0));
    }

    // Save player's inventory to GameManager
    public void SavePlayer()
    {
        GameManager.Instance.playerInventory = playerInventory;
        GameManager.Instance.goldAmount = gameObject.GetComponent<GoldAmount>().goldAmount;
        GameManager.Instance.hitpoint = hitpoint;
        GameManager.Instance.mana = mana;
        GameManager.Instance.currentWeapon = currentWeapon;
    }
}