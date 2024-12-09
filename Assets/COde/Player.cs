using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Các biến công khai cho cấu hình trong Inspector
    public float moveSpeed = 5f;             // Tốc độ di chuyển
    public Rigidbody2D rb;                  // Rigidbody2D
    public SpriteRenderer characterSR;      // Sprite Renderer cho nhân vật

    public float dashBoost = 2f;            // Tăng tốc khi lướt
    private float dashTime;                 // Thời gian lướt hiện tại
    public float DashTime = 0.25f;          // Thời gian lướt cấu hình
    private bool isDashing;                 // Trạng thái lướt

    public Vector3 moveInput;               // Đầu vào di chuyển
    public GameObject damPopUp;             // Popup hiển thị sát thương
    public losePanel losePanel;             // Màn hình thua
    public Health PlayerHealth; //
    // Animator để điều khiển hoạt ảnh
    [SerializeField] private Animator animator;

    private void Start()
    {
        // Gán các thành phần
        rb = GetComponent<Rigidbody2D>();

        // Kiểm tra Animator, nếu chưa gắn thì báo lỗi
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
                Debug.LogError("Animator không được gắn vào Player! Hãy kiểm tra lại.");
        }
    }

    private void Update()
    {
        // Xử lý di chuyển
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveSpeed * Time.deltaTime * moveInput;

        // Điều chỉnh tốc độ di chuyển để cập nhật Animation
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        // Kích hoạt kỹ năng lướt
        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            isDashing = true;
            animator.SetBool("luot", true); // Kích hoạt Animation lướt
            moveSpeed += dashBoost;         // Tăng tốc độ
            dashTime = DashTime;            // Đặt thời gian lướt
        }

        // Kết thúc lướt khi hết thời gian
        if (dashTime <= 0 && isDashing)
        {
            isDashing = false;
            animator.SetBool("luot", false); // Tắt Animation lướt
            moveSpeed -= dashBoost;          // Trả lại tốc độ ban đầu
        }
        else if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
        }

        // Quay mặt nhân vật
        if (moveInput.x != 0)
        {
            if (moveInput.x < 0)
                characterSR.transform.localScale = new Vector3(-1, 1, 0);
            else
                characterSR.transform.localScale = new Vector3(1, 1, 0);
        }
    }

public void TakeDamageEffect(int damage)
{
    if (damPopUp != null)
    {
        // Tạo popup hiển thị sát thương
        GameObject instance = Instantiate(damPopUp, transform.position
                + new Vector3(Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
        instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        Animator popupAnimator = instance.GetComponentInChildren<Animator>();
        popupAnimator.Play("red");
    }

    // Nếu Player chết, hiển thị màn hình thua
    if (GetComponent<Health>().isDead)
    {
        losePanel.Show(); // Hiển thị màn hình thua
    }
}
}
