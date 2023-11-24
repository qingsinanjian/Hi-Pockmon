using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Normal,
    Fight,
    Chat
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;

    public BattleSystem battleSystem;

    public PlayerMove playerMove;

    public Camera UICamera;

    public AudioClip bgClip;
    public AudioClip fightClip;

    private void Awake()
    {
        gameState = GameState.Normal;
        battleSystem.gameObject.SetActive(false);
        UICamera.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(gameState == GameState.Normal)
        {
            AudioManager.instance.PlayMusic(bgClip);
            playerMove.PlayerMoveUpdate();
        }
        else if(gameState == GameState.Fight)
        {
            battleSystem.BattleUpdate();
        }
        else
        {

        }
    }

    public void StartFight()
    {
        gameState = GameState.Fight;
        battleSystem.gameObject.SetActive(true);
        UICamera.gameObject .SetActive(true);

        AudioManager.instance.musicPlayer.Stop();
        AudioManager.instance.PlayMusic(fightClip);

        battleSystem.StartBattle();
    }
}
