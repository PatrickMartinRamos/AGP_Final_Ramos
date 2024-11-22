using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class rocketBoostScripts : MonoBehaviour
{   
    private Rigidbody2D rb;
    private Rigidbody2D obstacleRb;
    private float addBoost;
    [SerializeField] private TextMeshProUGUI addBoostText;
    private void Start() {
        rb = GameObject.Find("Rocket").GetComponent<Rigidbody2D>();
        obstacleRb = GetComponent<Rigidbody2D>();
        addBoost = Random.Range(100, 300);
        addBoostText.text = addBoost.ToString();
    }
    private void FixedUpdate() {
        Vector2 currentVelocity = obstacleRb.velocity;
        currentVelocity.y = rb.velocity.y  / 1.5f;  
        obstacleRb.velocity = currentVelocity; 
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            rb.AddForce(new Vector2(0, addBoost), ForceMode2D.Impulse);
            //Debug.Log("collisionPlayer");
            Destroy(this.gameObject);
        }
    }
}
