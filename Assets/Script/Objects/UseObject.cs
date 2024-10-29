using UnityEngine;

public class UseObject : ObjectBase
{
    public string itemId;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Item") && collider.GetComponent<ItemBase>().itemId == itemId)
        {
            GameInstance.Signal("item.use", itemId);
            TriggerAction();
        }
    }

    protected override void OnMouseDown()
    {

    }

    protected virtual void TriggerAction()
    {
        var playerAction = GenerateAction();
        GameInstance.Signal("player.interact", playerAction);
        GameInstance.Signal("fire.make");
        
    }

    protected override void ExecuteAction()
    {
        transform.Find("_Muzhi").gameObject.SetActive(true);
    }
}
