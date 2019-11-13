using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce;
    public LayerMask ziemia;
    public float l;
    public TextMeshProUGUI scoreText;

	private int score = 0;
    private Rigidbody2D rb;
    private BoxCollider2D bx;
    

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bx = GetComponent<BoxCollider2D>();
    }

	// FixedUpdate is called every fixed frame-rate frame, use it when using Rigidbody
	private void FixedUpdate()
	{
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x*speed, rb.velocity.y);
	}

	// Update is called once per frame
	void Update()
    {
        if(IsGrounded() && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(0, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        Vector3 boxPos = transform.position+new Vector3(bx.offset.x ,bx.offset.y,0);

        Vector2 lewaKreska = boxPos - new Vector3(bx.size.x / 2, 0);
        Vector2 prawaKreska = boxPos + new Vector3(bx.size.x / 2, 0);
        Vector2 kierunek = Vector2.down;

        RaycastHit2D hitLeft = Physics2D.Raycast(lewaKreska, kierunek, l, ziemia);
        RaycastHit2D hitRight = Physics2D.Raycast(prawaKreska, kierunek, l, ziemia);



        return (hitLeft.collider!=null || hitRight.collider!=null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        score++;

        scoreText.text = "Score: " + score;
    }
}
