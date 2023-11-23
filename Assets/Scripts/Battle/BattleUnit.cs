using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    public Pockmon pockmon;
    public bool isPlayer;
    public int lv;
    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void InitBattle(int id, int level = 1)
    {
        if (DataManager.pockmonDic.ContainsKey(id))
        {
            var data = DataManager.Instance.GetPockmonBaseData(id);
            lv = level;
            pockmon = new Pockmon(data, lv);           
            pockmon.currentHealth = pockmon.pockmonBaseData.MaxHp;

            SetSprit();
        }
        else
        {
            Debug.Log("�����ڸ�����");
        }
    }

    private void SetSprit()
    {

    }
}
