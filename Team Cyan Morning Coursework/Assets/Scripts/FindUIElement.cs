using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindUIElement : MonoBehaviour
{
    public GameObject manaUI;
    public GameObject spellCircle;

    public GameObject GetManaBarUI()
    {
        return manaUI;
    }

    public GameObject GetSpellCircle()
    {
        return spellCircle;
    }
}
