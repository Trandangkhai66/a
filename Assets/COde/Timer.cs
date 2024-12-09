using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;
    int gameMode = 0;
    public int timer;

    private void Start()
    {
        gameMode = PlayerPrefs.GetInt("gameMode");
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
{
    int showTimer = 0;
    int maxTimer = 300; // Ví dụ, 30 phút
    int second, minute;
    while (true)
    {
        timer++;
        showTimer = maxTimer - timer;
        if (timer >= maxTimer)
        {
            // Dừng trò chơi và hiển thị điểm số
            FindObjectOfType<losePanel>().Show();
            break;
        }

        second = showTimer % 60;
        minute = (showTimer / 60) % 60;
        textTimer.text = minute.ToString() + ":" + second.ToString();
        yield return new WaitForSeconds(1f);
    }
}
}
