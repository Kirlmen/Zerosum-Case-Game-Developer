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
    }
}
