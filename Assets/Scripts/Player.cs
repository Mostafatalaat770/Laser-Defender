using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header(header: "Player")]
    [SerializeField] float speed = 8f;
    [SerializeField] float padding = 1f;
    [SerializeField] float health = 500f;

    [Header(header: "Laser Beam")]
    [SerializeField] GameObject laserBeam;
    [SerializeField] float laserBeamSpeed = 20f;
    [SerializeField] float laserBeamFiringPeriod = 3;
    [SerializeField] AudioClip laserBeamClip;
    [SerializeField] [Range(0, 1)] float laserVolume = 1f;

    [Header(header: "SoundFX")]
    [SerializeField] AudioClip deathClip;
    [SerializeField][Range(0,1)] float deathVolume = 1f;

    Coroutine firingCoroutine;
    float minX;
    float maxX;
    float minY;
    float maxY;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            CreateLaserBeam();
            yield return new WaitForSeconds(laserBeamFiringPeriod);
        }
    }

    private void CreateLaserBeam()
    {
        GameObject laser = Instantiate(laserBeam,
            transform.position,
            Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserBeamSpeed);
        AudioSource.PlayClipAtPoint(laserBeamClip, Camera.main.transform.position, laserVolume);
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);


        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);


        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetupMoveBoundries()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position,deathVolume);
            Destroy(gameObject);
        }
    }
}
