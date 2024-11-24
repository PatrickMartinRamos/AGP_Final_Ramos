using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class rocketScript : MonoBehaviour
{
  public static rocketScript Instance;
  public bool isLaunching{get; set;} = false;
  public bool readyTolaunch {get; set;}= false; //make sure that rocket wont launch until a new value for thrust is set
  private bool reverseSlider;
  private float newThrustValue;
  private Slider rocketThrustSlider;
  public Rigidbody2D rb{get; set;}
  private gameUiScript uiScript;
  public Vector2 orgTransform{get;set;}

  //clamp rocket in x axis
  [SerializeField]private float minX;
  [SerializeField]private float maxX;
  private void Awake() {
    Instance = this;
  }
  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    uiScript = FindObjectOfType<gameUiScript>();
    rocketThrustSlider = uiScript.rocketSlider();
    reverseSlider = uiScript.reverseSlider();
    rb.mass = rocketStatScript.Instance.rocketMass;
    rocketStatScript.Instance.currentFuel = rocketStatScript.Instance.maxFuel;
    orgTransform = transform.position;
  }
  
  private void Update() {
    handleLaunchInput();
    handleThrustInput();
    clampRocketX();
    rocketControl();
    if(rocketStatScript.Instance.currentFuel > 0  && isLaunching){
      fuelConsumptionRate();
    }   
  }

  void clampRocketX(){
    //clamp player movement between min and max
    float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
    transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
  }

  void FixedUpdate() {
    applyUpwardAirResistance();
    applyDownwardAirResistance();
    if(rocketStatScript.Instance.currentFuel > 0  && isLaunching){
      launchRocket(); 

    }      
    else if(rocketStatScript.Instance.currentFuel <= 0){
      //make sure that fuel will reset when rocket is not flying or on ground
      //it also stop the thrust slider when rocket is flying
      //set launch ready to false and activate the thrustSlider gameobject
      //ready to launch set it to false when on ground so player can set a new value for rocket thrust
      if(rb.velocity == Vector2.zero){
        isLaunching = false;
        readyTolaunch = false;
        objectSpawnerScript.Instance.startSpawningObj = false;
        rocketStatScript.Instance.currentFuel = rocketStatScript.Instance.maxFuel;
        uiScript.launchingRocketUiButton(true);
      }
    }
  }

  void handleLaunchInput(){
    //to make sure rocket cant launh again when on rocket is launching or falling 
    if(!isLaunching && rb.velocity == Vector2.zero){
      //launch the rocket if ready to launch and shop panel is close
      if(Input.GetKeyDown(KeyCode.Space) && readyTolaunch && !uiScript.isShopPanelOpen && !playerScoreScript.Instance.showResultPanel){
        isLaunching = true;
        playerScoreScript.Instance.highestRocketReach = 0;
        objectSpawnerScript.Instance.startSpawningObj = true;
      }
    }
  }

  public float handleThrustInput(){
    //handle the value of the thrust base on where the slider stop
    //the slider will increment and edecrement while holding the F key when release
    //it will get the last value of the slider and assign it to the launchRocker();
    //cant set the thrust when shop panel is open
    if(Input.GetKey(KeyCode.F) && !rocketScript.Instance.isLaunching && !uiScript.isShopPanelOpen && !playerScoreScript.Instance.showResultPanel){        
      if(rocketThrustSlider.value <= rocketThrustSlider.maxValue && !reverseSlider){
        rocketThrustSlider.value += rocketStatScript.Instance.thrustControlSpeed * Time.deltaTime;
        if(rocketThrustSlider.value == rocketThrustSlider.maxValue){
          reverseSlider = true;
        }
      }
      else if(rocketThrustSlider.value >= rocketThrustSlider.minValue && reverseSlider){
        rocketThrustSlider.value -= rocketStatScript.Instance.thrustControlSpeed * Time.deltaTime;
        if(rocketThrustSlider.value == rocketThrustSlider.minValue){
          reverseSlider = false;
        }
      }
    }
    if(Input.GetKeyUp(KeyCode.F)&& !uiScript.isShopPanelOpen && !playerScoreScript.Instance.showResultPanel){
      //assign the new value to newThrustValue
      newThrustValue = rocketThrustSlider.value;
      //when set to true player can now launch by pressing space
      readyTolaunch = true;
      uiScript.launchingRocketUiButton(false);
    }

    return newThrustValue;
  }

  void launchRocket(){
    //launch the rocket using add force impulse
    rb.AddForce(new Vector2(0,handleThrustInput()) * Time.fixedDeltaTime, ForceMode2D.Impulse);
  }

  void rocketControl(){
    if(Input.GetKey(KeyCode.A) && rb.velocity.y > 0) {
      transform.Translate(new Vector2(-1,0)*rocketStatScript.Instance.rocketControl * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.D) && rb.velocity.y > 0) {
      transform.Translate(new Vector2(1,0)*rocketStatScript.Instance.rocketControl * Time.deltaTime);   
    }

    // if velocity is zero return the rocket to the original position
    if(rb.velocity.y == 0){
      rb.velocity = Vector2.zero;
      transform.position = orgTransform;
    }
  }

  void fuelConsumptionRate(){
    rocketStatScript.Instance.currentFuel -= rocketStatScript.Instance.fuelConsumptionSpeed * Time.deltaTime;
    //,make sure current fuel doesnt go below 0
    rocketStatScript.Instance.currentFuel = Mathf.Max(rocketStatScript.Instance.currentFuel, 0);

  }
  void applyUpwardAirResistance(){
    if(rb.velocity.y > 0){
      //air resistance on y
      var airResistance = 20f;
      Vector2 resistance = new Vector2(0f, -rb.velocity.y * airResistance);
      //Debug.Log("upward air res");
      rb.AddForce(resistance, ForceMode2D.Force);
    }
  }
  void applyDownwardAirResistance(){
  //if rocket going down change the air resistance to a lower value
  if(rb.velocity.y < 0){
      //air resistance on y
      var airResistance = 20f;
      Vector2 resistance = new Vector2(0f, -rb.velocity.y * airResistance);
      float distToGroundCheck = 10f;
      Debug.DrawRay(transform.position, Vector2.down * distToGroundCheck,Color.red);
      
      // use raycast to check if the rocket is closer to the ground and if closer change the air resitance to a lower value
      RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGroundCheck,LayerMask.GetMask("Ground"));
      if(hit.collider != null){
        var newAirResistance = 1f;
        resistance = new Vector2(0f, -rb.velocity.y * newAirResistance);
      }
      Debug.Log("airResistance");

      rb.AddForce(resistance, ForceMode2D.Force);
    }
  }
}
