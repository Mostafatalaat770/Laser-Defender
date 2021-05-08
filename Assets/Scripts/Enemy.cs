using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 500f;
    [SerializeField] LaserBeam LaserBeam;
    [SerializeField] float minTimeBetweenLaserBeamSpawn = 0.1f;
    [SerializeField] float maxTimeBetweenLaserBeamSpawn = 2f;
    [SerializeField] float laserBeamSpeed = 0.5f;
    [SerializeField] GameObject DeathVFX;
    [SerializeField] AudioClip deathClip;
    [SerializeField] [Range(0, 1)] float volume = 0.5f;
    [SerializeField] AudioClip laserBeamClip;
    [SerializeField] [Range(0, 1)] float laserVolume;
    float timeCounter;


    // Start is called before the first frame update
    void Start()
    {
        timeCounter = UnityEngine.Random.Range(minTimeBetweenLaserBeamSpawn,
            maxTimeBetweenLaserBeamSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        CountAndFire();
    }

    private void CountAndFire()
    {
        timeCounter -= Time.deltaTime;
        if(timeCounter <= 0f)
        {
            Fire();
            timeCounter = Random.Range(minTimeBetweenLaserBeamSpawn, maxTimeBetweenLaserBeamSpawn);
        }

    }

    private void Fire()
    {
        var laserBeam = Instantiate(LaserBeam,
            transform.position,
            Quaternion.identity);
        laserBeam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserBeamSpeed);
        AudioSource.PlayClipAtPoint(laserBeamClip, Camera.main.transform.position, laserVolume);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        if(health <= 0)
        {
            var explosion = Instantiate(DeathVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position,volume);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
