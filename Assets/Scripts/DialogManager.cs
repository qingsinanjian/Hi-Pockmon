using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialog;
    public Text dialogText;

    public GameManager gameManager;

    public int currentIndex = 0;

    public Dialog _dialog;

    public static DialogManager instance;
    public AudioClip clickClip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialog.SetActive(false);
    }

    private void Update()
    {
        if(gameManager.gameState == GameState.Chat)
        {
            DialogUpdate();
        }
    }

    public IEnumerator WaitShow(string str)
    {
        dialogText.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            dialogText.text += str[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator ShowDialog()
    {
        yield return null;
        gameManager.gameState = GameState.Chat;
        dialog.SetActive(true);
        StartCoroutine(WaitShow(_dialog.strings[currentIndex]));
    }

    public void DialogUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.instance.PlayAffect(clickClip, false);
            currentIndex++;
            if (currentIndex < _dialog.strings.Count)
            {
                StartCoroutine(WaitShow(_dialog.strings[currentIndex]));
            }
            else
            {
                dialog.SetActive(false);
                gameManager.gameState = GameState.Normal;
                currentIndex = 0;
            }
        }
    }
}
