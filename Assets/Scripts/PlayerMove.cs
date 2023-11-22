using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 target;
    public float speed;
    public bool isMoving;
    public LayerMask canMoveMask;
    public LayerMask fightMask;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckFight();
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (!isMoving)
        {
            if (h != 0 || v != 0)
            {
                if (h != 0) { v = 0; }               
                target = new Vector2(h, v);
                target += new Vector2(transform.position.x, transform.position.y);
                if(CanMove(target))
                {
                    StartCoroutine(Move(target));
                }
            }
        }
        animator.SetFloat("X", h);
        animator.SetFloat("Y", v);
    }

    IEnumerator Move(Vector3 target)
    {
        isMoving = true;
        while ((target - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            yield return null;
        }
        transform.position = target;
        isMoving = false;
    }

    private bool CanMove(Vector3 target)
    {
        Collider2D collider = Physics2D.OverlapCircle(target, 0.2f, canMoveMask);
        if(collider != null)
        {
            return false;
        }
        return true;
    }

    private void CheckFight()
    {
        Collider2D collider = Physics2D.OverlapCircle(target, 0.2f, fightMask);
        if (collider != null)
        {
            int chance = Random.Range(1, 11);
            if(chance == 1)
            {
                Debug.Log("发生战斗");
            }
            else
            {
                Debug.Log("什么都没发生");
            }
        }
    }
}
