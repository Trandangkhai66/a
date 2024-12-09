using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage = 6;
    public int maxDamage = 16;
     public float damage;

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Enemy"))
    {
        int damage = Random.Range(minDamage, maxDamage);
        // Gây sát thương cho Enemy
        collision.GetComponent<Health>().TakeDam(damage);
        
        // Thực hiện hiệu ứng sát thương cho Enemy
        collision.GetComponent<Controller>().TakeDamEffect(damage);

        // Kiểm tra nếu Enemy chết, cập nhật bảng điểm và xóa Enemy
        if (collision.GetComponent<Health>().isDead)
        {
            // Giả sử bạn có một phương thức trong `Killed` để cập nhật bảng điểm
            FindObjectOfType<Killed>().UpdateKilled();
            Destroy(collision.gameObject);
        }

        // Xóa Bullet sau khi gây sát thương
        Destroy(gameObject);
    }
}

}
