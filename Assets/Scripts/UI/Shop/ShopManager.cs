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
    
    // Item Display related attributes
    public Transform itemDisplay;
    private GameObject itemInDisplay;
    public Text itemNameText;
    public Text itemDescText;
    public Text priceText;
    public Text sellText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = $"Money: ${money}";
        catalogSize = vehicles.Count;
        itemInDisplay = vehicles[currIndex];
        showCaseItemInDisplay();
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
        currIndex--;
        if (currIndex < 0) { currIndex = catalogSize - 1; }
        itemInDisplay.SetActive(false);
        itemInDisplay = vehicles[currIndex];
        itemInDisplay.SetActive(true);
        showCaseItemInDisplay();
    }

    public void nextItem()
    {
        currIndex = (currIndex + 1) % catalogSize;
        itemInDisplay.SetActive(false);
        itemInDisplay = vehicles[currIndex];
        itemInDisplay.SetActive(true);
        showCaseItemInDisplay();
    }

    public void showCaseItemInDisplay() {
        ShopItem shopItem = itemInDisplay.GetComponent<ShopItem>();
        itemNameText.text = shopItem.itemName;
        itemDescText.text = shopItem.description;
        priceText.text = $"Cost: ${shopItem.price}";
        sellText.text = $"Sell: ${shopItem.sellPrice}";
    }

    public void boughtItem(ShopItem shopItem) {
        if (canBuy(shopItem)) { 
            money -= shopItem.price;
            moneyText.text = $"Money: ${money}";
        }
    }

    public bool canBuy(ShopItem shopItem) { return money >= shopItem.price; }


    public void soldItem(ShopItem shopItem) {
        money += shopItem.sellPrice;
        moneyText.text = $"Money: ${money}";
    }

    public void uiInvisible() { 
        this.gameObject.SetActive(false);
    }

    public void uiVisible()
    {
        this.gameObject.SetActive(true);
    }


    public GameObject getItemInDisplay() { return itemInDisplay; }
}
