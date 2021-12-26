using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Player resources
    public Inventory playerInventory;       // Player's inventory.
    public int goldAmount;      // Player's gold.
    public int hitpoint;        // Player's current hp.
    public int mana;        // Player's current mana.
    [HideInInspector]
    public Loot currentWeapon;     //Player's current weapon info.

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
