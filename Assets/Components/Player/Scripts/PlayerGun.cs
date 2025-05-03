using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;
    public float aimSpeed = 5f;

    
    public void Aim(Transform target)
    { // will change
        Vector3 distance = target.position - transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * aimSpeed);
    }
    public void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.Go(transform.right, bulletSpeed);
    }
}
