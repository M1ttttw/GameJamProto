using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button smStartButton;
    public Button smLoreButton;

    public GameObject loreMenu;

    public void switchToLore() { 
        smStartButton.gameObject.SetActive(false);
        smLoreButton.gameObject.SetActive(false);
        loreMenu.SetActive(true);
    }

    public void switchToStart() {
        loreMenu.SetActive(false);
        smStartButton.gameObject.SetActive(true);
        smLoreButton.gameObject.SetActive(true);
    }

}
