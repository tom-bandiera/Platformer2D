using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitbox : MonoBehaviour
{
    [SerializeField] private PoisonSlime poisonSlime;
    [SerializeField] private HurtState hurtState;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.transform.parent.gameObject.name == "Player")
        {
            poisonSlime.takeDamage(1);
        }
    }
}
