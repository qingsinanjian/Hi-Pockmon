using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pockmon : MonoBehaviour
{
    public PockmonBaseData pockmonBaseData;
    public int level;

    public int currentHealth;

    public Pockmon(PockmonBaseData pockmonBaseData, int level = 1)
    {
        this.pockmonBaseData = pockmonBaseData;
        this.level = level;
    }
}
