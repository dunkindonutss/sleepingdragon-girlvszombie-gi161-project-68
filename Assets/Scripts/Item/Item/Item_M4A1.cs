using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Item_M4A1 : Item,IPickupAble
{
    [SerializeField] private MMF_Player pickUpFeedBack;
    public void Pickup()
    {
        Inventory.Instance.AddWeapon(1);
        pickUpFeedBack.PlayFeedbacks();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }
}
