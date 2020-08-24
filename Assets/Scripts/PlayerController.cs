using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float angle;

    public GameObject blastPrefab; 
    public Transform FirePoint; 

    public float timebtwshots; //add

    public Rigidbody2D rb;
    public Camera cam;
    private bool canShoot; //add
     
    Vector2 movement;
    Vector2 mousePos;

    void Start() //add
    {
        canShoot = true; //add    
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        var rawmouse = Input.mousePosition;
        rawmouse.z = 10;
        mousePos = cam.ScreenToWorldPoint(rawmouse);

        if (Input.GetButton("Fire1") && canShoot) //add
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        canShoot = false;
        Instantiate(blastPrefab, FirePoint.position, transform.rotation);
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(timebtwshots);
        canShoot = true;
    }
     void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
