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
            butonAnimator.SetFloat("SpeedParameter", 0);
        }
    }
    public void OpenAnim()
    {
        butonAnimator.SetBool("Open", true);
    }

    public void CloseAnim()
    {
        butonAnimator.SetBool("Open", false);
    }



}

