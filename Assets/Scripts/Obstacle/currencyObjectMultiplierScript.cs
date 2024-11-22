using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class currencyObjectMultiplierScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyvalue;
    private Rigidbody2D rb;
    private Rigidbody2D obstacleRb;
    private int randomCurrencyMultiplier;
    private void Start() {
        rb = GameObject.Find("Rocket").GetComponent<Rigidbody2D>();
        obstacleRb = GetComponent<Rigidbody2D>();
        randomCurrencyMultiplier = Random.Range(2, 5);
        currencyvalue.text = randomCurrencyMultiplier.ToString();
    }
    private void FixedUpdate() {
        Vector2 currentVelocity = obstacleRb.velocity;
        currentVelocity.y = rb.velocity.y / 1.5f;  
        obstacleRb.velocity = currentVelocity; 
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){         
            playerCurrencyScript.Instance.currencyMultiplier *= randomCurrencyMultiplier;
            Destroy(this.gameObject);
        }
    }
}
