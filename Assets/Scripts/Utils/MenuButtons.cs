using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject menu;

    public void OpenMenu()
    {
        // Enable the menu object.
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        // Disable the menu object.
        menu.SetActive(false);
    }
}