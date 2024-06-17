using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Information Storage
    public int money = 1000;
    public List<GameObject> vehicles;
    public byte toggleShopCanvasVisibility = 0;

    private int currIndex = 0;
    private int catalogSize;

    // UI objects that need to be stored.
    public RectTransform shopCanvas;
    public Text moneyText;


    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = $"Money: {money}";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shopCanvasVisible()
    {
        toggleShopCanvasVisibility = (byte) ((toggleShopCanvasVisibility + 1) % 2);

        if (toggleShopCanvasVisibility == 1) { 
            shopCanvas.anchoredPosition = new Vector2 (0f, 386.2686f);
        } else
        {
            shopCanvas.anchoredPosition = new Vector2(-1492f, 386.2686f);
        }
    }

    public void prevItem()
    {

    }

    public void nextItem()
    {

    }

}
