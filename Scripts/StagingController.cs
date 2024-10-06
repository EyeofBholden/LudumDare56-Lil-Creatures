using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagingController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public List<Card> cardsInStaging = new List<Card>();
    public List<Card> monsterCardsInStaging = new List<Card>();
    // Start is called before the first frame update
    public static StagingController instance;
    private void Awake(){
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addCardToStaging(Card newCard){
        audioSource.PlayOneShot(clip, 0.75f);
        cardsInStaging.Add(newCard);

    }
    public void removeCardFromStaging(Card deadCard){
        cardsInStaging.Remove(deadCard);
    }
    public void addMonsterCardToStaging(Card newCard){
        monsterCardsInStaging.Add(newCard);
    }
    public void removeMonsterCardFromStaging(Card deadCard){
        monsterCardsInStaging.Remove(deadCard);
    }
    public void cleanUp(){
        if (cardsInStaging.Count > 0)
        {
            cardsInStaging[0].GoToDiscard();
            cardsInStaging.Remove(cardsInStaging[0]);
        }
        if (monsterCardsInStaging.Count > 0)
        {
            monsterCardsInStaging[0].GoToDiscard();

            monsterCardsInStaging.Remove(monsterCardsInStaging[0]);
        }

    }
}
