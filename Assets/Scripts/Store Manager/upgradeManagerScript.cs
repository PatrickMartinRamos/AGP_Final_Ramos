using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeManagerScript : MonoBehaviour
{
    public static upgradeManagerScript Instance;
    private rocketStatScript rocketStat;
    private Transform rocketObj;
    private Transform baseShip_1;
    private Transform baseShip_2;
    private Transform baseShip_3;
    
    private void Awake() {
        Instance = this;
        rocketStat = rocketStatScript.Instance;
    }

    private void Start() {
        rocketObj = GameObject.Find("Rocket").transform; //get the parent object
        baseShip_1 = rocketObj.Find("BaseShip_1");
        baseShip_2 = rocketObj.Find("BaseShip_2");
        baseShip_3 = rocketObj.Find("BaseShip_3");      
    }

    private void Update() {
       
    }

    public void upgradeThrustBaseShip_1(int increaseLevel){
        switch (increaseLevel)
        {
            case 1: 
                rocketStat.maxRocketthrust += new Vector2(0,25);
                Debug.Log("upgrade");
            break;

            case 2: 
                rocketStat.maxRocketthrust += new Vector2(0,25);
            break;

            case 3: 
                rocketStat.maxRocketthrust += new Vector2(0,25);
            break;

            case 4: 
                rocketStat.maxRocketthrust += new Vector2(0,25);
            break;
            
            default:
                break;
        }
    }
}
