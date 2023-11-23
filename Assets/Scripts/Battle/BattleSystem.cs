using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    Start,
    PlayerAction,
    PlayerTurn,
    EnemyTurn,
    Handle
}

public class BattleSystem : MonoBehaviour
{
    public BattleUnit playerUnit;
    public BattleUnit enemyUnit;

    public InfoUI playerUI;
    public InfoUI enemyUI;

    public BattleState battleState;
    public BattleDialog battleDialog;

    public int currentAction;
    public int currentSkill;

    private void Start()
    {
        StartBattle();
    }

    public void StartBattle()
    {
        StartCoroutine(SetBattleUnit());
    }

    private IEnumerator SetBattleUnit()
    {
        playerUnit.InitBattle(1, 3);
        enemyUnit.InitBattle(2, 1);

        playerUI.InitUI(playerUnit.pockmon);
        enemyUI.InitUI(enemyUnit.pockmon);

        battleDialog.SetSkillsEnabled(false);
        battleDialog.SetActionsEnabled(true);
        battleDialog.SetPPEnabled(false);
        //battleDialog.SetUpFightText("遭遇野生宝可梦");
        yield return battleDialog.StartCoroutine(battleDialog.WaitShow("遭遇野生宝可梦"));
        yield return new WaitForSeconds(1f);
        PlayAction();
    }

    private void PlayAction()
    {
        battleState = BattleState.PlayerAction;
        battleDialog.SetSkillsEnabled(false);
        battleDialog.SetActionsEnabled(true);
        battleDialog.SetPPEnabled(false);
        battleDialog.SetUpFightText("逃跑或战斗");
    }

    private void Update()
    {
        if(battleState == BattleState.PlayerAction)
        {
            ChooseAction();
            HandleAction();
        }
        if(battleState == BattleState.PlayerTurn)
        {
            ChooseSkill();
            HandleSkill();
        }
    }

    public void ChooseAction()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentAction -= 1;
            if(currentAction <= 0)
            {
                currentAction = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentAction += 1;
            if(currentAction >= 1)
            {
                currentAction = 1;
            }
        }
    }

    public void HandleAction()
    {
        //显示
        for (int i = 0; i < battleDialog.actionText.Count; i++)
        {
            if(currentAction == i)
            {
                battleDialog.actionText[i].color = Color.red;
            }
            else
            {
                battleDialog.actionText[i].color = Color.black;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentAction == 0)
            {
                PlayerTurn();
            }
            else
            {
                FightOver();
                Debug.Log("逃跑");
            }
        }
    }

    private void FightOver()
    {

    }

    private void PlayerTurn()
    {
        battleState = BattleState.PlayerTurn;
        battleDialog.SetSkillsEnabled(true);
        battleDialog.SetActionsEnabled(false);
        battleDialog.SetPPEnabled(false);
        battleDialog.FightText.enabled = false;
        battleDialog.FightText.text = string.Empty;
        ShowSkillText(playerUnit.pockmon);
        ChooseSkill();
    }

    private void ShowSkillText(Pockmon pockmon)
    {
        List<Skill> skilList = pockmon.skillList;
        for (int i = 0; i < battleDialog.skillText.Count; i++)
        {
            if(i < skilList.Count)
            {
                battleDialog.skillText[i].text = skilList[i].SkillName;
            }
            else
            {
                battleDialog.skillText[i].text = "____";
            }
        }
    }

    private void ChooseSkill()
    {
        if(battleState != BattleState.PlayerTurn)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(currentSkill < playerUnit.pockmon.skillList.Count - 1)
            {
                currentSkill++;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(currentSkill > 0)
            {
                currentSkill--;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(currentSkill > 1)
            {
                currentSkill -= 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(currentSkill<playerUnit.pockmon.skillList.Count - 2)
            {
                currentSkill += 2;
            }
        }
        HandleSkill();
    }

    public void HandleSkill()
    {
        if(battleState != BattleState.PlayerTurn)
        {
            return;
        }
        //显示
        for (int i = 0; i < battleDialog.skillText.Count; i++)
        {
            if (currentSkill == i)
            {
                battleDialog.skillText[i].color = Color.red;
            }
            else
            {
                battleDialog.skillText[i].color = Color.black;
            }
        }

        //显示PP
        int currentPP = playerUnit.pockmon.skillList[currentSkill].currentPP;
        int maxPP = playerUnit.pockmon.skillList[currentSkill].PP;
        battleDialog.ppText.text = "PP:" + currentPP + "/" + maxPP;
        battleDialog.SetPPEnabled(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            battleDialog.SetSkillsEnabled(false);
            battleDialog.SetPPEnabled(false);
            battleDialog.SetActionsEnabled(false);

            StartCoroutine(SkillAffect());
        }
    }

    public IEnumerator SkillAffect()
    {
        battleState = BattleState.Handle;

        playerUnit.pockmon.skillList[currentSkill].currentPP--;
        //TODO 播放动画
        string playerName = playerUnit.pockmon.pockmonBaseData.PockmonName;
        string skillName = playerUnit.pockmon.skillList[currentSkill].SkillName;

        battleDialog.FightText.enabled = true;
        string str1 = playerName + "使用" + skillName;
        yield return StartCoroutine(battleDialog.WaitShow(str1));

        float defendHealth = playerUnit.pockmon.Attack(playerUnit.pockmon, currentSkill, enemyUnit.pockmon);
        float percent1 = defendHealth * 1.0f / enemyUnit.pockmon.pockmonBaseData.MaxHp * 1.0f;
        yield return StartCoroutine(enemyUI.UpdateBlood(percent1));

        //float percent2 = playerUnit.pockmon.currentHealth * 1.0f / playerUnit.pockmon.pockmonBaseData.MaxHp * 1.0f;
        //yield return StartCoroutine(playerUI.UpdateBlood(percent2));

        string name1 = enemyUnit.pockmon.pockmonBaseData.PockmonName;
        string str2 = name1 + "受伤，当前血量为：" + enemyUnit.pockmon.currentHealth;
        yield return StartCoroutine(battleDialog.WaitShow($"{str2}"));
        yield return new WaitForSeconds(1f);

        if(defendHealth <= 0)
        {
            string str3 = enemyUnit.pockmon.pockmonBaseData.PockmonName + "昏迷";
            yield return StartCoroutine($"{str3}");
            yield return new WaitForSeconds(1f);
            FightOver();
        }
        else
        {
            yield return new WaitForSeconds(1);
            EnemyTurn();
        }
    }

    public void EnemyTurn()
    {

    }
}
