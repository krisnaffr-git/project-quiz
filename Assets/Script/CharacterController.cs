using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float speed = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (move != 0)
        {
            animator.SetBool("isWalking", true);
            Vector3 characterScale = transform.localScale;
            if (move < 0)
            {
                characterScale.x = -1; // Membalikkan karakter jika bergerak ke kiri
            }
            else if (move > 0)
            {
                characterScale.x = 1; // Mengatur karakter menghadap ke kanan jika bergerak ke kanan
            }
            transform.localScale = characterScale;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
