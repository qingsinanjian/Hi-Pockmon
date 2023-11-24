using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public void Chat()
    {
        StartCoroutine(DialogManager.instance.ShowDialog());
    }
}
