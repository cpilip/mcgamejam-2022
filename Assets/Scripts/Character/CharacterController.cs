using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterController : MonoBehaviour
{

	[SerializeField] private float m_jumpForce = 300f;                          
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .2f; 
	              
	[SerializeField] private LayerMask m_groundLayer;                      
	[SerializeField] private Transform m_groundCheckPos;

	private Rigidbody2D m_rb2D;
	private bool m_grounded;            
	private bool m_facingRight = true; 
	private bool m_airControl = true;
	private Vector3 m_velocity = Vector3.zero;

	private void Awake()
	{
		m_rb2D = GetComponent<Rigidbody2D>();

	}

	private void FixedUpdate()
	{
		m_grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheckPos.position, .2f, m_groundLayer);
		if (colliders.Length > 0)
        {
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
				{
					m_grounded = true;
					break;
				}
			}
		}
		
	}


	public void Move(float curr, bool jump)
	{
		if (m_grounded || m_airControl)
		{
			Vector3 targetVelocity = new Vector2(curr * 10f, m_rb2D.velocity.y);
			m_rb2D.velocity = Vector3.SmoothDamp(m_rb2D.velocity, targetVelocity, ref m_velocity, m_MovementSmoothing);

			if (curr > 0 && !m_facingRight)
			{
				m_facingRight = !m_facingRight;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
			}
			else if (curr < 0 && m_facingRight)
			{
				m_facingRight = !m_facingRight;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
			}
		}

		if (m_grounded && jump)
		{
			m_grounded = false;
			m_rb2D.AddForce(new Vector2(0f, m_jumpForce));
		}
	}

}
