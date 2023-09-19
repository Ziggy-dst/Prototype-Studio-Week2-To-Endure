using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject dayCounter;
    public GameObject Clock;
    public GameObject startButton;

    public static Action OnGameStarts;
    public static Action OnGameEnds;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        OnGameEnds += HideAll;
    }

    private void OnDestroy()
    {
        OnGameEnds -= HideAll;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void HideAll()
    {
        dayCounter.SetActive(false);
        Clock.SetActive(false);
    }

    public void RestartGame()
    {
        OnGameStarts();
        dayCounter.SetActive(true);
        Clock.SetActive(true);
        startButton.SetActive(false);

    }
}
