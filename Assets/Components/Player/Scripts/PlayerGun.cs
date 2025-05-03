using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;
    public float aimSpeed = 5f;

    
    public void Aim(Vector3 targetPos)
    { // will change
        targetPos=Camera.main.ScreenToViewportPoint(targetPos);
        if (transform.lossyScale.x<0)
        {
            targetPos.x = 1-targetPos.x;
        }
        targetPos=Camera.main.ViewportToWorldPoint(targetPos);
        Vector3 distance = targetPos - transform.position;
        distance.Normalize();

        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        angle =Mathf.Clamp(angle, -90f, 90f);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * aimSpeed);

    }
    public void Shoot()
    {
        Vector3 shootDir = transform.right;
        if (transform.lossyScale.x < 0)
        {
            shootDir *= -1;
        }
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.Go(shootDir, bulletSpeed);
    }
}
