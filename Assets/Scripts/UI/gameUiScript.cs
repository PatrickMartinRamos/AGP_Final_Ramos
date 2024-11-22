using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gameUiScript : MonoBehaviour
{    
    [Header("Shop UI")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject openShopBtn;
    [SerializeField] private TextMeshProUGUI shopCurrentCurrencyTect;
    [Header("Max Thrust Upgrade UI")]
    //max thrust upgrade toggle UI
    [SerializeField] private List<Transform> maxThrustToggleUpgrade = new List<Transform>();
    [SerializeField] private GameObject maxThrustToggleUpgradeBtn;
    [SerializeField] private GameObject maxUpgradeMaxThrustText;
    [SerializeField] private TextMeshProUGUI thrustMaxPriceText;
    private int currentMaxThrustToggleChildIndex = 0;
    [Header("Thrust Control Speed Upgrade UI")]
    //thrust control speed upgrade toggle UI
    [SerializeField] private List<Transform> thrustControlSpeedToggleUpgrade = new List<Transform>();
    [SerializeField] private GameObject thrustControlSpeedToggleUpgradeBtn;
    [SerializeField] private GameObject maxUpgradeThrustControlSpeedText;
    [SerializeField] private TextMeshProUGUI thrustControlSpeedPriceText;
    private int currentThrustControlSpeedToggleChildIndex = 0;
    [Header("Max Fuel Upgrade UI")]
    //max fuel upgrade toggle UI
    [SerializeField] private List<Transform> maxFuelToggleUpgrade = new List<Transform>();
    [SerializeField] private GameObject maxFuelUpgradeBtn;
    [SerializeField] private GameObject maxFuelText;
    [SerializeField] private TextMeshProUGUI maxFuelPriceText;
    private int currentMaxFuelToggleChildrenIndex = 0;

    public bool isShopPanelOpen{get; set;} //make sure player cant launch or set the thrust when shop panel is open

    [Header("Game UI")]
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Slider rocketThrustSlider;
    [SerializeField] private TextMeshProUGUI fuelStatText;
    [SerializeField] private TextMeshProUGUI maxFuelStatText;
    [SerializeField] private TextMeshProUGUI playerScoreGameText;    
    [SerializeField] private TextMeshProUGUI distanceToGroundText;
    [SerializeField] private TextMeshProUGUI finalDistanceToGroundText;
    [SerializeField] private TextMeshProUGUI defThrustValText;
    [SerializeField] private TextMeshProUGUI maxThrustValText;
    [Header("Result Panel UI")]
    [SerializeField] GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI playerScoreResultText;
    [SerializeField] private TextMeshProUGUI playerCurrencyResultText;
    private bool reverse = false;
    private void Start() {
        fuelSlider.value = rocketStatScript.Instance.maxFuel;
        fuelSlider.maxValue = rocketStatScript.Instance.maxFuel;
        resultPanel.SetActive(false);
        shopPanel.SetActive(false); 
    }
    private void Update() {
        currentFuelUiText();
        playerScoreUiText();
        distanceToGroundUiText();
        finalDistanceToGroundUiText();
        thrustValText();
        openShopButton();
        followRocket();
        showResultPanelUi();
        playerCurrencyUiText();
        upgradeThrustPriceText();
        upgradeThrustControlSpeedPriceText();
        upgradeMaxFuelPriceText();
        shopCurrentCurrencyText();
        updateRocketThrustSlider();
        updateMaxFuelText();
        updateFuelSliders();
    }
#region text UI
    void followRocket(){
        Transform rocketTransform = GameObject.Find("Rocket").transform;
        canvasTransform.position = new Vector3(rocketTransform.position.x ,rocketTransform.position.y + 2.5f,0);    
    }
    void thrustValText(){
        defThrustValText.text = rocketStatScript.Instance.defaultRocketthrust.y.ToString("F1");
        maxThrustValText.text = rocketStatScript.Instance.maxRocketthrust.y.ToString("F1");
    }
    void currentFuelUiText(){
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
       playerScoreResultText.text = "Score: " + playerScoreScript.Instance.getScore().ToString("F0");
       playerScoreGameText.text = "Score Result: " + playerScoreScript.Instance.getScore().ToString("F0");
    }
    void playerCurrencyUiText(){
        playerCurrencyResultText.text = "Currency: " +  playerCurrencyScript.Instance.currentCurrency.ToString();
    }
    public void upgradeThrustPriceText(){
        thrustMaxPriceText.text = "Price:" + upgradeManagerScript.Instance.maxThrustUpgradePrice + " $"; 
    }
    public void upgradeThrustControlSpeedPriceText(){
        thrustControlSpeedPriceText.text = "Price: "+ upgradeManagerScript.Instance.thrustControlPrice + " $";
    }
    public void upgradeMaxFuelPriceText(){
        maxFuelPriceText.text = "Price: " + upgradeManagerScript.Instance.maxFuelPrice + " $";
    }
    public void shopCurrentCurrencyText(){
        shopCurrentCurrencyTect.text = "Currency: " + playerCurrencyScript.Instance.currentCurrency.ToString() + " $";
    }
#endregion
    public void launchingRocketUiButton(bool isOnGround){
        if(isOnGround){
            rocketThrustSlider.gameObject.SetActive(true);  //enable the slider when on the ground
            rocketThrustSlider.value = rocketThrustSlider.minValue;
        }
        else{
            rocketThrustSlider.gameObject.SetActive(false);  //disable the slider when launching
        }
    }

    public void updateRocketThrustSlider(){
        //update the slider for rocket thrust slider when max value of slider is upgraded
        rocketThrustSlider.maxValue = rocketStatScript.Instance.maxRocketthrust.y;
        rocketThrustSlider.minValue = rocketStatScript.Instance.defaultRocketthrust.y;
    }

    public void updateFuelSliders(){  
        fuelSlider.value = rocketStatScript.Instance.currentFuel;  
        fuelSlider.maxValue = rocketStatScript.Instance.maxFuel;
    }
    public void updateMaxFuelText(){
        maxFuelStatText.text = rocketStatScript.Instance.maxFuel.ToString("F1");
    }

    public void toggleMaxThrustUpgradeUi(){    
        if(!upgradeManagerScript.Instance.isMaxThrustUpradeCanPush){
            return; //dont activate a toggle if upgrade cant push thru
        }
        Transform currentToggle = maxThrustToggleUpgrade[currentMaxThrustToggleChildIndex]; // get the current toggle in the list   
        //check kung may children
        if (currentToggle.childCount > 0){
            //get the child
            Transform childToActivate = currentToggle.GetChild(0);
            childToActivate.gameObject.SetActive(true); 
            //TODO: dto lagay ung logic for increasing the max thrust ng rocket
        }
        currentMaxThrustToggleChildIndex++;
        if (currentMaxThrustToggleChildIndex >= maxThrustToggleUpgrade.Count){
            //TODO: disable button na nag cacall for this function
            maxThrustToggleUpgradeBtn.SetActive(false);
            maxUpgradeMaxThrustText.SetActive(true);
            //Debug.Log("Max");
        }
    }

    public void toggleThrustControlSpeedUpgradeUi(){    
        if(!upgradeManagerScript.Instance.isThrustControlSpeedUpradeCanPush){
            return;
        }
        Transform currentToggle = thrustControlSpeedToggleUpgrade[currentThrustControlSpeedToggleChildIndex];
        if (currentToggle.childCount > 0){
            Transform childToActivate = currentToggle.GetChild(0);
            childToActivate.gameObject.SetActive(true);              
        }
        currentThrustControlSpeedToggleChildIndex++;
        if (currentThrustControlSpeedToggleChildIndex >= thrustControlSpeedToggleUpgrade.Count){
            thrustControlSpeedToggleUpgradeBtn.SetActive(false);
            maxUpgradeThrustControlSpeedText.SetActive(true);
        }
    }

    public void toggleMaxFuelUpgradeUi(){    
        if(!upgradeManagerScript.Instance.isMaxFuelUpgradeCanPush){
            return;
        }
        Transform currentToggle = maxFuelToggleUpgrade[currentMaxFuelToggleChildrenIndex];
        if (currentToggle.childCount > 0){
            Transform childToActivate = currentToggle.GetChild(0);
            childToActivate.gameObject.SetActive(true);    
        }
        currentMaxFuelToggleChildrenIndex++;
        if (currentMaxFuelToggleChildrenIndex >= maxFuelToggleUpgrade.Count){
            maxFuelUpgradeBtn.SetActive(false);
            maxFuelText.SetActive(true);
        }
        updateFuelSliders();
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
        if(playerScoreScript.Instance.showResultPanel){
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

