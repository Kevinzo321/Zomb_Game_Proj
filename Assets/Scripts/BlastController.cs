using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{

    public float speed;
    public float timeToLive;

    Vector3 moveVector;

    private void Start()
    {
        moveVector = Vector3.up * speed * Time.fixedDeltaTime;
        StartCoroutine(DestoryBlast());
    }

    private void FixedUpdate()
    {
        transform.Translate(moveVector); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    IEnumerator DestoryBlast()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
