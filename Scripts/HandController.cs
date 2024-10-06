using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public static HandController instance;
    private void Awake(){
        instance = this;
    }
    public List<Card> cardsInHand = new List<Card>();
    public Transform min, max;
    private List<Vector3> cardPositions = new List<Vector3>();
    public float rotation;
    public float offset;
    
    // Start is called before the first frame update
    void Start()
    {
        SetCardPositionInHand();
        offset = 70;
    }

    // Update is called once per frame
    void Update()
    {
        SetCardPositionInHand();
    }
    public void SetCardPositionInHand()
    {
        cardPositions.Clear();
        
        Vector3 midPoint = (max.position + min.position)/2;
        float listMid = cardsInHand.Count/2;
        // Debug.Log(cardsInHand.Count % 2);
        if(cardsInHand.Count % 2 == 0){
            float leftMid = listMid - 1;
            float rightMid = listMid;
            for(int i = 0; i <= cardsInHand.Count-1; i++){
                Vector3 mod = midPoint;
                mod.x += -1 * ((rightMid * 2) - 1 - (i * 2)) * offset/2;
                cardPositions.Add(mod);
                
            }
        }
        else{
            for(int i = 0; i <= cardsInHand.Count-1; i++){
                Vector3 mod = midPoint;
                
                mod.x += (i-Mathf.Floor(listMid)) * offset;
                cardPositions.Add(mod);   
            }
            
            
        }
        for(int i = 0; i <= cardsInHand.Count-1; i++){
            // cardsInHand[i].transform.position = cardPositions[i];
            if (!cardsInHand[i].interacting){
                cardsInHand[i].transform.rotation = min.rotation;
                cardsInHand[i].handPosition = i;
                
                cardsInHand[i].MoveToPoint(cardPositions[i]);
            }
        }
    }
    public void addCardToHand(Card newCard){
        cardsInHand.Add(newCard);
        SetCardPositionInHand();
    }
    public void removeCard(int deadCard){

        cardsInHand.RemoveAt(deadCard);
        SetCardPositionInHand();
    }
}
