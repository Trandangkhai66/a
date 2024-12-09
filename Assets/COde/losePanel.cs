using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class losePanel : MonoBehaviour
{
    public TextMeshProUGUI score;

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        
        // Kiểm tra sự tồn tại của đối tượng Killed trước khi lấy điểm
        Killed killedScript = FindObjectOfType<Killed>();
        if (killedScript != null)
        {
            int scoreI = killedScript.currentKilled * 10;
            score.text = "You get: " + scoreI.ToString() + " Score";
        }
        else
        {
            score.text = "You get: 0 Score";
        }

        // Tạm dừng thời gian khi game over
        Time.timeScale = 0;
    }

    public void Hide()
    {
        // Tiếp tục thời gian khi ẩn màn hình thua
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Nhấn phím R để tải lại cảnh
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
