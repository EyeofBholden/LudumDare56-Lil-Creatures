using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHand : MonoBehaviour
{
    
    public static MonsterHand instance;
    private void Awake(){
        instance = this;
    }
    public GameController controller;
    public List<CardInfo> cardsInHand = new List<CardInfo>();
    public Card topMonsterCard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void addCardToHand(CardInfo newCard){
        cardsInHand.Add(newCard);
        
    }
    public void removeCard(int deadCard){

        cardsInHand.RemoveAt(deadCard);
        
    }
    public void PlayMonsterCard(){
        Card newMonsterCard = Instantiate(topMonsterCard, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("MonsterStaging").transform);
        newMonsterCard.cardInfo = cardsInHand[0];
        newMonsterCard.monster = true;

        StagingController.instance.addMonsterCardToStaging(newMonsterCard);
        newMonsterCard.MoveToPoint(GameObject.FindGameObjectWithTag("MonsterStaging").transform.position);
        cardsInHand.RemoveAt(0);
     
    }
}
