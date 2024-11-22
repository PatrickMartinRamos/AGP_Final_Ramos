using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class objectSpawnerScript : MonoBehaviour
{
    public static objectSpawnerScript Instance;
    [SerializeField] private List <Transform> objectSpawnPos = new List<Transform>();
    [SerializeField] private List <GameObject> objectToSpawn = new List<GameObject>();
    private Transform rocketTransform;
    private Rigidbody2D rb;
    public bool startSpawningObj {get; set;}

    private float timer = 0f;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        rb = GameObject.Find("Rocket").GetComponent<Rigidbody2D>();
        rocketTransform = GameObject.Find("Rocket").transform;
    }
    private void FixedUpdate() {
        if(rb.velocity.y < 0){
            DestroyObjectsWithTags();
        }
    }
    private void Update() {
        //Debug.Log(rocketScript.Instance.rb.position.y);
        transform.position = new Vector3(0 ,rocketTransform.position.y +9f,0);    
        
        if(startSpawningObj && rb.velocity.y > 5){
            timer += Time.deltaTime;
            if(timer >= 1.5){
                int ranDomObjectToSpawnIndex = Random.Range(0, objectToSpawn.Count);
                int ranDomObjectSpawnPosIndex = Random.Range(0, objectSpawnPos.Count);
                //Debug.Log("start spawn obj");
                Instantiate(objectToSpawn[ranDomObjectToSpawnIndex],objectSpawnPos[ranDomObjectSpawnPosIndex].position, Quaternion.identity );
                timer = 0f;
            }
        }
    }
    private void DestroyObjectsWithTags() {
        
    GameObject[] objectsWithTagCurrencyMultiplier = GameObject.FindGameObjectsWithTag("currencyMultiplier");
    foreach (GameObject obj in objectsWithTagCurrencyMultiplier) {
        Destroy(obj);
    }

    GameObject[] objectsWithTagRocketBoost = GameObject.FindGameObjectsWithTag("rocketBoost");
    foreach (GameObject obj in objectsWithTagRocketBoost) {
        Destroy(obj);
    }

    GameObject[] objectsWithTagObstacleObj = GameObject.FindGameObjectsWithTag("obstacleObj");
    foreach (GameObject obj in objectsWithTagObstacleObj) {
        Destroy(obj);
    }
}

}
