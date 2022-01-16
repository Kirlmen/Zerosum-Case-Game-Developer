using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiplyTrigger : MonoBehaviour
{
    [SerializeField] int multiplyValue;

    public UnityEvent onMultiplyEnter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            onMultiplyEnter?.Invoke();
            int currentCurrency = GameManager.Instance.GetCurrentCurrency();
            currentCurrency *= multiplyValue;
            int totalCurrency = PlayerPrefs.GetInt("Currency");
            PlayerPrefs.SetInt("Currency", totalCurrency + currentCurrency);
        }
    }

}
