using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Shop : MonoBehaviour
{
    public List<Item> product;
    public Button buyButton;
    public Button sellButton;
    public GameObject listViewContent;
    public TextMeshProUGUI displayMoney;
    public TextMeshProUGUI productDescription;
    public TextMeshProUGUI possessionsNumber;
    public ShopItemButton productButton;
    [HideInInspector] public bool isOpen = true;
    private List<ShopItemButton> productButtons = new List<ShopItemButton>();
    private Action onCancel = null;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) | Input.GetKeyUp(KeyCode.Escape))
        {
            if (onCancel == null)
            {
                Closed();
                return;
            }

            onCancel?.Invoke();
            onCancel = null;
        }
    }

    public void Buy(Item item)
    {
        if (GameController.Money < item.price)
        {
            productDescription.text = "<color=red>お金がたりません</color>";
            return;
        }

        InventorySystem.AddItem(item);
        GameController.Money -= item.price;
        displayMoney.text = GameController.Money.ToString();
    }

    public void Sell(Item item)
    {
        InventorySystem.Remove(item);
        GameController.Money += item.price / 2;
        displayMoney.text = GameController.Money.ToString();
    }

    void Start()
    {
        ProductsDisplay();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        isOpen = true;
        displayMoney.text = GameController.Money.ToString() + "G";
        buyButton.Select();
        Enter();
    }

    public void Closed()
    {
        Exit();
        isOpen = false;
        gameObject.SetActive(false);
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    //商品陳列
    public void ProductsDisplay()
    {
        foreach (var item in product)
        {
            ShopItemButton BuyButton = Instantiate(productButton);
            BuyButton.productName.text = item.itemName;
            BuyButton.monyText.text = item.price.ToString() + "G";

            BuyButton.button.onClick.AddListener(() => { Buy(item); });
            BuyButton.button.onHover = (() =>
            {
                int possessionsNumber = 0;
                ViewPossessionsNumber(possessionsNumber);
                ProductDescription(item);
            });
            productButtons.Add(BuyButton);
            BuyButton.transform.SetParent(listViewContent.transform);
        }
    }

    public void BuyWindow()
    {
        ProductsDisplay();
        productButtons[0].button.Select();
    }

    public void SellWindow()
    {
        List<Item> items = InventorySystem.GetItems();
        int buttonIndex = -1;
        foreach (var item in items)
        {
            ShopItemButton sellButton = Instantiate(productButton);
            buttonIndex++;
            sellButton.button.index = buttonIndex;
            sellButton.productName.text = item.itemName;
            sellButton.monyText.text = (item.price / 2).ToString() + "G";

            sellButton.button.onClick.AddListener(() =>
            {
                Sell(item);
                DestroyButton(sellButton.button);
            });
            sellButton.button.onHover = (() =>
            {
                int possessionsNumber = 0;
                ViewPossessionsNumber(possessionsNumber);
                ProductDescription(item);
            });
            productButtons.Add(sellButton);
            sellButton.transform.SetParent(listViewContent.transform);
        }
        productButtons[0].button.Select();
    }

    private void DestroyButton(SelectableButton button)
    {
        int index = button.index;
        Destroy(button.gameObject);
        productButtons.RemoveAt(button.index);
        if (productButtons.Count == 0)
        {
            productDescription.text = string.Empty;
            return;
        }
        for (int i = 0; i < productButtons.Count; i++)
        {
            productButtons[i].button.index = i;
        }
        int selectIndex = Mathf.Clamp(index - 1, 0, int.MaxValue);
        productButtons[selectIndex].button.Select();
    }

    public void OnClickBuy()
    {
        ClearProducts();
        BuyWindow();
        onCancel = () => { buyButton.Select(); };
    }

    public void OnClickSell()
    {
        ClearProducts();
        SellWindow();
        onCancel = () => { sellButton.Select(); };
    }

    public void ClearProducts()
    {
        foreach (var item in productButtons)
        {
            Destroy(item.gameObject);
        }
        productButtons.Clear();
    }

    //確認Window
    public void ConfirmationWindow()
    {

    }

    public void ViewPossessionsNumber(int number)
    {
        possessionsNumber.text = number.ToString();
    }

    public void ProductDescription(Item item)
    {
        productDescription.text = item.description;
    }
}
