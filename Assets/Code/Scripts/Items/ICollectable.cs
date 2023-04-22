using UnityEngine;

public interface ICollectable
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Entity")
        {

        }
    }
}
