using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : Collectable
{
    public Item item;
    public Sprite emptyItemChestSprite;

    protected override void OnCollect()
    {
        if (!collected)
        {
            base.OnCollect();

            //Gets the sprite of the current item.
            Sprite itemSprite = item.GetComponent<SpriteRenderer>().sprite;
            //If the mana potion is the blue one (1 mana restore only), then randomly choose to give 1, 2 or 3 potions.
            if (item.name == "Mana_Portion_1") {
                System.Random rnd = new System.Random();
                item.amount = rnd.Next(1, 4);
            }

            Loot tempLoot;
            //If item is not of type weapon.
            if (item.itemType != Item.ItemType.Weapons)
            {
                //Creates a loot object that stores the information of itemSprite, itemType and amount.
                tempLoot = new Loot { sprite = itemSprite, lootType = item.itemType, lootAmount = item.amount };
            }
            //If item is a weapon.
            else
            {
                //Creates a loot object that stores the information of itemSprite, itemType and amount.
                tempLoot = new Loot { sprite = itemSprite, lootType = item.itemType, lootAmount = item.amount, dmg = item.weaponDmg, push = item.pushforce, colliderOffset = item.boxColliderOffset, colliderSize = item.boxColliderSize };
            }

            bool addSuccessful = GameObject.Find("Player").GetComponent<Player>().playerInventory.AddItem(tempLoot);
            //Destroys the loot object. (removes it from the chest.)
            if (addSuccessful)
            {
                //Plays the item picked up sound
                FindObjectOfType<SoundManager>().Play("ItemPickedUp");
                //Switch chest sprite to be open chest sprite.
                GetComponent<SpriteRenderer>().sprite = emptyItemChestSprite;
                //Set item to be null, since item has been picked up.
                item = null;
            }
            else
            {
                collected = false;
            }
        }
    }
}
