using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Random = System.Random;

public class DayCounter : MonoBehaviour
{
    public static Action OnDaysReset;

    public AudioSource ticking;

    private int year = 0;
    public int maxYear = 80;
    private int currentTotalDays = 0;
    public int maxTotalDays = 365;

    public TMP_Text yearBox;
    public TMP_Text speedBox;

    public GameObject dayCircle;
    public Vector3 initialPosition;
    public float posInterval = 0.5f;
    public int row = 12;
    public int column = 30;

    // 1, 108000, 864000, 1728000, 13824000
    public float[] speeds;
    public float[] speedShow;
    private int currentIndex = 0;
    private float speed;

    private void Awake()
    {
        GameManager.OnGameStarts += InitializeCounter;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStarts -= InitializeCounter;
    }

    void Start()
    {
        InitializeCounter();
    }

    private void InitializeCounter()
    {
        maxYear = UnityEngine.Random.Range(0, 80);
        currentTotalDays = 0;
        currentIndex = 0;
        year = 0;
        yearBox.text = year.ToString();
        speedBox.text = $"x{speedShow[0]}";
        speed = speeds[0];
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ChangeSpeedFactor(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeSpeedFactor(-1);
    }

    IEnumerator Timer()
    {
        while( true )
        {
            InitializeDay();
            yield return new WaitForSeconds(86400 * speed);
        }
    }

    private void ChangeSpeedFactor(int dir)
    {
        // CancelInvoke();
        if (currentIndex == speeds.Length - 1 & dir == 1) return;
        if (currentIndex == 0 & dir == -1) return;
        StopCoroutine(Timer());
        currentIndex += dir;
        speed = speeds[currentIndex];
        speedBox.text = $"x{speedShow[currentIndex]}";
        StartCoroutine(Timer());
        Debug.Log ("current speed is "+ speed);
        // Time.timeScale = speed;
    }

    private void ResetDays()
    {
        year++;
        yearBox.text = year.ToString();
        if (year == maxYear)
        {
            StopCoroutine(Timer());
            print("game ends");
            GameManager.OnGameEnds();
            OnDaysReset();
            return;
        }

        currentTotalDays = 0;

        // remove all circles
        OnDaysReset();
    }

    private void InitializeDay()
    {
        int currentRow = currentTotalDays / column;
        int currentCol = currentTotalDays % column;
        Vector3 currentPosition = new Vector3(initialPosition.x + posInterval * currentCol, initialPosition.y - posInterval * currentRow, 0);
        Instantiate(dayCircle, currentPosition, Quaternion.identity);
        currentTotalDays++;

        ticking.Play();

        if (currentTotalDays == maxTotalDays) ResetDays();
    }
}
