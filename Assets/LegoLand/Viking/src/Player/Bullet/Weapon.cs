using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate;
    public Transform bulletPos;
    public GameObject bullet;
    public int maxAmmo;
    public int currentAmmo;

    private void Awake()
    {
        currentAmmo = GetComponentInParent<Player>().ammo;  
    }

    public void Use()
    {
        if(currentAmmo > 0)
        {
            currentAmmo--;
            StartCoroutine(Shot());
        }
    }

    IEnumerator Shot()
    {
        // #. �Ѿ� �߻�
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;
        // #. ź�� ����
        yield return null;
    }
}


