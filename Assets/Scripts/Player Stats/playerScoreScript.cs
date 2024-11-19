using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScoreScript : MonoBehaviour
{
    public static playerScoreScript Instance;
    private float distanceToGrond;
    public float finalDistanceBeforeFall{get; set;}
    public float getScore{get; set;}
    private float scoreMultiplier = 2f;
    private bool isFalling = false;

    void Awake(){
        Instance = this;
    }
    private void Update() {
        distanceToGround();
        storeScoreAndFinalDist();
    }
    public float distanceToGround(){
        var rocketObj = GameObject.Find("Rocket");
        var groundObj = GameObject.Find("ground");

        var rocketBounds = rocketObj.GetComponent<BoxCollider2D>().bounds;
        var groundBounds = groundObj.GetComponent<BoxCollider2D>().bounds;

        // Distance from the rocket collider bottom edge to the ground collider top edge
        return distanceToGrond = rocketBounds.min.y - groundBounds.max.y;
    }
    void storeScoreAndFinalDist(){
        var rb = GameObject.Find("Rocket").GetComponent<Rigidbody2D>();

        // Check if the rocket's velocity is transitioning from upward to downward
        if (rb.velocity.y < 0 && !isFalling && rocketScript.Instance.isLaunching){
            isFalling = true; // Rocket is now falling
            finalDistanceBeforeFall = distanceToGround(); // Store the final distance
        }
        else if (rb.velocity.y == 0){
            isFalling = false; // Reset if the rocket is not falling
        }
    }
    public float playerScore(){
        //roundoff to the nearest number
        return Mathf.Round(finalDistanceBeforeFall * scoreMultiplier);
    }
}
