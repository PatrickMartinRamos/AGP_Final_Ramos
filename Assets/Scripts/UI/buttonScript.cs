using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameUiScript.toggleThrustUpgradeUi();
        upgradeManagerScript.upgradeThrustBaseShip_1(1);
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
        playerScoreScript.Instance.showPanel = false;
        playerScoreScript.Instance.hasFlown = false;
    }
}
