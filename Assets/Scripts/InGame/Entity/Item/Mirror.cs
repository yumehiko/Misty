using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Mirror : Item
{
    [SerializeField] private FaceDirection faceDirection = default;

    public override void PlaceOnGround(Inventory inventory)
    {
        gameObject.SetActive(true);
        transform.position = inventory.transform.position;

        FaceDirection faceDirection = inventory.GetComponent<FaceDirection>();
        this.faceDirection.TurnToDirection(faceDirection.Direction, 0.0f);
    }
}
