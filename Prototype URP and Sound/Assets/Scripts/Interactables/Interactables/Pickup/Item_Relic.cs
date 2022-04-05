using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item_Relic : PickUp
{
    #region Fields
    public Item itemData;
    public int ID; // unique identifier for each item
    [SerializeField]
    [Range(0,1f)]
    public float max_area;
    [SerializeField]
    [Range(0, 3f)]
    public float old_duration;
    [SerializeField]
    [Range(0, 3f)]
    public float new_duration;
    private float dirt_area;
    private float dirt_intensity;
    private Tween turn_new;
    private Tween turn_old;
    #endregion

    void Start()
    {
        base.Start();
        turn_old = DOVirtual.Float(0f, max_area, old_duration, (x) => {
            dirt_area = x;
            foreach (Material m in materials)
            {
                m.SetFloat("_DirtArea", dirt_area);
            }
        }).SetEase(Ease.OutQuart).Pause().SetAutoKill(false);
        turn_new = DOVirtual.Float(max_area, 0, new_duration, (x) => {
            dirt_area = x;
            foreach (Material m in materials)
            {
                m.SetFloat("_DirtArea", dirt_area);
            }
        }).OnComplete(()=> { turn_old.Restart(); }).SetEase(Ease.OutQuart).Pause().SetAutoKill(false);
    }

    #region Helper Methods
    /// <summary>
    /// Put it in the bag.
    /// </summary>
    override public void Interaction()
    {
        base.Interaction();
        // Add the item to the inventory
        CharacterComponents.instance.controller.GetComponent<CollectableInventory>().GiveItem(ID);
    }

    public override void Use()
    {
        if (!usable)
            return;
        if(turn_new.IsPlaying() == false && turn_old.IsPlaying() == false)
        {
            turn_new.Restart();
        }
    }
    #endregion
}
