using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject owner{get;set;}
    public float MaxLifeTime = 5f;
    public float Speed = 0.1f;

    private bool hasHit = false;
    private ProjectileBase _projectileBase;
    private Vector3 _velocity ;
    private Rigidbody _rigidbody;

    private void OnEnable(){
        

        _projectileBase = GetComponent<ProjectileBase>();
        _rigidbody = GetComponent<Rigidbody>();
        _projectileBase.OnShoot += onShoot;
        Destroy(gameObject,MaxLifeTime);
    }

    public void onShoot(){
        Debug.Log("arrow onShoot");
        _velocity += transform.forward * Speed;
        if (_rigidbody != null)
        {
            // 设置Rigidbody的速度
            _rigidbody.velocity = _velocity;
            // GetComponents<AudioSource>()[1].Play();
        }
        else
        {
            Debug.LogError("未找到Rigidbody组件");
        }
    }

    private void Update(){
        if(hasHit){
            GameObject player = GameObject.Find("Player");
            float distance = Vector3.Distance(player.transform.position, transform.position);
            // Debug.Log("dist to arrow:" + distance);
            if(distance<2){
                GetComponents<AudioSource>()[1].Play();
                gameObject.SetActive(false);
                Destroy(gameObject);
                PlayerWeaponManager weaponManager = player.GetComponent<PlayerWeaponManager>();
                WeaponController weaponController = weaponManager.GetCurrentWeapon();
                if(weaponController!=null){
                    weaponController.ammunition += 1;
                }
                Debug.Log("ADD ammunition");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 获取碰撞对象的信息
        GameObject other = collision.gameObject;
        Debug.Log("Collided with " + other.name);

        if (!hasHit)
        {
            GetComponents<AudioSource>()[0].Play();
            hasHit = true;

            // 停止箭的运动
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            GetComponent<BoxCollider>().enabled = false;
            _rigidbody.isKinematic = true;

            // 获取碰撞点
            ContactPoint contact = collision.contacts[0];
            Vector3 hitPoint = contact.point;
            Vector3 hitNormal = contact.normal;

            // 计算箭的长度
            float arrowLength = GetComponent<Renderer>().bounds.extents.z * 2; // 需要 *2，获取箭的完整长度

            // 计算箭头的头部位置（箭的尾部在原点，因此将碰撞点向后移动箭长的一半）
            Vector3 arrowHeadPosition = hitPoint - hitNormal * (-arrowLength *3/ 4);

            // 将箭的头部设置到碰撞点
            transform.position = arrowHeadPosition;

            // 使箭指向碰撞法线（箭头朝向碰撞物体）
            transform.rotation = Quaternion.LookRotation(-hitNormal);

            // 将箭设置为碰撞物体的子物体
            transform.SetParent(collision.transform);
        }
    }

}
