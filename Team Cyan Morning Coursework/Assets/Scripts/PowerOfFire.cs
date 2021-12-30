using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PowerOfFire : Collectable
{
    private GameObject fullUI; //FUll UI GameObject.

    protected override void OnCollect()
    {
        //If the PowerOfFire Object has not been collected.
        if (!collected)
        {
            //Set the PowerOfFire Object as collected.
            base.OnCollect();
            //Allows the player to use the portal.
            GameObject.Find("Portal").GetComponent<Portal>().bossDead = true;
            //Finds the door seal object.
            GameObject doorSeal = GameObject.Find("Door Seal");
            //Disable the door seal effect if it is available.
            if (doorSeal != null)
            {
                doorSeal.GetComponent<Light2D>().enabled = false;
            }

            //Finds Full UI GameObject.
            fullUI = GameObject.Find("Full UI");
            //Enableds the mana bar UI.
            if (fullUI != null)
            {
                GameObject manaUI = fullUI.GetComponent<FindUIElement>().GetManaBarUI();
                if (manaUI != null)
                {
                    manaUI.SetActive(true);
                    manaUI.GetComponent<Mana_Bar_UI>().RefreshManaBarUI();
                }
                GameObject playerSpell = fullUI.GetComponent<FindUIElement>().GetSpellCircle();
                //Enables the spellcircle of the player so they can use fire magic.
                if (playerSpell != null)
                {
                    playerSpell.SetActive(true);
                }
            }
            //Destroy the current game object.
            Destroy(gameObject);
            Debug.Log("Collected Power of Fire!");
        }
    }
}
