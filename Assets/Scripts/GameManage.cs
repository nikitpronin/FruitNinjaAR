using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManage : MonoBehaviour
{
    public static GameManage Instance {get;set;}

    private const float REQUIRED_SLICEFORCE = 400.0f;   

    private List<Fruit> fruits = new List<Fruit>();
    public GameObject fruitPrefab;

    public Transform trail;

    private Collider2D[] fruitColls;

    private float lastSpawn;
    private float deltaSpawn = 1.0f;
    private Vector3 lastMousePosition;

    private int score;
    private int highestScore;
    private int lifepoint;

    private Fruit GetFruit(){
        Fruit f = fruits.Find(x => !x.IsActive);
        if (f == null){
            f = Instantiate(fruitPrefab).GetComponent<Fruit>();
            fruits.Add(f);
        }

        return f;
    }
    
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        fruitColls = new Collider2D[0];
        NewGame();
    }

    private void NewGame(){
        score = 0;
        lifepoint = 3;
    }
    
    private void Update(){
        if (Time.time - lastSpawn > deltaSpawn){
            Fruit f = GetFruit();
            float randomX = Random.Range(-1.65f, 1.65f);

            f.LaunchFruit(Random.Range(1.85f, 2.75f),randomX, -randomX);
            lastSpawn = Time.time;
        }

        if (Input.GetMouseButton(0)){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 1;
            trail.position = pos;

            Collider2D[] thisFruits = Physics2D.OverlapPointAll(new Vector2(pos.x,pos.y), LayerMask.GetMask("Fruit"));    

            if ((Input.mousePosition - lastMousePosition).sqrMagnitude > 9)
            foreach (var item in thisFruits)
            {
                for (int i=0; i<thisFruits.Length; i++)
                {
                    if (item == thisFruits[i])
                    Debug.Log(item.name);

                }
            }
            fruitColls = thisFruits;
       }
    }

    public void Death(){

    }

    public void LoseLP(){
        if (lifepoint == 0)
            return;
        lifepoint--;
        //здесь UI

        if(lifepoint == 0)
            Death();
    }

}
