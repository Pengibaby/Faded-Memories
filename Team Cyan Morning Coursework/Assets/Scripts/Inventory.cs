using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class to represent the inventory of the player.
 */
public class Inventory
{
    //Creates an event that will trigger when item is added to the inventory.
    public event EventHandler OnItemListChange;

    //A list of Loot to store items picked up by the player.
    private List<Loot> listOfItems;

    //Constructor to initialize the list.
    public Inventory() {
        listOfItems = new List<Loot>();
    }

    //A function to add items to the inventory.
    public bool AddItem(Loot loot) {
        bool addSuccessful = false; //Loot has not been added to the inventory yet, so this is false by default.
        bool containsLoot = false; //Bool to indicate whether the item to be added is in the inventory already or not.
        int index = -1; //Index of the already added item.
        //Loops through the list and check if the item is already in the inventory.
        for (int i = 0; i < listOfItems.Count; i++)
        {
            if (listOfItems[i].lootType == loot.lootType)
            {
                containsLoot = true;
                index = i;
                break;
            }
        }
        //If item not in inventory, add it.
        if (containsLoot == false)
        {
            //Max of 6 items in inventory.
            if (listOfItems.Count < 6)
            {
                listOfItems.Add(loot);
                addSuccessful = true;
            }
        }
        //If item is in inventory, do the following.
        else
        {
            //If item is stackable, increase the amount.
            if (loot.IsStackable())
            {
                listOfItems[index].lootAmount = listOfItems[index].lootAmount + loot.lootAmount;
                addSuccessful = true;
            }
            //If the item is not stackable, just add it to the inventory.
            else
            {
                //Max of 6 items in inventory.
                if (listOfItems.Count < 6)
                {
                    listOfItems.Add(loot);
                    addSuccessful = true;
                }
            }
        }

        //triggers the event. Which calls the function in Inventory_UI that refreshes the inventory UI.
        OnItemListChange?.Invoke(this, EventArgs.Empty);

        //Temp: Prints out the inventory list to console.
        for (int i = 0; i < listOfItems.Count; i++)
        {
            Debug.Log(listOfItems[i].lootType + ", " + listOfItems[i].lootAmount);
        }

        return addSuccessful;
    }

    //Funtion to remove 1 item from the inventory.
    public Loot RemoveItem(int index) {
        //Sets the return value to be null.
        Loot temp = null;
        Loot temptemp = null;
        //Make sure the index is less than 6, since max inventory space is 6.
        if (index < 6) {
            //Set temp to be the item to be removed.
            temp = listOfItems[index];
            //Check if the item is stackable. i.e. it is not a weapon.
            if (listOfItems[index].IsStackable())
            {
                //Check if the stackable item has count of 2 or more.
                if (listOfItems[index].lootAmount > 1)
                {
                    //Decrease the count by 1.
                    listOfItems[index].lootAmount = listOfItems[index].lootAmount - 1;
                }
                //If the count was 1, just remove it.
                else
                {
                    listOfItems.RemoveAt(index);
                }
            }
            //if it is not stackable, i.e. a weapon, do the following.
            else 
            {
                //Find the current player equip weapon.
                GameObject playerWeapon = GameObject.Find("Player_Knife");
                //Saves the current weapon from the inventory in a new loot item.
                temptemp = new Loot { sprite = temp.sprite, lootType = temp.lootType, lootAmount = temp.lootAmount, dmg = temp.dmg, push = temp.push, cooldown = temp.cooldown, colliderOffset = temp.colliderOffset, colliderSize = temp.colliderSize };
                //Set the weapon item in the inventory to be the player equiped weapon info.
                listOfItems[index].sprite = playerWeapon.GetComponent<SpriteRenderer>().sprite;
                listOfItems[index].lootType = Item.ItemType.Weapons;
                listOfItems[index].lootAmount = 1;
                listOfItems[index].dmg = playerWeapon.GetComponent<Weapon>().damagePoint;
                listOfItems[index].push = playerWeapon.GetComponent<Weapon>().pushForce;
                listOfItems[index].cooldown = playerWeapon.GetComponent<Weapon>().GetCooldown();
                listOfItems[index].colliderOffset = playerWeapon.GetComponent<BoxCollider2D>().offset;
                listOfItems[index].colliderSize = playerWeapon.GetComponent<BoxCollider2D>().size;
            }
        }

        //triggers the event. Which calls the function in Inventory_UI that refreshes the inventory UI.
        OnItemListChange?.Invoke(this, EventArgs.Empty);

        //Temp: Prints out the inventory list to console.
        for (int i = 0; i < listOfItems.Count; i++)
        {
            Debug.Log(listOfItems[i].lootType + ", " + listOfItems[i].lootAmount);
        }

        //Avoid altering the removed object by direct reference.
        Loot duplicateItem;
        //If the loot is not a weapon
        if (temp.lootType != Item.ItemType.Weapons)
        {
            duplicateItem = new Loot { sprite = temp.sprite, lootType = temp.lootType, lootAmount = temp.lootAmount };
        }
        //If loot is a weapon.
        else
        {
            duplicateItem = temptemp;
        }
        //Returns the removed item (duplicated from the actual removed item.
        return duplicateItem;
    }

    //Funtion to drop all of the right clicked item from the inventory.
    public Loot DropItem(int index)
    {
        //Sets the return value to be null.
        Loot temp = null;
        //Make sure the index is less than 6, since max inventory space is 6.
        if (index < 6)
        {
            //Set temp to be the item to be dropped.
            temp = listOfItems[index];

            //Completely drops the item from the inventory.
            listOfItems.RemoveAt(index);
        }

        //triggers the event. Which calls the function in Inventory_UI that refreshes the inventory UI.
        OnItemListChange?.Invoke(this, EventArgs.Empty);

        //Temp: Prints out the inventory list to console.
        for (int i = 0; i < listOfItems.Count; i++)
        {
            Debug.Log(listOfItems[i].lootType + ", " + listOfItems[i].lootAmount);
        }

        //Avoid altering the dropped object by direct reference.
        Loot duplicateItem;
        //If the loot is not a weapon
        if (temp.lootType != Item.ItemType.Weapons)
        {
            duplicateItem = new Loot { sprite = temp.sprite, lootType = temp.lootType, lootAmount = temp.lootAmount };
        }
        //If loot is a weapon.
        else
        {
            duplicateItem = new Loot { sprite = temp.sprite, lootType = temp.lootType, lootAmount = temp.lootAmount, dmg = temp.dmg, push = temp.push, cooldown = temp.cooldown, colliderOffset = temp.colliderOffset, colliderSize = temp.colliderSize };
        }
        //Returns the dropped item (duplicated from the actual removed item.
        return duplicateItem;
    }

    public List<Loot> GetListOfItems() {
        return listOfItems;
    }
}
