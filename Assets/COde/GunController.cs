using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;  // Đối tượng đạn
    public Transform firePoint;      // Điểm bắn của súng
    public float timeBtwShots = 0.5f; // Thời gian giữa các lần bắn
    private float timeBtwShotsCounter; // Bộ đếm thời gian giữa các lần bắn
    public float bulletSpeed = 20f;   // Vận tốc của viên đạn
    public float minDamage = 10f;     // Sát thương tối thiểu
    public float maxDamage = 20f;     // Sát thương tối đa
    public GameObject muzzleEffect;   // Hiệu ứng nòng súng (lửa, ánh sáng)

    private Player player;            // Tham chiếu tới Player

    void Start()
    {
        // Lấy tham chiếu đến đối tượng Player
        player = FindObjectOfType<Player>();
    }

   void Update()
{
    // Lấy vị trí chuột trên màn hình
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    // Tính toán góc xoay từ súng đến vị trí chuột
    Vector3 direction = mousePosition - transform.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Xoay súng theo góc tính được
    transform.rotation = Quaternion.Euler(0, 0, angle);

    // Đảm bảo súng không bị lật ngược
    Vector3 gunScale = transform.localScale;
    if (angle > 90 || angle < -90) 
    {
        gunScale.y = -Mathf.Abs(gunScale.y); // Đảm bảo scale.y luôn âm
    }
    else
    {
        gunScale.y = Mathf.Abs(gunScale.y); // Đảm bảo scale.y luôn dương
    }
    transform.localScale = gunScale;

    // Tính toán thời gian giữa các lần bắn
    if (timeBtwShotsCounter <= 0)
    {
        if (Input.GetMouseButtonDown(0))  // Bắn khi nhấn chuột trái
        {
            FireBullet();
            timeBtwShotsCounter = timeBtwShots;
        }
    }
    else
    {
        timeBtwShotsCounter -= Time.deltaTime; // Đếm ngược thời gian
    }
}


    void FireBullet()
{
    // Tạo viên đạn và xác định hướng bắn
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

    // Đẩy viên đạn về phía con trỏ chuột
    Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;
    rb.velocity = direction * bulletSpeed;

    // Áp dụng sát thương ngẫu nhiên cho viên đạn
    Bullet bulletScript = bullet.GetComponent<Bullet>();
    if (bulletScript != null)
    {
        bulletScript.damage = Random.Range(minDamage, maxDamage); // Gán giá trị damage cho viên đạn
    }

    // Hiệu ứng nòng súng (ví dụ: lửa hoặc ánh sáng)
    if (muzzleEffect != null)
    {
        Instantiate(muzzleEffect, firePoint.position, firePoint.rotation);
    }
}

}
