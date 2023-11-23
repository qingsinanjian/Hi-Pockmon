using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pockmon : MonoBehaviour
{
    public PockmonBaseData pockmonBaseData;
    public int level;

    public int currentHealth;

    public List<Skill> skillList;

    public Pockmon(PockmonBaseData pockmonBaseData, int Level = 1)
    {
        this.pockmonBaseData = pockmonBaseData;
        this.level = Level;

        skillList = new List<Skill>();

        foreach (var skill in DataManager.skillDic)
        {
            if(skill.Value.LearnSkill <= Level)
            {
                skillList.Add(skill.Value);
            }
            if(skillList.Count >= 4)
            {
                break;
            }
        }
    }

    public int Attack(Pockmon attack, int index, Pockmon defend, float percent = 1)
    {
        float parm1 = (attack.skillList[index].Power * attack.pockmonBaseData.Attack / defend.pockmonBaseData.Defense);
        float parm2 = attack.level * 0.4f / 2;
        float result = parm1 * parm2 / 5 * percent;
        int realDamage = System.Convert.ToInt32(result);
        defend.currentHealth -= realDamage;
        if(defend.currentHealth < 0)
        {
            defend.currentHealth = 0;
        }
        else
        {

        }
        return defend.currentHealth;
    }

    public Skill GetRandomSkill()
    {
        int index = Random.Range(0, 4);
        return skillList[index];
    }
}
