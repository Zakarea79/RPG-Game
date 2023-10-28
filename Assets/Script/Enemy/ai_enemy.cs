using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class ai_enemy : MonoBehaviour
{
	private NavMeshAgent enemyAgent;
	private Transform Player;
	private Vector3 FirstPosition;
	private float max = 0;
	private float HPR = 0;
	[SerializeField] private Transform RandomPos;
	[SerializeField] private float LHP = 1000;
	[SerializeField] private Transform LableHP;
	[SerializeField] private Vector3 LableHPPos;

	public float HPP
	{
		set{
			LHP += value;
			if(LHP > max) LHP = max;
		}
		get{return LHP;}
	}

	public float HPM
	{
		set{
			LHP -= value;
			if(LHP < 0) LHP = 0;
		}
		get{return LHP;}
	}

    void Start()
    {
		HPR = LHP;
		max = LHP;
		FirstPosition = gameObject.transform.position;
        enemyAgent = gameObject.GetComponent<NavMeshAgent>();
		Player = GameObject.Find("Player").transform;
		randomPosition = Instantiate(RandomPos);
		randomPosition.gameObject.AddComponent<point_enemy>();
		randomPosition.gameObject.GetComponent<point_enemy>().ThisEnemy = this;
		randomPosition.position = Vector3.zero;
		LableHP = Instantiate(LableHP);
    }

	void Update()
	{
	    Behaviour();
		ShowHP();
	}

	float DistanceBetweenCharacters()
	{
	    Vector3 enemyPosition = enemyAgent.transform.position;
	    Vector3 playerPosition = Player.position;
	    return Vector3.Distance(enemyPosition, playerPosition);
	}

	private enum EnemyStatuse : byte 
	{
		home , run , back
	}

	public Vector3 RandomNavmeshLocation(float radius) 
	{
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }

	private EnemyStatuse enemyStatuse = EnemyStatuse.home;
	private Transform randomPosition;
	private float Arrea = 10f;
	public void CrateRandomPosition()
	{
		randomPosition.position = new Vector3(Random.Range(FirstPosition.x , FirstPosition.x +  Random.Range(-Arrea , Arrea)) , 
		FirstPosition.y + .1f , Random.Range(FirstPosition.z , FirstPosition.z + Random.Range(-Arrea , Arrea)));
	}
	IEnumerator DoRotationAtTargetDirection(Vector3 opponentPlayer)
	{
		Quaternion targetRotation = Quaternion.identity;
		do
		{
			Debug.Log("do rotation");
			Vector3 targetDirection = opponentPlayer - transform.position;
			targetRotation = Quaternion.LookRotation(targetDirection);
			Quaternion nextRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime);
			transform.localRotation = nextRotation;
			yield return null;

		} while (Quaternion.Angle(transform.localRotation, targetRotation) < 0.01f);
	}
	void Behaviour()
	{
		switch (enemyStatuse)
		{
			case EnemyStatuse.home:
			print("Play Gasht Zani");
			if(randomPosition.position == Vector3.zero)
			{
				CrateRandomPosition();
			}
			enemyAgent.SetDestination(randomPosition.position);
			DoRotationAtTargetDirection(randomPosition.position);
			if(Vector3.Distance(randomPosition.position , gameObject.transform.position) < 1.5f)
			{
				randomPosition.position = Vector3.zero;
			}
			if(DistanceBetweenCharacters() < 5)
			{
				enemyStatuse = EnemyStatuse.run;
				enemyAgent.speed = 3.5f;
			}
			break;
			case EnemyStatuse.run:
			print("Play Run");
			enemyAgent.SetDestination(Player.position);
			DoRotationAtTargetDirection(Player.position);
			if(DistanceBetweenCharacters() > 10)
			{
				enemyStatuse = EnemyStatuse.back;
				enemyAgent.speed = 3.5f;
			}else if(DistanceBetweenCharacters() < 3f)
			{
				enemyAgent.speed = 0;
				print("play Attak");
			}else
			{
				enemyAgent.speed = 3.5f;
				print("play Fwllo Player");
			}
			break;
			case EnemyStatuse.back:
			print("Play Go To Home");
	    	enemyAgent.SetDestination(FirstPosition);
			DoRotationAtTargetDirection(FirstPosition);
			
			if(Vector3.Distance(transform.position , FirstPosition) < 1.5)
			{
				enemyStatuse = EnemyStatuse.home;
				enemyAgent.speed = 3.5f;
			}
			break;
		}
	}

	private void ShowHP()
	{
		HPR = Mathf.Lerp(HPR , LHP , 0.1f);
		LableHP.position = new Vector3(transform.position.x + LableHPPos.x , 
		transform.position.y + LableHPPos.y , transform.position.z + LableHPPos.z);
		LableHP.transform.GetChild(0).transform.GetChild(0).transform.localScale = 
		new Vector3(HPR / max , 1 , 1);
		LableHP.LookAt(Camera.main.transform.position);
		if((int)HPR == 0)
		{
			print("Die");
			gameObject.SetActive(false);
			LableHP.gameObject.SetActive(false);
		}
	}
}
