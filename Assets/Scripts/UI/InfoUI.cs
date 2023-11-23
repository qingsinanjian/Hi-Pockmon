using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    public Text nameUI;
    public Text levelUI;
    public HealthBar healthBar;

    public void InitUI(Pockmon pockmon)
    {
        nameUI.text = pockmon.pockmonBaseData.PockmonName;
        levelUI.text = "lv:" + pockmon.level;
        healthBar.SetBloodUI(pockmon.currentHealth / pockmon.pockmonBaseData.MaxHp);
    }

    public IEnumerator UpdateBlood(float percent)
    {
        yield return StartCoroutine(healthBar.RefreshUI(percent));
    }
}
