using UnityEngine;

public interface IHittable{
    public void ReceiveHit(RaycastHit2D hit);
}