using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{   
    public CharacterInfo instance;

    private void Awake(){
        instance = this;
    }
    public bool Player;
    public int handSize;
    public int initiative;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}