using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 0.5f;
    public Transform firingPoint;
    public Transform shieldPos;
    public GameObject player;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private float bullSpeed = 10;
    GameObject projectile;
    public GameObject shieldPrefab;
    

    float timeUntilFire;
    private Animator animator;
    private GameObject clone;

    private void Start()
    {
        
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.L) && timeUntilFire < Time.time)
        {
            Shoot();
            timeUntilFire = Time.time + fireRate;
            animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Shield();
            
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            Destroy(clone);
        }

    }

    public void Shoot()
    {
        Vector3 position = transform.position;
        projectile = Pool.Instance.GetFromPool();
        projectile.transform.position = position;

        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Transform>().localScale.x * bullSpeed, 0);
    }

    public void Shield()
    {
        clone = (GameObject)Instantiate(shieldPrefab, shieldPos.position, Quaternion.identity);
    }
}
