using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Function to load the village scene.
    public void StartGame() {
        //Plays the button sound effect.
        FindObjectOfType<SoundManager>().Play("MenuButtonPress");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Checks to make sure that the game manager is not null.
        if(GameObject.Find("GameManager") != null)
        {
            //Resets the player's inventory, health,gold and mana up on pressing the start game button from the main menu.
            GameManager.Instance.playerInventory = new Inventory();
            GameManager.Instance.hitpoint = 4;
            GameManager.Instance.goldAmount = 100;
            GameManager.Instance.mana = 10;
        }
    }

    //Exits the game , back to Windows/Mac.
    public void ExitGame() {
        //Plays the button sound effect.
        FindObjectOfType<SoundManager>().Play("MenuButtonPress");

        Application.Quit();
    }
}
