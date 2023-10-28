using UnityEngine;

public class point_enemy : MonoBehaviour
{
    private RaycastHit hit;
    [HideInInspector]public ai_enemy ThisEnemy;
    private void Start() {
        InvokeRepeating("UpdateRay" , .3f , .3f);
    }
    
    private void UpdateRay()
    {
		if (Physics.Raycast(new Vector3(transform.position.x , transform.position.y -40 , 
        transform.position.z), transform.TransformDirection(Vector3.up), out hit))
		{
			if(hit.transform.gameObject != null)
            {
                ThisEnemy.CrateRandomPosition();
            }
		}
    }
    #if UNITY_EDITOR
    private void Update() {
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 10, Color.red);
    }
    #endif
}
