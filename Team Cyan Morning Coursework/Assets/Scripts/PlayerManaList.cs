using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A class to store the current and max Mana of the player as a list.
/*
 * Technically a useless class, this is mainly a way to trigger the event that refreshes the Mana bar UI. otherwise, it's a redundant class.
 * DO NOT delete this class though, otherwise Mana bar will not work.
 */
public class PlayerManaList
{
    //Creates an event that will trigger when mana values change.
    public event EventHandler onManaChange;

    private List<int> manaList; //List for storing mana values.

    //Constructor.
    public PlayerManaList()
    {
        manaList = new List<int>();
    }

    //Updates the Mana values.
    public void ChangeMana(int current, int max)
    {
        manaList[0] = current;
        manaList[1] = max;

        //triggers the event. Which calls the function in Mana_Bar_UI that refreshes the Mana bar UI.
        onManaChange?.Invoke(this, EventArgs.Empty);
    }

    //Getter method that returns the list.
    public List<int> GetManaList()
    {
        return manaList;
    }
}
