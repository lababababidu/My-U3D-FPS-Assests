using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileBase : MonoBehaviour
{
    public GameObject Owner{get;private set;}
    public Vector3 InitialPosition{get;private set;}
    public Vector3 InitialDirection{get;private set;}

    public UnityAction OnShoot;

    public void Shoot(WeaponController controller){
        transform.SetParent(null);

        Owner = controller.owner;
        InitialPosition = transform.position;
        InitialDirection = transform.forward;

        if(OnShoot!=null){
            OnShoot.Invoke();
        }
    }

}
