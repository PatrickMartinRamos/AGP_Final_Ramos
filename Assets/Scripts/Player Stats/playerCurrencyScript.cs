using UnityEngine;

public class playerCurrencyScript : MonoBehaviour
{
    public static playerCurrencyScript Instance;
    public int currentCurrency{get; set;} 
    public int currencyMultiplier{get; set;} = 1;
    private bool isCurrencyUpdated = false;
    private Rigidbody2D rb;
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        rb = GameObject.Find("Rocket").GetComponent<Rigidbody2D>();
    }

    private void Update() {
        UpdateCurrency();
        Debug.Log(currencyMultiplier);
    }
    private void FixedUpdate() {
        //reset the currency multiplier to default 1
        if(rb.velocity == Vector2.zero){
            currencyMultiplier = 1;
        }
    }
    public void UpdateCurrency() {
        //only update when the rocket has landed and currency hasnt updated
        if (playerScoreScript.Instance.showResultPanel && !isCurrencyUpdated) {
            int score = Mathf.RoundToInt(playerScoreScript.Instance.getScore() * currencyMultiplier);
       
            currentCurrency += score; //add the score to the existing currency
            isCurrencyUpdated = true;
        }
        else if (!playerScoreScript.Instance.showResultPanel) {
            //reset the flag when the rocket is not landed yet
            isCurrencyUpdated = false;
        }
    }
}
