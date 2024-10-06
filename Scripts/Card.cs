using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    public CardInfo cardInfo;
    public Text nameText;
    public Text descriptionText;
    public Image artworkImage;
    public Text fierceMod;
    public Text cuteMod;
    public Text durpyMod;
    public Text coolMod;
    private bool selected;
    private Vector3 targetPoint;
    public float moveSpeed = 5f;
    private int homeIndex;
    public bool interacting = false;
    public bool isDragging = false;
    private bool isOverDropZone = false;
    private GameObject dropzone;
    public HandController hand;
    public StagingController staging;
    public int handPosition;
    private bool placed;
    private GameObject handParent;
    public DiscardSystem discard;
    public bool discarding = false;
    public bool monster = false;
    

    // Start is called before the first frame update
    void Start() {
        handParent = this.transform.parent.gameObject;
        nameText.text = cardInfo.name;
        descriptionText.text = cardInfo.description;
        artworkImage.sprite = cardInfo.artwork;
        fierceMod.text = cardInfo.fierceMod.ToString();
        cuteMod.text = cardInfo.cuteMod.ToString();
        durpyMod.text = cardInfo.durpyMod.ToString();
        coolMod.text = cardInfo.coolMod.ToString();
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
      Debug.Log("Collision");
      if(discarding && collision.gameObject.tag.Equals("Discard")){
            Debug.Log("monster check");
            Debug.Log(monster);
            if (!monster)
            {
                DiscardSystem.instance.addCardToDiscard(cardInfo);
                Destroy(this.gameObject);
            }
            else
            {
                DiscardSystem.instance.addCardToMonsterDiscard(cardInfo);
                Destroy(this.gameObject);
            }
        }
      if(isDragging && collision.gameObject.tag.Equals("Staging")){
        Debug.Log("Placing");
        if(collision.gameObject.GetComponent<StagingController>().cardsInStaging.Count == 0){
          MoveToPoint(transform.localScale -= new Vector3(.3F, .3f, .3f));
          isOverDropZone = true;
          dropzone = collision.gameObject;
        }
      }
    }
    private void OnCollisionExit2D(Collision2D collision){
      if(isDragging && collision.gameObject.tag.Equals("Staging")){
      MoveToPoint(transform.localScale += new Vector3(.3F, .3f, .3f));
      isOverDropZone = false;
      dropzone = null;
      }
    }
     void Update(){
      
        if (isDragging){
               targetPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
         };
         
        transform.position = Vector3.Lerp(
            transform.position, targetPoint, moveSpeed * Time.deltaTime
        );

     }
     public void MoveToPoint(Vector3 moveTargetPoint){
        targetPoint = moveTargetPoint;
     }
        
    public void OnMouseOver()
     
      {
        if(!monster){
          if(!placed){
          interacting = true;
          if (transform.localScale.y < 1.1f){
              MoveToPoint(transform.localScale += new Vector3(.3F, .3f, .3f)); 
              MoveToPoint(transform.position = new Vector3(transform.position.x, 132.5f, 0f));
              homeIndex = transform.GetSiblingIndex();
              transform.SetAsLastSibling();
          }
          }
      }
     }
 
    public void OnMouseExit()
     {
      if(!monster){
         interacting = false;
        if (transform.localScale.y > 1f){
        MoveToPoint(transform.localScale -= new Vector3(.3F, .3f, .3f)); 
        // MoveToPoint(transform.position -= new Vector3(0, 50, 0));
        transform.SetSiblingIndex(homeIndex);
        }
      }
     }
     public void OnMouseDown(){
      if(!monster){
        if (Input.GetKey(KeyCode.Mouse0)){
          if(!placed){
            isDragging = true;
            moveSpeed = 250f;
          }
        }
      }
        if (Input.GetKey(KeyCode.Mouse1)){
          if(monster){

          }
          else if(placed){
            placed = false;
            transform.SetParent(handParent.transform, false);
            StagingController.instance.removeCardFromStaging(this);
            HandController.instance.addCardToHand(this);
          }
        }
     }
     public void OnMouseUp(){
      if(!monster){
        if (Input.GetMouseButtonUp(0) && !placed){
          interacting = false;
          isDragging = false;
          moveSpeed = 7f;
          if (isOverDropZone){
              placed = true;
              transform.SetParent(dropzone.transform, false);
              MoveToPoint(transform.position = dropzone.transform.position);
              HandController.instance.removeCard(handPosition);
              StagingController.instance.addCardToStaging(this);
              
          }
        }
      }
     }
     public void GoToDiscard(){
        discarding = true;
        MoveToPoint(discard.transform.position);
     }
     public void ResetCard(){

        nameText.text = cardInfo.name;
        descriptionText.text = cardInfo.description;
        artworkImage.sprite = cardInfo.artwork;
        fierceMod.text = cardInfo.fierceMod.ToString();
        cuteMod.text = cardInfo.cuteMod.ToString();
        durpyMod.text = cardInfo.durpyMod.ToString();
        coolMod.text = cardInfo.coolMod.ToString();
    }

}
