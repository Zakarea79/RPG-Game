using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float Speed = 10f;
	[SerializeField] private Animator animPlayer;
	public float LenSword = .5f;
	private float jumpspeed = -2;
	private Vector3 JumpBase;
	private CharacterController characterController;
	private bool JumpControl = false;
	internal bool AttakAction = false;
	//--------------------------------
	private RaycastHit hit;
#if UNITY_EDITOR
	private RaycastHit hitDebug;
#endif
	protected void Start()
	{
		characterController = gameObject.GetComponent<CharacterController>();
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
		{
			JumpBase = hit.point;
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
		}
	}
	float x;
	float y;
	private readonly Vector3 Debug_ray_x = new Vector3(.5f, 0, 0);
	private readonly Vector3 Debug_ray_y = new Vector3(0, 0, .5f);
	void Update()
	{
        
		JumpControl = ChackPlayAnim("jump normal") == true || ChackPlayAnim("jump acrobat") == true ? true : false;
		if (ChackPlayAnim("attak3") == false && ChackPlayAnim("attak4") == false)
		{
			x = ZInput.GetAxis("Horizontal") == 0 ? Input.GetAxis("Horizontal") : ZInput.GetAxis("Horizontal");
			y = ZInput.GetAxis("Vertical") == 0 ? Input.GetAxis("Vertical") : ZInput.GetAxis("Vertical");
		}
		else
		{
			x = 0;
			y = 0;
		}
		//-----------------------------------------------------
		if (System.Math.Abs(x) > .3)
			animPlayer.SetFloat("Walk", System.Math.Abs(x));
		else if (System.Math.Abs(y) > .3)
			animPlayer.SetFloat("Walk", System.Math.Abs(y));
		else
			animPlayer.SetFloat("Walk", 0);
		//-----------------------------------------------------
		if (ZInput.GetKeyDown("jump") && JumpControl == false)
		{
			var best_base_jump = new Vector3[5];
			animPlayer.SetTrigger("normalJamp");
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
			{
				best_base_jump[0] = hit.point;
			}
			// ------------------------------------------------XXX----------------------------------------------------------------------

			if (Physics.Raycast(transform.position + Debug_ray_x, transform.TransformDirection(Vector3.down), out hit))
			{
				best_base_jump[1] = hit.point;
			}
			if (Physics.Raycast(transform.position + -Debug_ray_x, transform.TransformDirection(Vector3.down), out hit))
			{
				best_base_jump[2] = hit.point;
			}
			//------------------------------------------------YYY----------------------------------------------------------------------
			if (Physics.Raycast(transform.position + Debug_ray_y, transform.TransformDirection(Vector3.down), out hit))
			{
				best_base_jump[3] = hit.point;
			}
			if (Physics.Raycast(transform.position + -Debug_ray_y, transform.TransformDirection(Vector3.down), out hit))
			{
				best_base_jump[4] = hit.point;
				
			}

			JumpBase = best_base_jump[0];

			foreach (var item in best_base_jump)
			{
				if (Vector3.Distance(transform.position, item) < Vector3.Distance(transform.position, JumpBase))
				{
					JumpBase = item;
				}
			}
		}
#if UNITY_EDITOR
		//------------------------------------------Debug-----------------------------------------------------------------------
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitDebug))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitDebug.distance, Color.green);
		}
		//------------------------------------------------XXX----------------------------------------------------------------------
		if (Physics.Raycast(transform.position + Debug_ray_x, transform.TransformDirection(Vector3.down), out hitDebug))
		{
			Debug.DrawRay(transform.position + Debug_ray_x, transform.TransformDirection(Vector3.down) * hitDebug.distance, Color.blue);
		}
		if (Physics.Raycast(transform.position + -Debug_ray_x, transform.TransformDirection(Vector3.down), out hitDebug))
		{
			Debug.DrawRay(transform.position + -Debug_ray_x, transform.TransformDirection(Vector3.down) * hitDebug.distance, Color.red);
		}
		//------------------------------------------------YYY----------------------------------------------------------------------
		if (Physics.Raycast(transform.position + Debug_ray_y, transform.TransformDirection(Vector3.down), out hitDebug))
		{
			Debug.DrawRay(transform.position + Debug_ray_y, transform.TransformDirection(Vector3.down) * hitDebug.distance, Color.yellow);
		}
		if (Physics.Raycast(transform.position + -Debug_ray_y, transform.TransformDirection(Vector3.down), out hitDebug))
		{
			Debug.DrawRay(transform.position + -Debug_ray_y, transform.TransformDirection(Vector3.down) * hitDebug.distance, Color.grey);
		}
		//------------------------------------------Debug-----------------------------------------------------------------------
#endif
		if (ZInput.GetKeyDown("sowrd_attak") && StatuseAnimitonAttack())
		{
			animPlayer.SetInteger("Attak", Random.Range(1, 3));
		}
		else if (ZInput.GetKeyDown("sowrd_attak_cercl") && StatuseAnimitonAttack())
		{
			animPlayer.SetInteger("Attak", 3);
		}
		else if (ZInput.GetKeyDown("magic_attak") && StatuseAnimitonAttack())
		{
			animPlayer.SetInteger("Attak", 4);
		}
		animPlayer.applyRootMotion = ChackPlayAnim("attak3");
		AttakAction = StatuseAnimitonAttack() == false && ChackPlayAnim("attak4") == false ? true : false;
		if (Vector3.Distance(new Vector3(0, JumpBase.y, 0), new Vector3(0, transform.position.y, 0)) > 3)
		{
			jumpspeed = -2;
		}
		float xmove = 0;
		float ymove = 0;
		if (System.Math.Abs(x) > .3 || System.Math.Abs(y) > .3)
		{
			xmove = x;
			ymove = y;

			var Rotatev = new Vector3(-y, 0, x);
			Quaternion quaternion = Quaternion.LookRotation(Rotatev);
			transform.rotation = quaternion;
		}
		characterController.Move(new Vector3(-ymove, jumpspeed, xmove) * Speed * Time.deltaTime);
	}
	public void FierFlay(int input) { transform.GetChild(5).GetComponent<SphereCollider>().radius = input; }
	public void SetJumpControl(int statuse) { JumpControl = System.Convert.ToBoolean(statuse); if (JumpControl == true) jumpspeed = 2; }
	public void resevarable(string statename) { animPlayer.SetInteger(statename, 0); }
	private bool ChackPlayAnim(string name) { return animPlayer.GetCurrentAnimatorStateInfo(0).IsName(name); }
	private bool StatuseAnimitonAttack()
	{
		for (int i = 1; i < 5; i++)
		{
			if (ChackPlayAnim("attak" + i) == true)
			{
				return false;
			}
		}
		return true;
	}
	private bool StatuseAnimitonDamage()
	{
		for (int i = 1; i < 4; i++)
		{
			if (ChackPlayAnim("Damage" + i) == true)
			{
				return false;
			}
		}
		return true;
	}
}
