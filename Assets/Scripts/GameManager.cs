using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;



    public TMP_Text currencyText, buyPrice, startStackText, ingameCurrencyText, levelText, collectedCurText;
    [SerializeField] Button buyButton;
    [SerializeField] GameObject openButton;
    [SerializeField] GameObject startingMenu, gameScreen, endLevelScreen;


    public UnityEvent onLevelWon;
    public bool canBuy = false;



    public int currency;
    public bool isStarted = false;

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("Currency"))
        {
            PlayerPrefs.SetInt("Currency", 0);
        }
        if (!PlayerPrefs.HasKey("Price"))
        {
            PlayerPrefs.SetInt("Price", 1);
        }
    }
    private void Awake()
    {
        Instance = this;

        currency = PlayerPrefs.GetInt("Currency");
        currencyText.text = currency.ToString();


        price = PlayerPrefs.GetInt("Price", 1);
        buyPrice.text = price.ToString();
        startStackText.text = PlayerPrefs.GetInt("LevelValue", 0).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        //openbutton deactivate
        if (!openButton.GetComponent<Button>().interactable) { openButton.SetActive(false); }
        else { openButton.SetActive(true); }

        if (canBuy && PlayerPrefs.GetInt("Currency") >= PlayerPrefs.GetInt("Price")) { buyButton.interactable = true; } //Buy button protection
        else { buyButton.interactable = false; }

    }

    public void TapToPlay()
    {
        isStarted = true;
        Player.Instance.AnimPlay(Player.PlayerStatus.Run);
        if (isStarted)
        {
            startingMenu.SetActive(false);
            gameScreen.SetActive(true);
        }
    }

    int temp;
    public void RuntimeIncrease(int amount) //calculate the runtime currency
    {
        if (isStarted)
        {
            temp += amount;
            ingameCurrencyText.text = temp.ToString();
        }
    }


    public void GameWon()
    {
        onLevelWon?.Invoke();
        Player.Instance.AnimPlay(Player.PlayerStatus.Dance);
        Player.Instance.stop = true;
        int collected;
        collected = int.Parse(ingameCurrencyText.text);
        collectedCurText.text = collected.ToString();
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + temp);//saving the collected currency + saved currency

        //TODO: Gathered currency added to playerprefs.
        //TODO: Next Scene button.

    }




    [SerializeField] GameObject screenBuyButton;
    int price;
    [SerializeField] int priceIncreaseRate = 4;
    public void LevelUpButton() //TO MAXIMIZE LEVEL > NEED TO SPEND TOTAL 20.300 CURRENCY
    {
        if (PlayerPrefs.GetInt("Currency") <= 0)
        {
            return;
        }
        //Price SetUP
        price = PlayerPrefs.GetInt("Price", 1);
        int curr = PlayerPrefs.GetInt("Currency");
        PlayerPrefs.SetInt("Currency", curr - price);
        currencyText.text = PlayerPrefs.GetInt("Currency").ToString();
        price += priceIncreaseRate;
        PlayerPrefs.SetInt("Price", price);

        //LevelCalculation
        int inc = PlayerPrefs.GetInt("LevelValue") + 1;
        PlayerPrefs.SetInt("LevelValue", inc);
        Player.Instance.levelSlider.value = PlayerPrefs.GetInt("LevelValue");
        Player.Instance.StageManager();

        //startstacktext update
        startStackText.text = PlayerPrefs.GetInt("LevelValue").ToString();

        buyPrice.text = PlayerPrefs.GetInt("Price").ToString();
        if (PlayerPrefs.GetInt("Price") > 403)  //if its reached full level
        {
            buyPrice.text = "Full!";
            screenBuyButton.SetActive(false); // buton açılır kapanırı yaptığında değiştir!!
        }


        if (PlayerPrefs.GetInt("LevelValue") > 100)
        {
            PlayerPrefs.SetInt("LevelValue", 100);
        }



    }
}
