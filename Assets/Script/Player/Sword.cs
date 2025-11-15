using UnityEngine;

public class Sword : MonoBehaviour
{
    Player player;
    [SerializeField] private Transform Random_pos, lable_hp;
    // private RaycastHit hit;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    protected void FixedUpdate()
    {
        DrawRay(Vector3.left, Vector3.right, new Vector3(0, 0, 1), new Vector3(0, 0, -1));
    }
    private void DrawRay(params Vector3[] roate)
    {
        RaycastHit hitv;
        foreach (var item in roate)
        {
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, transform.TransformDirection(item) * player.LenSword, Color.red);
#endif
            if (Physics.Raycast(transform.position, transform.TransformDirection(item), out hitv, player.LenSword))
            {
                ai_enemy enemy;
                if (player.AttakAction && hitv.transform.CompareTag("Enemy") &&
                    hitv.transform.TryGetComponent<ai_enemy>(out enemy) &&
                    hitv.collider.isTrigger == false && player.AttakAction == true)
                {
                    enemy.HPM = 5;
                    break;
                }
                else if (player.AttakAction && hitv.transform.CompareTag("non-player") &&
                    hitv.collider.isTrigger == false && player.AttakAction == true)
                {
                    if (hitv.transform.tag != "Enemy")
                    {
                        hitv.transform.tag = "Enemy";
                        var ai__enemy = hitv.transform.gameObject.AddComponent<ai_enemy>();
                        ai__enemy.RandomPos = Random_pos;
                        ai__enemy.LHP = 1000;
                        ai__enemy.LableHP = lable_hp;
                        ai__enemy.LableHPPos = new Vector3(.35f, 1.3f, 0);
                        hitv.transform.gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();

                    }
                }
            }
        }
    }
}
