using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    private gameUiScript gameUiScript;
    private upgradeManagerScript upgradeManagerScript;
    private void Start() {
        upgradeManagerScript = upgradeManagerScript.Instance;
        gameUiScript = FindObjectOfType<gameUiScript>();
    }
    public void upgradeMaxThrustBtn(){
        //TODO: dito lagay logic na i-increment value ng maxthrust
        gameUiScript.toggleMaxThrustUpgradeUi();
        upgradeManagerScript.upgradeMaxThrust();
    }
    public void upgradeThrustControlSpeedBtn(){
        //TODO: dito lagay logic na i-increment value ng maxthrust
        gameUiScript.toggleThrustControlSpeedUpgradeUi();
        upgradeManagerScript.upgradeThrustControlSpeed();
    }
    public void upgradeMaxFuelBtn(){
        gameUiScript.toggleMaxFuelUpgradeUi();
        upgradeManagerScript.upgradeMaxFuel();
        //when upgrade max button is click set the current fuel to max fuel because the max fuel has a new value
        rocketStatScript.Instance.currentFuel = rocketStatScript.Instance.maxFuel; 
    }
    public void goToPlayScene()
    {
        SceneManager.LoadScene(1);
    }
    public void exitApplication()
    {
        Application.Quit();
    }
    public void openShopPanelUiBtn(){
        gameUiScript.showShopPanelUi().gameObject.SetActive(true);
        gameUiScript.isShopPanelOpen = true;
    }
    public void closeShopPanelUiBtn(){
        gameUiScript.showShopPanelUi().gameObject.SetActive(false);
        gameUiScript.isShopPanelOpen = false;
    }
    public void closeResultPanelUi(){
        //reset the rocket state
        playerScoreScript.Instance.showResultPanel = false;
        playerScoreScript.Instance.hasFlown = false;
    }
}
