using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BossDeath : MonoBehaviour
{
    public GameObject doorSeal; //Door Seal for the exit of the level.
    public GameObject playerSpell1; //SpellCircle
    public GameObject manaUI; //Mana bar UI.

    private void OnDestroy()
    {
        GameObject.Find("Portal").GetComponent<Portal>().bossDead = true;
        //Disable the door seal effect if it is available.
        if (doorSeal != null)
        {
            doorSeal.GetComponent<Light2D>().enabled = false;
        }
        //Enables the spellcircle of the player so they can use fire magic.
        if (playerSpell1 != null) {
            playerSpell1.SetActive(true);
        }
        //Enableds the mana bar UI.
        if (manaUI != null)
        {
            manaUI.SetActive(true);
            manaUI.GetComponent<Mana_Bar_UI>().RefreshManaBarUI();
        }
    }
}
