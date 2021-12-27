using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RightClickUIButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] listOfPrefabs;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Only Runs if the game is not paused and there is no transition screens active.
            if (PauseMenu.isGamePaused == false && GameObject.Find("Canvas").GetComponent<TransitionUI>().GetTransitionUI().activeSelf == false)
            {
                //Gets the game object transform.
                Transform gameObj = gameObject.transform;
                //Sets the value of the x and y position of the item slot based on the values stored in the hidden text.
                string xStr = gameObj.Find("x").GetComponent<TextMeshProUGUI>().text;
                string yStr = gameObj.Find("y").GetComponent<TextMeshProUGUI>().text;

                //sets the index of the inventory item in the inventory list to be -1 by default.
                int index = -1;
                float x = float.Parse(xStr); //converts string to float for x.
                float y = float.Parse(yStr); //converts string to float for y.
                //If y is -1.06f, meaning if item slot is in the first row.
                if (y == -1.06f)
                {
                    //index is the value of x - 0.95.
                    index = (int)(x - 0.95f);
                }
                //If item slot is on the second row.
                else if (y == -2.06f)
                {
                    //index is the value of x - 0.95 + 3.
                    index = (int)((x - 0.95f) + 3f);
                }

                //Checks to make sure x is not -1, then remove item from the inventory.
                if (index >= 0)
                {
                    Loot item = GameObject.Find("Player").GetComponent<Player>().playerInventory.DropItem(index);
                    //Drop the item.
                    bool successful = DropItem(item);
                    //Plays the item consumption sound if item used successfully.
                    //FindObjectOfType<SoundManager>().Play("ItemConsumption");
                    Debug.Log("Right Clicked: " + index + ", " + item.lootType);
                }
            }
        }
    }

    //Function that uses the item removed from the inventory.
    private bool DropItem(Loot item)
    {
        //Successfully dropped the item?
        bool successful = false;
        int prefabIndex = -1;
        //Loop through the list of prefabs.
        for(int i = 0; i < listOfPrefabs.Length; i++)
        {
            //If the item type is the same
            if(listOfPrefabs[i].GetComponent<Item>().itemType == item.lootType)
            {
                //Check if the item is a weapon, it it is not, set the prefabindex and break.
                if(item.lootType != Item.ItemType.Weapons)
                {
                    prefabIndex = i;
                    break;
                }
                //if it is a weapon, do the following.
                else
                {
                    //Check if the dmg and push force is the same.
                    if(listOfPrefabs[i].GetComponent<Weapon>().damagePoint == item.dmg && listOfPrefabs[i].GetComponent<Weapon>().pushForce == item.push)
                    {
                        //If both are the same, this is the weapon, so set the prefabindex and break.
                        prefabIndex = i;
                        break;
                    }
                }
            }
        }

        //If the prefab index is not -1.
        if(prefabIndex != -1)
        {
            //Need to change direction to random direction.
            Vector3 direction = direction = Random.insideUnitCircle.normalized;
            //Instantiate the GameObject.
            GameObject lootDrop = Instantiate(listOfPrefabs[prefabIndex], GameObject.Find("Player").transform.position + direction * 0.2f, Quaternion.identity);
            //Set the amount to be the amount dropped.
            lootDrop.GetComponent<Item>().amount = item.lootAmount;
            //Item dropped successfully.
            successful = true;
        }

        return successful;
    }
}
