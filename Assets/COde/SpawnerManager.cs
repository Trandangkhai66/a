using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public float startTimeBtwSpawn; // Thời gian giữa mỗi lần sinh Enemy
    private float timeBtwSpawn; // Biến đếm thời gian cho việc sinh tiếp theo

    public GameObject[] enemies; // Mảng chứa các Prefab của Enemy

    public List<Spawner> spawners; // Danh sách các Spawner

    private Player player; // Tham chiếu đến Player
    int maxEnemy = 5; // Số lượng Enemy tối đa mỗi lần sinh
    int roundCount = 0; // Đếm số lần sinh để tăng dần số lượng Enemy

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Tìm đối tượng Player trong scene
        timeBtwSpawn = startTimeBtwSpawn; // Đặt thời gian chờ ban đầu cho lần sinh đầu tiên
    }

    // Hàm lấy danh sách ngẫu nhiên các chỉ số từ 0 đến n-1
    public List<int> GetRandomIndices(int n, int k)
    {
        List<int> allIndices = new List<int>();
        for (int i = 0; i < n; i++)
        {
            allIndices.Add(i);
        }

        List<int> randomIndices = new List<int>();
        int remainingItems = n;
        for (int i = 0; i < k; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, remainingItems);
            randomIndices.Add(allIndices[randomIndex]);
            allIndices[randomIndex] = allIndices[remainingItems - 1];
            remainingItems--;
        }

        return randomIndices;
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            // Tính toán số lượng Enemy sẽ được sinh ra trong lần này
            int randEnemyCount = UnityEngine.Random.Range(2, maxEnemy);

            // Lấy các chỉ số ngẫu nhiên cho spawner
            List<int> randomIndex = GetRandomIndices(maxEnemy, randEnemyCount);

            foreach (int index in randomIndex)
            {
                // Chọn ngẫu nhiên một prefab từ mảng enemies
                int randEnemy = UnityEngine.Random.Range(0, enemies.Length);
                spawners[index].spawnEnemy(enemies[randEnemy]);  // Sinh enemy từ prefab được chọn
            }

            timeBtwSpawn = startTimeBtwSpawn; // Đặt lại thời gian chờ cho lần sinh tiếp theo

            roundCount++;
            if (roundCount > 10)
            {
                roundCount = 0;
                maxEnemy = Mathf.Max(spawners.Count, maxEnemy + 1); // Tăng số lượng Enemy tối đa
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime; // Giảm thời gian chờ
        }
    }
}
