using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;

    float timeSinceLastShot;

    public AudioSource[] sounds;
    public AudioSource shootingSound;
    public AudioSource reloadSound;
    public AudioSource dryGunSound;

    public ParticleSystem muzzleFlash;

    public GameObject bulletHole;

    private void Start()
    {
        sounds = GetComponents<AudioSource>();
        shootingSound = sounds[0];
        reloadSound = sounds[1];
        dryGunSound = sounds[2];

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            reloadSound.Play();
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if (gunData.currentAmmo > 0) 
        { 
            if (CanShoot())
            {
                if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.Damage(gunData.damage);

                    GameObject bH = Instantiate(bulletHole, hitInfo.point + new Vector3(0f, 0f, -.02f), Quaternion.LookRotation(-hitInfo.normal));
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
        else
        {
            dryGunSound.Play();
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward);
    }

    private void OnGunShot()
    {
        shootingSound.Play();
        muzzleFlash.Play();
    }

}
