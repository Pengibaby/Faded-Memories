using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana_Bar_UI : MonoBehaviour
{
    private PlayerManaList manaList; //Manalist object.
    private Transform Mana_Bar_Group; //Mana_Bar_group gameObject from the unity hierarchy.
    private Transform Mana_Bar; //Block_1 gameObject from the unity hierarchy.
    [SerializeField] private Sprite fullBar; //Sprite of the mana bar block.

    private void Awake()
    {
        Mana_Bar_Group = transform.Find("Mana_Bar_Blocks");
        Mana_Bar = Mana_Bar_Group.Find("Block_1");
    }

    public void SetManaStats(PlayerManaList manaList)
    {
        //The health list contains the current health value (list[0]) and max health value (list[1]).
        this.manaList = manaList;

        //Subriscribe to the event in UIButtonClick.cs.
        this.manaList.onManaChange += ManaBar_onManaChange;
        RefreshManaBarUI();
    }

    //On triggering the event in UIButtonClick.cs and , calls the function RefreshHealthBarUI().
    private void ManaBar_onManaChange(object sender, System.EventArgs e)
    {
        RefreshManaBarUI();
    }

    public void RefreshManaBarUI()
    {
        //Checks to see if the Mana_Bar_Group is null or not.
        if(Mana_Bar_Group != null)
        {
            //Destroy all mana bar block in the Mana_bar_Group, except the Block_1 object, which acts as a template.
            foreach (Transform child in Mana_Bar_Group)
            {
                if (child == Mana_Bar)
                {
                    continue;
                }
                Destroy(child.gameObject);
            }

            float x = -10.89f; //Initial x posiiton
            float y = -0.75f; //Initial y position
            float cellSize = 18.5f; //size of the Mana bar block..

            //Loops through the current mana value of the player and displays the correct amount of bars,
            for (int i = 0; i < this.manaList.GetManaList()[0]; i++)
            {
                //Instantiate a new mana block under the mana bar group and set it to be active (not hidden)
                RectTransform itemSlotRectTransform = Instantiate(Mana_Bar, Mana_Bar_Group).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                //Set the position of the mana bar block in the UI.
                itemSlotRectTransform.anchoredPosition = new Vector2(x * cellSize, y * cellSize);

                //Finds the icon image under the mana bar block.
                Image image = itemSlotRectTransform.GetComponent<Image>();
                //Sets the image to the sprite of the mana bar block in the mana bar group.
                image.sprite = fullBar;

                //Increment the x position.
                x++;
            }
        }
    }

    public PlayerManaList GetList()
    {
        return manaList;
    }
}
