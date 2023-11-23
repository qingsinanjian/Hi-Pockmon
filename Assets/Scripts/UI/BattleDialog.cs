using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialog : MonoBehaviour
{
    public Text FightText;
    public GameObject Skills;
    public GameObject Actions;

    public List<Text> actionText;
    public List<Text> skillText;
    public Text ppText;

    private void Awake()
    {
        FightText = GetComponentInChildren<Text>();
    }

    public void SetUpFightText(string str)
    {
        FightText.text = str;
    }

    public void SetSkillsEnabled(bool enabled)
    {
        Skills.SetActive(enabled);
    }

    public void SetActionsEnabled(bool enabled)
    {
        Actions.SetActive(enabled);
    }

    public IEnumerator WaitShow(string str)
    {
        FightText.text = "";
        for (int i = 0;i<str.Length;i++)
        {
            FightText.text += str[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetPPEnabled(bool enabled)
    {
        ppText.gameObject.SetActive(enabled);
    }
}
