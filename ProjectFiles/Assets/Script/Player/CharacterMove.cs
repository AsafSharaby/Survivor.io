using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CharacterMove : MonoBehaviour
{
	public float moveSpeed;
	private Rigidbody2D rb;
	private Animator anim;
	private Vector2 moveInput;
	[HideInInspector] public Vector2 lastMove;

	

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		lastMove = new Vector2(1,0);
	}

    void Update()
	{
		MoveInput();

		//GetPointerInput();

	}

	private void MoveInput()
	{
		moveInput.x = Input.GetAxisRaw("Horizontal");
		moveInput.y = Input.GetAxisRaw("Vertical");

		moveInput.Normalize();
		rb.velocity = moveInput * moveSpeed;

		if (!Mathf.Approximately(0, moveInput.x))
			transform.rotation = moveInput.x > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

		if (moveInput.x != 0 || moveInput.y != 0)
			anim.Play("Move");
		else
			anim.Play("Idle");

		if (moveInput.x != 0 )
			lastMove = new Vector2(moveInput.x, 0);
		else if ( moveInput.y != 0)
			lastMove = new Vector2(0, moveInput.y);

		if (moveInput.x != 0 && moveInput.y != 0)
			lastMove = new Vector2(moveInput.x, moveInput.y);
	}

	private void GetPointerInput()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
		//Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		WeaponHandler.instance.weaponHolder.transform.rotation = Quaternion.Euler(0, 0, angle);

		Vector2 scale = WeaponHandler.instance.weaponHolder.transform.localScale;
		if (mousePosition.x < 0)
			scale.y = -1;
		else
			scale.y = 1;

		WeaponHandler.instance.weaponHolder.transform.localScale = scale;
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("coin"))
		{
			GameHandler.instance.GainExperience(1);
			Destroy(collision.gameObject);
		}
	}
}
