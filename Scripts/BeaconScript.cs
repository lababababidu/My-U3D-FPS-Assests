using UnityEngine;

public class BeaconScript : MonoBehaviour
{
    public GameObject player; // 玩家角色
    public GameObject targetObject; // 目标GameObject
    public GameObject beaconEffect; // 光束效果的预制体
    public float distanceThreshold = 50.0f; // 距离阈值

    private GameObject currentBeacon; // 当前生成的光束

    void Update()
    {
        // 计算玩家和目标物体之间的距离
        float distance = Vector3.Distance(player.transform.position, targetObject.transform.position);
        // Debug.Log("current dist:"+distance);
        // 如果距离大于阈值且当前没有光束，则生成光束
        if (distance > distanceThreshold && currentBeacon == null)
        {
            GenerateBeacon();

        }
        // 如果距离小于或等于阈值且当前有光束，则移除光束
        else if (distance <= distanceThreshold && currentBeacon != null)
        {
            RemoveBeacon();
        }
    }

    void GenerateBeacon()
    {
        // 生成光束并设置其位置和旋转
        currentBeacon = Instantiate(beaconEffect, targetObject.transform.position, Quaternion.identity);
        // 调整光束的方向朝向天际
        currentBeacon.transform.rotation = Quaternion.Euler(0, 0, 0); // 朝上的方向
        GameObject player = GameObject.Find("Player");
        if(player!=null){
            PlayerWeaponManager playerWeaponManager = player.GetComponent<PlayerWeaponManager>();
            if(playerWeaponManager!=null){
                playerWeaponManager.FireAbility = false;
            }
        }
        
    }

    void RemoveBeacon()
    {
        // 移除光束
        Destroy(currentBeacon);
        GameObject player = GameObject.Find("Player");
        if(player!=null){
            PlayerWeaponManager playerWeaponManager = player.GetComponent<PlayerWeaponManager>();
            if(playerWeaponManager!=null){
                playerWeaponManager.FireAbility = true;
            }
        }
    }
}
