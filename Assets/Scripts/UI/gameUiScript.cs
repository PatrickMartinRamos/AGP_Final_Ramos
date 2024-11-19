using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gameUiScript : MonoBehaviour
{    
    [Header("Shop UI")]
    [SerializeField] private List<Transform> maxThrustToggleUpgrade = new List<Transform>();
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject maxThrustToggleUpgradeBtn;
    [SerializeField] private GameObject maxThrustText;
    [SerializeField] private GameObject openShopBtn;
    public bool isShopPanelOpen{get; set;} //make sure player cant launch or set the thrust when shop panel is open
    // [SerializeField] private GameObject openShopPanelUIBtn;
    // [SerializeField] private GameObject closeShopPanelUIBtn;
    private int currentChildIndex = 0; // Keeps track of the current child to activate.
    [Header("Game UI")]
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Slider rocketThrustSlider;
    [SerializeField] private TextMeshProUGUI fuelStatText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI distanceToGroundText;
    [SerializeField] private TextMeshProUGUI finalDistanceToGroundText;
    [SerializeField] private TextMeshProUGUI defThrustValText;
    [SerializeField] private TextMeshProUGUI maxThrustValText;
    [Header("Result Panel UI")]
    [SerializeField] GameObject resultPanel;
    private bool reverse = false;
    private bool showResultPanel = false;
    private void Start() {
        fuelSlider.value = rocketStatScript.Instance.maxFuel;
        fuelSlider.maxValue = rocketStatScript.Instance.maxFuel;
        fuelSlider.minValue = 0f;

        resultPanel.SetActive(false);
        shopPanel.SetActive(false); 
    }
    private void Update() {
        fuelUiText();
        playerScoreUiText();
        distanceToGroundUiText();
        finalDistanceToGroundUiText();
        thrustValText();
        openShopButton();
        followRocket();
        showResultPanelUi();
        rocketThrustSlider.maxValue = rocketStatScript.Instance.maxRocketthrust.y;
        rocketThrustSlider.minValue = rocketStatScript.Instance.defaultRocketthrust.y;

    }
    void followRocket(){
        Transform rocketTransform = GameObject.Find("Rocket").transform;
        canvasTransform.position = new Vector3(0f,rocketTransform.position.y + 2.5f,0);    
    }
    void thrustValText(){
        defThrustValText.text = rocketStatScript.Instance.defaultRocketthrust.y.ToString("F1");
        maxThrustValText.text = rocketStatScript.Instance.maxRocketthrust.y.ToString("F1");
    }
    void fuelUiText(){
        fuelStatText.text = "Fuel\n" + rocketStatScript.Instance.currentFuel.ToString("F2");
        fuelSlider.value = rocketStatScript.Instance.currentFuel;
    }
    void distanceToGroundUiText(){
        distanceToGroundText.text = "Distance To Ground: " + playerScoreScript.Instance.getDistance().ToString("F1");
    }
    void finalDistanceToGroundUiText(){
        finalDistanceToGroundText.text = "Final Distance To Ground: " + playerScoreScript.Instance.getHighestRocketReach().ToString("F1");
    }
    void playerScoreUiText(){
       playerScoreText.text = "Score: " + playerScoreScript.Instance.getScore().ToString("F0");
    }
    public void launchingRocketUiButton(bool isOnGround){
        if(isOnGround){
            rocketThrustSlider.gameObject.SetActive(true);  //enable the slider when on the ground
            rocketThrustSlider.value = rocketThrustSlider.minValue;
        }
        else{
            rocketThrustSlider.gameObject.SetActive(false);  //disable the slider when launching
        }
    }

    public void toggleThrustUpgradeUi(){
        //if (maxThrustToggleUpgrade.Count == 0) return; //check if there are any toggles to process
        Transform currentToggle = maxThrustToggleUpgrade[currentChildIndex]; // get the current toggle in the list   
        //check kung may children
        if (currentToggle.childCount > 0){
            // Get the first child (or the child you want to activate)
            Transform childToActivate = currentToggle.GetChild(0);
            childToActivate.gameObject.SetActive(true); 
            //TODO: dto lagay ung logic for increasing the max thrust ng rocket
        }
        // Increment the child index and loop back if reached the end
        currentChildIndex++;
        if (currentChildIndex >= maxThrustToggleUpgrade.Count){
            //TODO: disable button na nag cacall for this function
            maxThrustToggleUpgradeBtn.SetActive(false);
            maxThrustText.SetActive(true);
            //Debug.Log("Max");
        }
    }
    public void openShopButton(){
        if(rocketScript.Instance.isLaunching){
            openShopBtn.SetActive(false);
        }
        else{
            openShopBtn.SetActive(true);
        }
    }  
    public void showResultPanelUi(){
        if(playerScoreScript.Instance.showPanel){
            resultPanel.SetActive(true); 
        }
        else{
            resultPanel.SetActive(false); 
        }
    } 
    public GameObject showShopPanelUi(){
        return shopPanel;
    }
    public GameObject resultPanelUi(){
        return resultPanel;
    }
    public Slider rocketSlider(){
        return rocketThrustSlider;
    }
    public bool reverseSlider(){
        return reverse;
    }
}

