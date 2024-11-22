using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private Rigidbody2D obstacleRb;
    private void Start() {
        rb = GameObject.Find("Rocket").GetComponent<Rigidbody2D>();
        obstacleRb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        Vector2 currentVelocity = obstacleRb.velocity;
        currentVelocity.y = rb.velocity.y / 1.5f ;  
        obstacleRb.velocity = currentVelocity; 
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            rocketStatScript.Instance.currentFuel -= 25f;
            Debug.Log("collisionPlayer");
            Destroy(this.gameObject);
        }
    }
}
