using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int maxMessages = 10;
    [SerializeField]
    List<Message> messageList = new List<Message>();
    
    
    public List<PartsCode> Heads = new List<PartsCode>();
    public List<PartsCode> Tails = new List<PartsCode>();
    public List<PartsCode> Body = new List<PartsCode>();
    public List<CardInfo> playerCards = new List<CardInfo>();
    public List<CardInfo> monsterCards = new List<CardInfo>();
    public static GameController instance;
    public GameObject actionButton, chatPanel, TextObject;
    public TargetController targetController;
    public bool playerHasPriority;
    //public Hand playerHand;
    public Image pBodyImage;
    public Image pHeadImage;
    public Image pTailImage;
    public Image mBodyImage;
    public Image mHeadImage;
    public Image mTailImage;
    public int hardmod = 0;
    public CharacterInfo localPlayer;
    public CharacterInfo nonLocalPlayer;
    //public HandController playerHand;
    public MonsterHand monsterHand;

    public bool RedoPriority = true;
    private void Awake(){
        instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        GeneratePlayer();
        Generateopponent();
        //DeckSystem.SetupDecks();
        DeckSystem.instance.DrawCards(localPlayer.instance.handSize);
        //SetupMonsterHand();
        actionButton.SetActive(false);
        DoPriority();
    }
    public void SetupMonsterHand() {
        MonsterDeck.instance.DrawCards(nonLocalPlayer.instance.handSize);
    }
    // Update is called once per frame
    void Update()
    {
        if(StagingController.instance.cardsInStaging.Count > 0){
            actionButton.SetActive(true);
        }
        if(StagingController.instance.cardsInStaging.Count == 0){
            actionButton.SetActive(false);
        }
    }

    public void GeneratePlayer() {

        PartsCode body = Body[Random.Range(0, Body.Count)];
        PartsCode head = Heads[Random.Range(0, Heads.Count)];
        PartsCode tail = Tails[Random.Range(0, Tails.Count)];
        pBodyImage.sprite = body.artwork;
        pHeadImage.sprite = head.artwork;
        pTailImage.sprite = tail.artwork;

        playerCards.AddRange(head.baseDeckChunk);
        playerCards.AddRange(tail.baseDeckChunk);
        playerCards.AddRange(body.baseDeckChunk);
        DeckSystem.instance.SetupDeck(playerCards);
    }

    public void Generateopponent()
    {
        PartsCode body = Body[Random.Range(0, Body.Count)];
        PartsCode head = Heads[Random.Range(0, Heads.Count)];
        PartsCode tail = Tails[Random.Range(0, Tails.Count)];
        mBodyImage.sprite = body.artwork;
        mHeadImage.sprite = head.artwork;
        mTailImage.sprite = tail.artwork;
        monsterCards.AddRange(head.baseDeckChunk);
        monsterCards.AddRange(tail.baseDeckChunk);
        monsterCards.AddRange(body.baseDeckChunk);
        MonsterDeck.instance.SetupDeck(monsterCards);
        SetupMonsterHand();
    }

    public void NukeOpponenetData()
    {
        DiscardSystem.instance.monsterDiscardPile.Clear();
        MonsterDeck.instance.activeDeck.Clear();
        MonsterHand.instance.cardsInHand.Clear();
    }

    public void doAttackStepOne() {
        CharacterInfo attacker = nonLocalPlayer.instance;
        CharacterInfo defender = localPlayer.instance;
        if (playerHasPriority) {
            attacker = localPlayer.instance;
            defender = nonLocalPlayer.instance;
        }
        if (!playerHasPriority) {
            MonsterHand.instance.PlayMonsterCard();
        }
        Attack(attacker, defender, "Impress");
        //Attack(defender, attacker, "Tailend Attacker");
        Invoke ("Cleanup", 2);
    }

    private void Cleanup()
    {
        
        WinCheck();
        StagingController.instance.cleanUp();
       
        HandChecks();
       
        DeckChecks();
       
        PreCombatSetup();
       
    }
    private void HandChecks()
    {
        if (monsterHand.cardsInHand.Count == 0)
        {
       
            MonsterDeck.instance.DrawCards(nonLocalPlayer.instance.handSize);
        }
        if (HandController.instance.cardsInHand.Count == 0)
        {
       
            DeckSystem.instance.DrawCards(localPlayer.instance.handSize);
        }
    }

    private void DeckChecks()
    {
        if (monsterHand.cardsInHand.Count == 0)
        {
       
            MonsterDeck.instance.activeDeck = DiscardSystem.instance.monsterDiscardPile;
         
        }
        if (HandController.instance.cardsInHand.Count == 0)
        {
       
            DeckSystem.instance.activeDeck = DiscardSystem.instance.discardPile;
        }
    }

    public void Attack(CharacterInfo attacker, CharacterInfo defender, string label) {
        SendMessagetoLog(" ");
        SendMessagetoLog(label);
        int playerResult = 0;
        int monsterResult = 0;
        if (StagingController.instance.cardsInStaging[0].cardInfo.fierceMod == 1)
        {
            playerResult = (Random.Range(1, 6)+hardmod);
        }
        if (StagingController.instance.monsterCardsInStaging[0].cardInfo.fierceMod == 1)
        {
            monsterResult = (Random.Range(1, 6));
        }
        if (monsterResult != 0 || playerResult != 0)
        {
            if (playerResult > monsterResult)
            {
                SendMessagetoLog(" You were very fierce.");
            }
            else if (playerResult < monsterResult)
            {
                SendMessagetoLog(" They were very fierce.");
            }

            else
            {
                SendMessagetoLog("  You were both fierce.");
            }
            TargetController.instance.changeFierceMeter(playerResult - monsterResult);
        }
        playerResult = 0;
        monsterResult = 0;
        if (StagingController.instance.cardsInStaging[0].cardInfo.cuteMod == 1)
        {
            playerResult = (Random.Range(1, 3));
        }
        if (StagingController.instance.monsterCardsInStaging[0].cardInfo.cuteMod == 1)
        {
            monsterResult = (Random.Range(1, 3));
        }
        if (monsterResult != 0 || playerResult != 0)
        {
            TargetController.instance.changeCuteMeter(playerResult - monsterResult);
            if (playerResult > monsterResult)
            {
                SendMessagetoLog(" You were so cute.");
            }
            else if (playerResult < monsterResult)
            {
                SendMessagetoLog(" They were so cute.");
            }

            else
            {
                SendMessagetoLog("  You were both cute.");
            }
        }
        playerResult = 0;
        monsterResult = 0;

        if (StagingController.instance.cardsInStaging[0].cardInfo.durpyMod == 1)
        {
            playerResult = (Random.Range(1, 3));
        }
        if (StagingController.instance.monsterCardsInStaging[0].cardInfo.durpyMod == 1)
        {
            monsterResult = (Random.Range(1, 3));
        }
        if (monsterResult != 0 || playerResult != 0)
        {
            TargetController.instance.changeDurpyMeter(playerResult - monsterResult);
            if (playerResult > monsterResult)
            {
                SendMessagetoLog(" You were durpy.");
            }
            else if (playerResult < monsterResult)
            {
                SendMessagetoLog(" They were durpy.");
            }

            else
            {
                SendMessagetoLog("  You were both durp.");
            }
        }
        playerResult = 0;
        monsterResult = 0;

        if (StagingController.instance.cardsInStaging[0].cardInfo.coolMod == 1)
        {
            playerResult = (Random.Range(1, 3));
        }
        if (StagingController.instance.monsterCardsInStaging[0].cardInfo.coolMod == 1)
        {
            monsterResult = (Random.Range(1, 3));
        }
        if (monsterResult != 0 || playerResult != 0)
        {
            TargetController.instance.changeCoolMeter(playerResult - monsterResult);
            if (playerResult > monsterResult)
            {
                SendMessagetoLog(" You played it cool.");
            }
            else if (playerResult < monsterResult)
            {
                SendMessagetoLog(" They played it cool.");
            }

            else
            {
                SendMessagetoLog("  You were both cool.");
            }
        }
        playerResult = 0;
        monsterResult = 0;
    }

    private void DoPriority(){
                  
        int foeRoll = Random.Range(0, 7) + nonLocalPlayer.instance.initiative;
        int playerRoll = Random.Range(0, 7) + localPlayer.instance.initiative;
        if( foeRoll== playerRoll){
            DoPriority();
        }
        else if(foeRoll < playerRoll ){
            //TODO fix the card bug so you can turn this back on
            playerHasPriority = false;

        }
        else{
            playerHasPriority = false;

        }
        PreCombatSetup();
        
    }
    private void PreCombatSetup()
    {
        if (playerHasPriority)
        {
            MonsterHand.instance.PlayMonsterCard();
        }
    }

    public void WinCheck()
    {
        string result = TargetController.instance.WinCheck();
        switch (result)
        {
        case "adopt":
            SceneManager.LoadScene("Win");
            break;
        case "foeAdopt":
            SendMessagetoLog(" They were adopted.");
            NukeOpponenetData();
            Generateopponent();
            hardmod++;
            break;
        case "no":
            break;
        default:
            break;
        }
    }
        
    public void SendMessagetoLog(string text)
    {
        if(messageList.Count >= maxMessages){
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
        Message newMessage = new Message();
        newMessage.text = text;

        GameObject newText = Instantiate(TextObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}
