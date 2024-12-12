using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<WeaponController> StartingWeapons = new List<WeaponController>();
    public Transform WeaponParentSocket;
    public UnityAction<WeaponController> onSwitchedToWeapon;
    private WeaponController[] _weaponSlots = new WeaponController[9];
    private WeaponController _currentWeaponController;
    private PlayerInputHandler _playerInputHandler;
    public bool FireAbility = false;



    private void Start(){

        _playerInputHandler = GetComponent<PlayerInputHandler>();

        onSwitchedToWeapon += OnWeaponSwitched;

        foreach (WeaponController weapon in StartingWeapons){
            AddWeapon(weapon);
        }

        SwitchWeaponToIndex(0);
    }

    private void OnWeaponSwitched(WeaponController newWeaponsController){
        if(newWeaponsController != null){
            newWeaponsController.ShowWeapon(true);
            _currentWeaponController = newWeaponsController;
        }
    }

    public bool AddWeapon(WeaponController weaponPrefab){
        for (int i=0;i<_weaponSlots.Length;i++){
            if(_weaponSlots[i] != null){
                continue;
            }
            WeaponController newWeaponController = Instantiate(weaponPrefab,WeaponParentSocket);
            newWeaponController.transform.localPosition = Vector3.zero;
            newWeaponController.transform.localRotation = Quaternion.identity;

            newWeaponController.owner = gameObject;
            newWeaponController.sourcePrefab = weaponPrefab.gameObject;
            newWeaponController.ShowWeapon(false);

            _weaponSlots[i] = newWeaponController;
            return true;
        }
        return false;
    }

    public void SwitchWeaponToIndex(int index){
        WeaponController weaponToSwitch = null;
        if(index>=0&&index<9){
            weaponToSwitch = _weaponSlots[index];
        }

        if(onSwitchedToWeapon != null){
            onSwitchedToWeapon.Invoke(weaponToSwitch); 
        }
    }

    private void Update(){
        if(!FireAbility){
            return;
        }
        if(_playerInputHandler.GetReloadInput()){
            _currentWeaponController.Reload();
        }

        if(_playerInputHandler.GetFireInput()){
            _currentWeaponController.TryFire();
        }else{
            _currentWeaponController.Idle();
        }   
    }

    public WeaponController GetCurrentWeapon(){
        return _currentWeaponController;
    }
}
