using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardSystem : MonoBehaviour
{
    public List<CardInfo> discardPile = new List<CardInfo>();
    public List<CardInfo> monsterDiscardPile = new List<CardInfo>();
    public static DiscardSystem instance;
    private void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addCardToDiscard(CardInfo cardInfo){
        discardPile.Add(cardInfo);   
    }
    public void addCardToMonsterDiscard(CardInfo cardInfo)
    {
        monsterDiscardPile.Add(cardInfo);
    }
    //public List<CardInfo> getPlayerDiscardPile()
    //{
    //    //TODO make a pop chain onto the active deck since its being dumb
    //    List<CardInfo> returnList = discardPile;
    //    Debug.Log(discardPile.Count);
    //    //discardPile.Clear();
    //    //Debug.Log(returnList.Count);
    //    return returnList;
    //}
    ////public List<CardInfo> getMonsterDiscardPile()
    ////{
    ////    List<CardInfo> returnList = monsterDiscardPile;
    ////    //monsterDiscardPile.Clear();
    ////    //Debug.Log(returnList.Count);  
    ////    return returnList;
    //}
    public void clearPlayerDiscard() { 
        discardPile.Clear();
    }
    public void clearMonsterDiscard() { 
        monsterDiscardPile.Clear();
    }
}
