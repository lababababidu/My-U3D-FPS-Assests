using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //common
    public GameObject WeaponRoot;
    public bool isWeaponActive{get;private set;}
    public GameObject owner{get;set;}
    public GameObject sourcePrefab{get;set;}
    
    public int ammunition = 10;
    public int ammo = 1;
    public ProjectileBase Projectileprefab;
    private Animator weaponAnimator;
    private bool Tag = false;
    //bow 
    private float holdingStartTime = 0f;
    private float holdingEfficient = 4f;
    private void Start(){
        weaponAnimator = GetComponent<Animator>();

        //

        Transform childTransform = transform.Find("箭"); // 查找子对象
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(false); 
        }
    }

    public void ShowWeapon(bool show){
        WeaponRoot.SetActive(show);
        isWeaponActive = show;
    }

    public void TryFire(){
        AnimatorStateInfo weaponStateInfo = weaponAnimator.GetCurrentAnimatorStateInfo(0);
        
        if(weaponStateInfo.IsName("Empty")){
            if(ammunition == 0){
                return;
            }

            weaponAnimator.SetBool("Reload",true);
            weaponAnimator.SetBool("Fire",false);
            if(!Tag){
                ammunition -= 1;
                Tag = true;
            }
            
            Transform childTransform = transform.Find("箭"); // 查找子对象
            if (childTransform != null)
            {
                childTransform.gameObject.SetActive(true); 
            }

            if(holdingStartTime == 0){
                holdingStartTime = Time.time;

            }
            weaponAnimator.SetFloat("Blend", holdingEfficient * (Time.time - holdingStartTime));
            if(holdingEfficient * (Time.time - holdingStartTime) > 1f){
                weaponAnimator.SetBool("Filled",true);
            }
            ammo = 1;
        }

        else if(weaponStateInfo.IsName("Hold")){
            fire();
        }
        else if(weaponStateInfo.IsName("Half Filled")){
            fire();
        }
        
    }

    private void fire(){
        if(ammo <=0){
            return ;
        }
        bool isReload = weaponAnimator.GetBool("Reload");
        if(isReload){
            return ;
        }
        Tag = false;
        ammo -= 1;
        weaponAnimator.Play("Shoot");
        weaponAnimator.SetBool("Fire",true);
        weaponAnimator.SetBool("Holding",false);
        GetComponent<AudioSource>().Play();
        holdingStartTime = 0f;
        Invoke("hideArrow", 0.3f);

        Vector3 shotDirection = transform.forward;
        ProjectileBase arrow = Instantiate(Projectileprefab,transform.position,transform.rotation,transform);
        arrow.Shoot(this);
    }

    public void Idle(){
        // Debug.Log("Idle");
        
        AnimatorStateInfo weaponStateInfo = weaponAnimator.GetCurrentAnimatorStateInfo(0);
        // Debug.Log(weaponStateInfo);
        if(weaponStateInfo.IsName("Fill")){
            weaponAnimator.SetBool("Fire",false);
            weaponAnimator.SetBool("Reload",false);
            weaponAnimator.SetBool("Holding",false);
            holdingStartTime = 0f;
        }
        else if(weaponStateInfo.IsName("Empty")){
            weaponAnimator.SetBool("Fire",false);
            // weaponAnimator.SetBool("Filled",false);
            weaponAnimator.SetBool("Holding",false);
            holdingStartTime = 0f;
            // hideArrow();
        }
        else if(weaponStateInfo.IsName("Hold")){
            weaponAnimator.SetBool("Fire",false);
            weaponAnimator.SetBool("Filled",false);
            weaponAnimator.SetBool("Reload",false);
            weaponAnimator.SetBool("Holding",true);
            // ammo = 1;
            holdingStartTime = 0f;
        }
        else if(weaponStateInfo.IsName("Half Filled")){
            Debug.Log("Idle HF");
            weaponAnimator.SetBool("Fire",false);
            weaponAnimator.SetBool("Filled",false);
            weaponAnimator.SetBool("Reload",false);
            weaponAnimator.SetBool("Holding",true);
            // ammo = 1;
            holdingStartTime = 0f;
        }
        
    }

    public void Reload(){

        bool isReload = weaponAnimator.GetBool("Reload");
        if(isReload){
            return ;
        }
        if(ammunition == 0){
            return;
        }
        ammunition -= 1;
        AnimatorStateInfo weaponStateInfo = weaponAnimator.GetCurrentAnimatorStateInfo(0);
        if(weaponStateInfo.IsName("Empty")){
            weaponAnimator.SetBool("Reload",true);
            weaponAnimator.SetBool("Filled",true);
            weaponAnimator.SetBool("Fire",false);
            ammo = 1;
            Transform childTransform = transform.Find("箭"); // 查找子对象
            if (childTransform != null)
            {
                childTransform.gameObject.SetActive(true); // 隐藏子对象
            }
        }
    }

    private void hideArrow(){
        Transform childTransform = transform.Find("箭"); // 查找子对象
        if (childTransform != null)
        {
            childTransform.gameObject.SetActive(false); // 隐藏子对象
        }
    }
}
