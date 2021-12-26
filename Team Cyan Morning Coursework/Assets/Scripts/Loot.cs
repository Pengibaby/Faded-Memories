using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class to represent the item/loot being picked up by the player.
 */
public class Loot
{
    //Stores the sprite of the loot.
    public Sprite sprite;
    //Loot type: So far can be Apple or Berry.
    public Item.ItemType lootType;
    //Amount picked up.
    public int lootAmount;

    //Item effect.
    private int healingAmount;
    private int manaAmount;

    //Weapon loot.
    public int dmg;
    public float push;
    public Vector2 colliderOffset;
    public Vector2 colliderSize;

    //A function that returns the sprite of the loot.
    public Sprite GetSprite() {
        return sprite;
    }

    //A function to check if the item is stackable.
    public bool IsStackable() {
        switch (lootType) {
            default:
                return false;
            case Item.ItemType.Apple:
            case Item.ItemType.Berry:
            case Item.ItemType.ManaPotion1:
            case Item.ItemType.ManaPotion2:
                return true;
            case Item.ItemType.Weapons:
                return false;
        }
    }

    public int GetHealingAmount() {
        switch (lootType)
        {
            default:
                healingAmount = 0;
                return healingAmount;
            case Item.ItemType.Apple:
                healingAmount = 1;
                return healingAmount;
            case Item.ItemType.Berry:
                healingAmount = 2;
                return healingAmount;
            case Item.ItemType.ManaPotion1:
            case Item.ItemType.ManaPotion2:
            case Item.ItemType.Weapons:
                healingAmount = 0;
                return healingAmount;
        }
    }

    public int GetManaAmount()
    {
        switch (lootType)
        {
            default:
                manaAmount = 0;
                return manaAmount;
            case Item.ItemType.Apple:
            case Item.ItemType.Berry:
            case Item.ItemType.Weapons:
                manaAmount = 0;
                return manaAmount;
            case Item.ItemType.ManaPotion1:
                manaAmount = 1;
                return manaAmount;
            case Item.ItemType.ManaPotion2:
                manaAmount = 2;
                return manaAmount;
        }
    }
}
