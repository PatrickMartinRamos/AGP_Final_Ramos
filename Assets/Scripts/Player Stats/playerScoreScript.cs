using Unity.VisualScripting;
using UnityEngine;

public class playerScoreScript : MonoBehaviour
{
    public static playerScoreScript Instance;
    private Rigidbody2D rb;
    private float distToGround;
    public float highestRocketReach{get; set;} 
    public bool hasFlown {get; set;}
    public bool showResultPanel {get; set;}
    [SerializeField] GameObject rocket;
    [SerializeField] Transform ground;

    private void Awake(){
        Instance = this;
        rb = rocket.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        getDistance(); 
        CheckRocketState();
    }
    public float getDistance(){
        //calculate distance to the ground
        distToGround = Vector2.Distance(ground.position, rocket.transform.position) - 1;
        if (rb.velocity.y > 0){
            //update highest position   |  return the highest value between this two
            highestRocketReach = Mathf.Max(highestRocketReach, distToGround);
        }
        return distToGround;
    }
    private void CheckRocketState()
    {
        //set the hast flown and show panel when condition is met
        if (rb.velocity.y > 0 && !hasFlown && rocketScript.Instance.isLaunching){
            hasFlown = true; 
            showResultPanel = false;
        }
        else if (hasFlown && rb.velocity.y == 0){
            //rocket has landed after flying
            showResultPanel = true;
        }
    }
    public float getScore(){
        return getHighestRocketReach() * 2;
    }
    public float getHighestRocketReach(){
        return highestRocketReach;
    }
}
