using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeck : MonoBehaviour
{
    
    public static MonsterDeck instance;
    private void Awake()
    {
        instance = this;
    }
    public List<CardInfo> baseDeck = new List<CardInfo>();
    public List<CardInfo> activeDeck = new List<CardInfo>();
    public Card topCard;
    public float drawDelay = .05f;
    bool startupFinished = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetupDeck(List<CardInfo> cardInfoList){
        while (cardInfoList.Count > 0){
            int selected = Random.Range(0, cardInfoList.Count);
            activeDeck.Add(cardInfoList[selected]);
            cardInfoList.RemoveAt(selected);
        }
    }
    public void DrawCardToMonsterHand(){
        if (startupFinished && activeDeck.Count == 0)
        {
            SetupDeck(DiscardSystem.instance.monsterDiscardPile);
        }
        if (activeDeck.Count != 0){
            MonsterHand.instance.addCardToHand(activeDeck[0]);
            activeDeck.RemoveAt(0);
            startupFinished = true;
        }
    }
    public void DrawCards(int amount){
        StartCoroutine(DrawCardsCo(amount));
    }


    IEnumerator DrawCardsCo(int amount){
        for(int i = 0; i <= amount-1; i++){
            DrawCardToMonsterHand();
            yield return new WaitForSeconds(drawDelay);
        }
    }
}
