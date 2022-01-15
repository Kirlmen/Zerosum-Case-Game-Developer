using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public bool isStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TapToPlay()
    {
        isStarted = true;
        Player.Instance.AnimPlay(Player.PlayerStatus.Run);
        GameUI();
    }

    public void GameWon()
    {
        Player.Instance.AnimPlay(Player.PlayerStatus.Dance);
        Player.Instance.stop = true;
        //todo: endlevelui;
        //todo: level completed text.
        //TODO: Gathered currency added to playerprefs.
        //TODO: Next Scene button.

    }

    public void CurrencyManager()
    {
        //todo: currency
    }

    public void GameUI()
    {

    }
}
