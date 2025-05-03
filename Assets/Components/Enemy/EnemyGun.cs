using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float bulletSpeed = 10f;

    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;
    public float aimSpeed = 5f;
    private float accuracyError;

    void Start()
    {
       accuracyError = Random.Range(-15f, 15f);
    }


    void Update()
    {
        
    }

    public void Aim(Transform target,int facingDirection)
    {
        Vector3 distance = target.position - transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + accuracyError;
        if (facingDirection == -1)
        {
            angle += 180;
            angle *= -1;
        }
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * aimSpeed);
    }
    public void Shoot()
    {
        accuracyError = Random.Range(-15f, 15f);
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.Go(transform.right, bulletSpeed);
    }
}
