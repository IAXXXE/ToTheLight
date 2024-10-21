using UnityEngine;

public class UseGetObject : ObjectBase
{
    public string itemId;

    public GameObject getItem;
    public GameObject UiItem;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Item") && collider.GetComponent<ItemBase>().itemId == itemId)
        {
            GameInstance.Signal("item.use",itemId);
            TriggerAction();    
        }
    }

    protected virtual void TriggerAction()
    {
        if(!interactable) return;

        var playerAction = GenerateAction();
        GameInstance.Signal("player.interact", playerAction);

    }

    protected override void OnMouseDown()
    {
        
    }

    protected override void ExecuteAction()
    {
        base.ExecuteAction();
        sprite.SetActive(false);
        sprited.SetActive(true);
        interactable = false;

        GameInstance.Instance.player.GetItem(getItem);
        GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem)); 
    }
}
