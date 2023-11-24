using DG.Tweening;
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
    public Vector3 originPos;
    private Color originColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        originPos = image.transform.localPosition;
        originColor = image.color;
    }

    public void InitBattle(int id, int level = 1)
    {
        if (DataManager.pockmonDic.ContainsKey(id))
        {
            var data = DataManager.Instance.GetPockmonBaseData(id);
            lv = level;
            pockmon = new Pockmon(data, lv);           
            pockmon.currentHealth = pockmon.pockmonBaseData.MaxHp;

            SetSprite();
        }
        else
        {
            Debug.Log("不存在该数据");
        }
        PlayEnterAnim();
    }

    private void SetSprite()
    {

    }

    private void PlayEnterAnim()
    {
        if (isPlayer)
        {
            image.transform.localPosition = new Vector3(-500, originPos.y);
        }
        else
        {
            image.transform.localPosition = new Vector3(500, originPos.y);
        }
        image.transform.DOLocalMoveX(originPos.x, 1f);
    }

    public void PlayAttackAnim()
    {
        Sequence sequence = DOTween.Sequence();

        if (isPlayer)
        {
            sequence.Append(image.transform.DOLocalMoveX(originPos.x + 50, 0.25f));
        }
        else
        {
            sequence.Append(image.transform.DOLocalMoveX(originPos.x - 50, 0.25f));
        }

        sequence.Append(image.transform.DOLocalMoveX(originPos.x, 0.25f));
    }

    public void PlayHurtAnim()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(image.DOColor(Color.gray, 0.1f));
        seq.Append(image.DOColor(originColor, 0.1f));
    }

    /// <summary>
    /// 播放退场动画
    /// </summary>
    public void PlayComaAnim()
    {
        if (isPlayer)
        {
            image.transform.DOLocalMoveY(originPos.y - 300, 0.5f);
        }
        else
        {
            image.transform.DOLocalMoveY(originPos.y + 300, 0.5f);
        }
    }
}
