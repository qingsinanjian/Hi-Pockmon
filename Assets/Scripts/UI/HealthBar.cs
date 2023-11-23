using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject bloodBar;

    public void SetBloodUI(float percent)
    {
        float result = Mathf.Max(0, percent);
        bloodBar.transform.localScale = new Vector3(result, 1);
    }

    public IEnumerator RefreshUI(float percent)
    {
        float curHp = bloodBar.transform.localScale.x;
        //float changeAmount = curHp - percent;
        while((curHp - percent) > 0.01f)
        {
            yield return null;
            curHp -= Time.deltaTime;
            bloodBar.transform.localScale = new Vector3(curHp, 1);
        }

        bloodBar.transform.localScale = new Vector3(percent, 1);
    }
}
