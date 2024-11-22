using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeManagerScript : MonoBehaviour
{
    public static upgradeManagerScript Instance;
    private int playerCurrency;
    private rocketStatScript rocketStat;
    //max thrust upgrade
    public int maxThrustUpgradePrice {get; set;}
    public bool isMaxThrustUpradeCanPush {get; set;}
    private int maxThrustUpgradeLevel = 0;
    //thrust speed control upgrade
    public int thrustControlPrice {get; set;}
    public bool isThrustControlSpeedUpradeCanPush {get; set;}
    private int thrustControlUpgradeLevel = 0;
    //max fuel upgrade
    public int maxFuelPrice {get; set;}
    public bool isMaxFuelUpgradeCanPush {get; set;}
    private int maxFuelUpgradeLevel = 0;

    private void Awake() {
        Instance = this;
        rocketStat = rocketStatScript.Instance;
    }

    private void Start() {
        //set starting price 
        maxThrustUpgradePrice = 2;
        thrustControlPrice = 2;
        maxFuelPrice = 2;

    }

    private void Update() {
        playerCurrency = playerCurrencyScript.Instance.currentCurrency;
        checkIfCanPushThrustPrice();
        checkIfCanPushThrustControlSpeedPrice();
        checkIfCanPushMaxFuelPrice();
    }
#region check if upgrade can push
    void checkIfCanPushThrustPrice(){
        if (playerCurrency >= maxThrustUpgradePrice){
            isMaxThrustUpradeCanPush = true;
        }else{
            isMaxThrustUpradeCanPush = false;
        }
    }
    void checkIfCanPushThrustControlSpeedPrice(){
        if(playerCurrency >= thrustControlPrice){
            isThrustControlSpeedUpradeCanPush = true;
        }else{
            isThrustControlSpeedUpradeCanPush = false;
        }
    }
    void checkIfCanPushMaxFuelPrice(){
        if(playerCurrency >= maxFuelPrice){
            isMaxFuelUpgradeCanPush = true;
        }else{
            isMaxFuelUpgradeCanPush = false;
        }
    }
#endregion

#region thrust upgrade
    public void upgradeMaxThrust() {
        //ensure the player has enough currency
        if (playerCurrency >= maxThrustUpgradePrice && isMaxThrustUpradeCanPush) {  
            maxThrustUpgradeLevel++; //increment the upgrade level 
            switch (maxThrustUpgradeLevel)
            {   
                case 1:
                    rocketStat.maxRocketthrust += new Vector2(0, 25);
                    playerCurrency -= maxThrustUpgradePrice;  //deduct currency
                    maxThrustUpgradePrice = 4;  //det price for the next upgrade
                break;

                case 2:
                    rocketStat.maxRocketthrust += new Vector2(0, 50);
                    playerCurrency -= maxThrustUpgradePrice;
                    maxThrustUpgradePrice = 6;
                break;

                case 3:
                    rocketStat.maxRocketthrust += new Vector2(0, 75);
                    playerCurrency -= maxThrustUpgradePrice;
                    maxThrustUpgradePrice = 8;
                break;

                case 4:
                    rocketStat.maxRocketthrust += new Vector2(0, 100);
                    playerCurrency -= maxThrustUpgradePrice;
                    maxThrustUpgradePrice = 10;
                break;

                case 5:
                    rocketStat.maxRocketthrust += new Vector2(0, 125);
                    playerCurrency -= maxThrustUpgradePrice;
                    maxThrustUpgradePrice = 12;
                break;

                case 6:
                    rocketStat.maxRocketthrust += new Vector2(0, 150);
                    playerCurrency -= maxThrustUpgradePrice;
                    maxThrustUpgradePrice = 14;
                break;

                default:
                    //Debug.Log("Max upgrade level reached");
                break;
            }

            // Update player currency after upgrade
            playerCurrencyScript.Instance.currentCurrency = playerCurrency;
        }
    }
#endregion

#region thrust speed control upgrade
    public void upgradeThrustControlSpeed(){
        if(playerCurrency >= thrustControlPrice && isThrustControlSpeedUpradeCanPush){
            thrustControlUpgradeLevel++;

            switch (thrustControlUpgradeLevel)
            {
                case 1 :
                    rocketStat.thrustControlSpeed += 30;
                    playerCurrency -= thrustControlPrice;
                    thrustControlPrice = 4;
                break;

                case 2 :
                    rocketStat.thrustControlSpeed += 30;
                    playerCurrency -= thrustControlPrice;
                    thrustControlPrice = 6;
                break;

                case 3 :
                    rocketStat.thrustControlSpeed += 30;
                    playerCurrency -= thrustControlPrice;
                    thrustControlPrice = 8;
                break;

                case 4 :
                    rocketStat.thrustControlSpeed += 30;
                    playerCurrency -= thrustControlPrice;
                    thrustControlPrice = 10;
                break;
                
                case 5 :
                    rocketStat.thrustControlSpeed += 30;
                    playerCurrency -= thrustControlPrice;
                    thrustControlPrice = 12;
                break;
                
                case 6 :
                    rocketStat.thrustControlSpeed += 20;
                    playerCurrency -= thrustControlPrice;
                    thrustControlPrice = 14;
                break;

                default:
                break;
            }

            playerCurrencyScript.Instance.currentCurrency = playerCurrency;
        }
    }
#endregion

#region max fuel upgrade
    public void upgradeMaxFuel(){
        if(playerCurrency >= maxFuelPrice && isMaxFuelUpgradeCanPush){
            maxFuelUpgradeLevel++;
            switch (maxFuelUpgradeLevel)
            {
                case 1 :
                    rocketStat.maxFuel += 30;
                    playerCurrency -= maxFuelPrice;
                    maxFuelPrice = 4;
                break;

                case 2 :
                    rocketStat.maxFuel += 30;
                    playerCurrency -= maxFuelPrice;
                    maxFuelPrice = 6;
                break;

                case 3 :
                    rocketStat.maxFuel += 30;
                    playerCurrency -= maxFuelPrice;
                    maxFuelPrice = 8;
                break;

                case 4 :
                    rocketStat.maxFuel += 30;
                    playerCurrency -= maxFuelPrice;
                    maxFuelPrice = 10;
                break;
                
                case 5 :
                    rocketStat.maxFuel += 30;
                    playerCurrency -= maxFuelPrice;
                    maxFuelPrice = 12;
                break;
                
                case 6 :
                    rocketStat.maxFuel += 20;
                    playerCurrency -= maxFuelPrice;
                    maxFuelPrice = 14;
                break;

                default:
                break;
            }

            playerCurrencyScript.Instance.currentCurrency = playerCurrency;
        }
    }
#endregion
}
