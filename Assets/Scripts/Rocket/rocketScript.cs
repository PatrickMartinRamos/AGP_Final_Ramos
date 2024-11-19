using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class rocketScript : MonoBehaviour
{
  public static rocketScript Instance;
  public bool isLaunching{get; set;} = false;
  private bool readyTolaunch = false; //make sure that rocket wont launch until a new value for thrust is set
  private bool reverseSlider;
  private float newThrustValue;
  private Slider rocketThrustSlider;
  private Rigidbody2D rb;
  private gameUiScript uiScript;
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
  }
  
  private void Update() {
    handleLaunchInput();
    handleThrustInput();
  }

  void FixedUpdate() {
    if(rocketStatScript.Instance.currentFuel > 0  && isLaunching){
      launchRocket(); 
      applyAirResistance();
    }      
    else if(rocketStatScript.Instance.currentFuel <= 0){
      //make sure that fuel will reset when rocket is not flying or on ground
      //it also stop the thrust slider when rocket is flying
      //set launch ready to false and activate the thrustSlider gameobject
      //ready to launch set it to false when on ground so player can set a new value for rocket thrust
      if(rb.velocity == Vector2.zero){
        isLaunching = false;
        readyTolaunch = false;
        rocketStatScript.Instance.currentFuel = rocketStatScript.Instance.maxFuel;
        uiScript.launchingRocketUiButton(true);
      }
    }
  }

  void handleLaunchInput(){
    //to make sure rocket cant launh again when on rocket is launching or falling 
    if(!isLaunching && rb.velocity == Vector2.zero){
      //launch the rocket if ready to launch and shop panel is close
      if(Input.GetKeyDown(KeyCode.Space) && readyTolaunch && !uiScript.isShopPanelOpen){
        isLaunching = true;
        playerScoreScript.Instance.highestRocketReach = 0;
      }
    }
  }

  public float handleThrustInput(){
    //handle the value of the thrust base on where the slider stop
    //the slider will increment and edecrement while holding the F key when release
    //it will get the last value of the slider and assign it to the launchRocker();
    //cant set the thrust when shop panel is open
    if(Input.GetKey(KeyCode.F) && !rocketScript.Instance.isLaunching && !uiScript.isShopPanelOpen){        
      if(rocketThrustSlider.value <= rocketThrustSlider.maxValue && !reverseSlider){
        rocketThrustSlider.value += 30f * Time.deltaTime;
        if(rocketThrustSlider.value == rocketThrustSlider.maxValue){
          reverseSlider = true;
        }
      }
      else if(rocketThrustSlider.value >= rocketThrustSlider.minValue && reverseSlider){
        rocketThrustSlider.value -= 30f * Time.deltaTime;
        if(rocketThrustSlider.value == rocketThrustSlider.minValue){
          reverseSlider = false;
        }
      }
    }
    if(Input.GetKeyUp(KeyCode.F)&& !uiScript.isShopPanelOpen){
      //assign the new value to newThrustValue
      newThrustValue = rocketThrustSlider.value;
      //when set to true player can now launch by pressing space
      readyTolaunch = true;
      uiScript.launchingRocketUiButton(false);
    }

    return newThrustValue;
  }

  void launchRocket(){
    rb.AddForce(new Vector2(0,handleThrustInput()) * Time.fixedDeltaTime, ForceMode2D.Impulse);
    rocketStatScript.Instance.currentFuel -= rocketStatScript.Instance.fuelConsumptionSpeed * Time.fixedDeltaTime;
  }

  void applyAirResistance(){
    // air resistance on y
    var airResistance = 20f;
    Vector2 resistance = new Vector2(0f, -rb.velocity.y * airResistance);
    //Debug.Log("s");
    rb.AddForce(resistance);
  }
  
}
