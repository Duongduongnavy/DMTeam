using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D rg;
    public float vertical;
    public float horizontal;
    Vector2 direction;
    public float speedMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Debug.Log("1234 ua Zalo?!");

    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        direction = new Vector2(horizontal, vertical);
        direction.Normalize();

        if (direction.magnitude > 0)
        {
            animator.SetBool("IsRuning", true);
            animator.SetFloat("x", horizontal); 
            animator.SetFloat("y", vertical);
        }
        else
        {
            animator.SetBool("IsRuning", false);
        }
    }
    private void FixedUpdate()
    {
        rg.linearVelocity = direction * speedMove;
    }

}
//Hello Anh EM
