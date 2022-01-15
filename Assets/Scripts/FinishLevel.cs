using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLevel : MonoBehaviour
{
    public int requiredLevel;
    public UnityEvent triggerEnter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {

            if (Player.Instance.levelValue >= requiredLevel)
            {
                triggerEnter?.Invoke();
            }
            else
            {
                //TODO: WON.
            }
        }
    }
}
