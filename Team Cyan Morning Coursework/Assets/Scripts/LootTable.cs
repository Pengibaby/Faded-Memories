using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public int[] lootTable; //Table of drop chances/weights.
    public GameObject[] lootPrefabs; //Table of items corresponding to the drop chance table slots.

    private int total = 0; //Total chance. Should be 100%.
    private int randomNumber; //Random number.

    private Vector3 deathPosition; //Death position of the enemy.

    //Function to drop an item.
    public void DropItem()
    {
        //Gets the death position of the enemy.
        deathPosition = gameObject.transform.position;

        //Gets the total weight/drop chances.
        for (int i = 0; i < lootTable.Length; i++)
        {
            total += lootTable[i];
        }

        //If total weight is larger than 100, set it to 100.
        if(total > 100)
        {
            total = 100;
        }

        //Gets a random number between 0 (inclusive) and total (exclusive)
        randomNumber = Random.Range(0, total);

        //Loops through the loot table.
        for(int i = 0; i < lootTable.Length; i++)
        {
            //If the random number is equal to or lower than the drop chance/weight. Drop an item.
            if(randomNumber <= lootTable[i])
            {
                //Checks to make sure the item is not null. Otherwise, drop nothing.
                if(lootPrefabs[i] != null)
                {
                    //Need to change direction to random direction.
                    Vector3 direction = Random.insideUnitCircle.normalized;
                    //Instantiate the GameObject.
                    GameObject lootDrop = Instantiate(lootPrefabs[i], deathPosition + direction * 0.05f, Quaternion.identity);
                    lootDrop.AddComponent<Rigidbody2D>();
                    lootDrop.GetComponent<Rigidbody2D>().AddForce(direction * 0.75f, ForceMode2D.Impulse);
                    lootDrop.GetComponent<Rigidbody2D>().drag = 1;
                    lootDrop.GetComponent<Rigidbody2D>().gravityScale = 0;

                    Destroy(lootDrop.GetComponent<Rigidbody2D>(), 0.5f);
                }
                //If the weight 100, then check the next one. otherwise, break the loop.
                if (lootTable[i] != 100)
                {
                    //Break the loop.
                    break;
                }
            }
            //If the number is larger, do the following.
            else
            {
                //Subtract the drop chance from the random number.
                randomNumber -= lootTable[i];
            }
        }
    }
}
