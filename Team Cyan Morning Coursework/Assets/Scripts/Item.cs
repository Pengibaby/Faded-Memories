using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class to represent an item that can be picked up by the player.
 */
public class Item : Collectable
{
    private Sprite itemSprite; // Sprite of the item.

    //Enumeration of item types. Can add more, as long as these items are meant to despawn when the player picks it up. i.e. so not chests.
    public enum ItemType { 
        Apple,
        Berry,
        ManaPotion1,
        ManaPotion2,
        Weapons
    }
    //Can set the itemtype and amount in the unity editor.
    public ItemType itemType;
    public int amount;

    //Weapon only info.
    [HideInInspector]
    public int weaponDmg;
    [HideInInspector]
    public float pushforce;
    [HideInInspector]
    public float weaponCooldown;
    [HideInInspector]
    public Vector2 boxColliderOffset;
    [HideInInspector]
    public Vector2 boxColliderSize;

    //Overrides the OnCollect function from Collectables class.
    protected override void OnCollect() {
        if (!collected)
        {
            //Calls the parent's OnCollect function to set collect to be true.
            base.OnCollect();
            //Gets the sprite of the current item.
            itemSprite = this.GetComponent<SpriteRenderer>().sprite;
            Loot tempLoot;
            //If not weapon, but an item, either healing item or mana item.
            if (this.GetComponent<Weapon>() == null)
            {
                //Creates a loot object that stores the information of itemSprite, itemType and amount.
                tempLoot = new Loot { sprite = itemSprite, lootType = itemType, lootAmount = amount };
            }
            //If item is weapon.
            else
            {
                //Get weapon info from the item colliding with the player.
                weaponDmg = this.GetComponent<Weapon>().damagePoint;
                pushforce = this.GetComponent<Weapon>().pushForce;
                weaponCooldown = this.GetComponent<Weapon>().GetCooldown();
                boxColliderOffset = this.GetComponent<BoxCollider2D>().offset;
                boxColliderSize = this.GetComponent<BoxCollider2D>().size;
                //Creates a loot object that stores the information of itemSprite, itemType, amount, dmg, push force, collider offset and collider size.
                tempLoot = new Loot { sprite = itemSprite, lootType = itemType, lootAmount = amount, dmg = weaponDmg, push = pushforce, cooldown = weaponCooldown, colliderOffset = boxColliderOffset, colliderSize = boxColliderSize};
            }
            //Adds the loot to the player inventory. And stores a bool to check if add is sucessful.
            bool addSuccessful = GameObject.Find("Player").GetComponent<Player>().playerInventory.AddItem(tempLoot);
            //Destroys the game object. (removes it from the map.)
            if (addSuccessful)
            {
                //Displays what was grabbed.
                Debug.Log("Grabbed a " + itemType);

                //Plays the item picked up sound
                FindObjectOfType<SoundManager>().Play("ItemPickedUp");

                Destroy(this.gameObject);
            }
            else 
            {
                collected = false;
            }
        }
    }
}
