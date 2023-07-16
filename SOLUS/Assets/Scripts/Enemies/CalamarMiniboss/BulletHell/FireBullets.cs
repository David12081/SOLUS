using UnityEngine;

public class FireBullets : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    public float repeatRate;
    public Transform firePoint;

    private void Fire()
    {
        FindObjectOfType<AudioManager>().Play("SquidBullet");
        
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = firePoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = firePoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - firePoint.transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstanse.GetBullet();
            bul.transform.position = firePoint.transform.position;
            bul.transform.rotation = firePoint.transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }

    public void CancelFireBullet()
    {
        CancelInvoke("Fire");
    }

    public void InvokeFire()
    {
        InvokeRepeating("Fire", 0f, repeatRate);
    }
}