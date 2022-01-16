using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{

    public Animator butonAnimator;

    private void Awake()
    {
        butonAnimator = GetComponent<Animator>();
        if (PlayerPrefs.GetInt("Currency") < PlayerPrefs.GetInt("Price"))
        {
            butonAnimator.SetFloat("Stop", 0);
        }
    }
    public void AnimateButton()
    {
        GameManager.Instance.canBuy = true;
        butonAnimator.SetTrigger("Open");
    }



}

