using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameUiScript : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI fuelStatText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI distanceToGroundText;
    [SerializeField] private TextMeshProUGUI finalDistanceToGroundText;
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Slider rocketThrustSlider;
    private bool reverse = false;
    private void Start() {
        fuelSlider.value = rocketStatScript.Instance.maxFuel;
        fuelSlider.maxValue = rocketStatScript.Instance.maxFuel;
        fuelSlider.minValue = 0f;

        rocketThrustSlider.maxValue = rocketStatScript.Instance.maxRocketthrust.y;
        rocketThrustSlider.minValue = rocketStatScript.Instance.defaultRocketthrust.y;
    }
    private void Update() {
        scoreUiText();
        fuelUi();
        playerScoreUi();
        distanceToGroundUi();
        finalDistanceToGroundUi();
    }
    void scoreUiText(){
    
    }
    void fuelUi(){
        fuelStatText.text = "Fuel\n" + rocketStatScript.Instance.currentFuel.ToString("F2");
        fuelSlider.value = rocketStatScript.Instance.currentFuel;
    }
    void distanceToGroundUi(){
        distanceToGroundText.text = "Distance To Ground: " + playerScoreScript.Instance.distanceToGround().ToString("F1");
    }
    void finalDistanceToGroundUi(){
        finalDistanceToGroundText.text = "Final Distance To Ground: " + playerScoreScript.Instance.finalDistanceBeforeFall.ToString("F1");
    }
    void playerScoreUi(){
        playerScoreText.text = "Score: " + playerScoreScript.Instance.playerScore().ToString("F0");
    }
    public void launchingRocketUi(bool isOnGround){
        if(isOnGround){
            rocketThrustSlider.gameObject.SetActive(true);  //enable the slider when on the ground
            rocketThrustSlider.value = rocketThrustSlider.minValue;
        }
        else{
            rocketThrustSlider.gameObject.SetActive(false);  //disable the slider when launching
        }
    }
    public Slider rocketSlider(){
        return rocketThrustSlider;
    }
    public bool reverseSlider(){
        return reverse;
    }
}

