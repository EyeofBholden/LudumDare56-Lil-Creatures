using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{   
    public static EnemyInfo instance;

    private void Awake(){
        instance = this;
    }
    public int handSize;
    public int accuracy;
    public int power;
    public int avoidance;
    public int armour;
    public int initiative;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
