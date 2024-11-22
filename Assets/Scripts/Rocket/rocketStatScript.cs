using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketStatScript : MonoBehaviour
{
    public static rocketStatScript Instance;
    public Vector2 defaultRocketthrust{get; set;} = new Vector2(0,100); //defaultt rocket thrust
    public Vector2 maxRocketthrust{get; set;} = new Vector2(0,350); //max limit of rocket thrust
    public Vector2 appliedRocketthrust{get; set;}
    public float rocketMass{get; set;} = 10;
    public float currentFuel{get; set;}
    public float maxFuel{get; set;} = 100;
    public float fuelConsumptionSpeed{get; set;} = 30;
    public float thrustControlSpeed{get; set;} = 50;
    public float rocketControl {get; set;} = 7;
    
    private void Awake() {
        Instance = this;
    }
}
