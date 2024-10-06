using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSystem : MonoBehaviour
{
    
    public static DeckSystem instance;
    private void Awake()
    {
        instance = this;
    }
    public AudioSource audioSource;
    public AudioClip clip;
    public List<CardInfo> baseDeck = new List<CardInfo>();
    public List<CardInfo> activeDeck = new List<CardInfo>();
    public Card topCard;
    public float drawDelay = .25f;
    bool startupFinished = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            DrawCardToHand();
        }
    }
    public void SetupDeck(List<CardInfo> cardInfoList){
        while (cardInfoList.Count > 0){
            int selected = Random.Range(0, cardInfoList.Count);
            activeDeck.Add(cardInfoList[selected]);
            cardInfoList.RemoveAt(selected);
        }
    }
    public void DrawCardToHand(){
        if (startupFinished && activeDeck.Count == 0)
        {
            SetupDeck(DiscardSystem.instance.discardPile);
        }
        
        if(activeDeck.Count != 0){
            Card newCard = Instantiate(topCard, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Hand").transform);
            newCard.cardInfo = activeDeck[0];
            activeDeck.RemoveAt(0);
            HandController.instance.addCardToHand(newCard);
            startupFinished = true;
            audioSource.PlayOneShot(clip, 0.75f);
        }
    }
    public void DrawCards(int amount){
        StartCoroutine(DrawCardsCo(amount));
    }

    IEnumerator DrawCardsCo(int amount){
        for(int i = 0; i <= amount-1; i++){
            DrawCardToHand();
            yield return new WaitForSeconds(drawDelay);
        }
    }
}
