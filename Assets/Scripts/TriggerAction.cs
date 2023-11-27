using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public abstract class TriggerAction : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExecuteTriggerActionStay();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExecuteTriggerActionEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExecuteTriggerActionExit();
        }
    }

    protected abstract void ExecuteTriggerActionStay();
    protected abstract void ExecuteTriggerActionEnter();
    protected abstract void ExecuteTriggerActionExit();
}