using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameEndInterface : MonoBehaviour
{
    public GameObject endTextBox;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnGameEnds += EndGame;
        GameManager.OnGameStarts += StartGameInterface;
    }

    private void OnDestroy()
    {
        GameManager.OnGameEnds -= EndGame;
        GameManager.OnGameStarts -= StartGameInterface;
    }

    private void EndGame()
    {
        transform.DOScale(Vector3.one * 25, 1.5f)
            .OnComplete(() => {endTextBox.SetActive(true);});
    }

    private void StartGameInterface()
    {
        transform.DOScale(Vector3.zero, 1.5f);
    }
}
