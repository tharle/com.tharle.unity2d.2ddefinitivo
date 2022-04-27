using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponMenuScript : MonoBehaviour
{
    private Color _colorMinus = Color.red;
    private Color _colorPlus = Color.green;

    [Header("Objetos externos")]
    private _WeaponController _weaponController;
    private _PlayerInfoController _playerInfoController;
    private _GameController _gameController;

    [Header("Inforções da arma")]
    private Weapon _weaponSelected;
    private int posSlotSelected;
    public bool ForceReloadWeaponInfos;

    [Header("Objetos de texto e informações")]
    public TextMeshProUGUI TxtWeaponName;
    public TextMeshProUGUI TxtTypeDamage;
    public TextMeshProUGUI TxtDamageMin;
    public TextMeshProUGUI TxtDamageMax;
    public TextMeshProUGUI TxtDamageMinCompare;
    public TextMeshProUGUI TxtDamageMaxCompare;
    public Image ImgWeapon;

    [Header("Upgrade Bar")]
    public GameObject[] UpgradeBar;


    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        _weaponController = FindObjectOfType(typeof(_WeaponController)) as _WeaponController;
        _playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        ForceReloadWeaponInfos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(posSlotSelected != _gameController.PosSlotSelected || ForceReloadWeaponInfos)
        {
            posSlotSelected = _gameController.PosSlotSelected;
            var item = _playerInfoController.GetItemSlot(posSlotSelected);
            if(item != null && item is Weapon)
            {
                _weaponSelected = item as Weapon;
                UpdateInfosWeapon();
                UpdateInfoDamageWeapon();
                UpdateUpgradeBar();
            }
            ForceReloadWeaponInfos = false;
        }
    }

    private void UpdateInfosWeapon()
    {
        TxtWeaponName.text = _weaponSelected.NameItem;
        ImgWeapon.sprite = _weaponSelected.SpriteItem;
    }

    private void UpdateInfoDamageWeapon()
    {
        TxtTypeDamage.text = _weaponSelected.TypeDamageName;
        TxtDamageMin.text = _weaponSelected.minDamage.ToString();
        TxtDamageMax.text = _weaponSelected.maxDamage.ToString();

        Weapon weaponCurrent = _weaponController.armaEquipada;
        var weaponCurrentMinDamage = weaponCurrent != null ? weaponCurrent.minDamage : 0;
        var weaponCurrentMaxDamage = weaponCurrent != null ? weaponCurrent.maxDamage : 0;

        int damageMinCompare = _weaponSelected.minDamage - weaponCurrentMinDamage;
        string damageMinCompareString = damageMinCompare < 0? "(-" : "(+";
        damageMinCompareString += Mathf.Abs(damageMinCompare) < 10? "0" : "";
        damageMinCompareString += Mathf.Abs(damageMinCompare).ToString() + ")";
        TxtDamageMinCompare.text = damageMinCompareString;
        TxtDamageMinCompare.color = damageMinCompare > 0?  _colorPlus : _colorMinus;
        TxtDamageMinCompare.color = damageMinCompare == 0?  Color.gray : TxtDamageMinCompare.color;
        
        int damageMaxCompare = _weaponSelected.maxDamage - weaponCurrentMaxDamage;
        string damageMaxCompareString = damageMaxCompare < 0? "(-" : "(+";
        damageMaxCompareString += Mathf.Abs(damageMaxCompare) < 10? "0" : "";
        damageMaxCompareString += Mathf.Abs(damageMaxCompare).ToString() + ")";
        TxtDamageMaxCompare.text = damageMaxCompareString;
        TxtDamageMaxCompare.color = damageMaxCompare > 0?  _colorPlus : _colorMinus;
        TxtDamageMaxCompare.color = damageMaxCompare == 0?  Color.gray : TxtDamageMaxCompare.color;

    }

    private void UpdateUpgradeBar() {
        for(int i = 0; i < UpgradeBar.Length; i++){
            GameObject upgradeGO = UpgradeBar[i];
            upgradeGO.SetActive(i < _weaponSelected.UpgradeQnty);
        }
    }

    public void UpgradeWeapon()
    {
        print($"MELHORAR Arma [{_weaponSelected.index}]");
        // upgrade weapon by one
        _weaponSelected.UpgradeQnty++;
        ForceReloadWeaponInfos = true;
        
        // save in slot
        _playerInfoController.SetItemSlot(posSlotSelected, _weaponSelected);

        //TODO consume the itens
    }

    public void EquipWeapon()
    {
        _weaponController.ReloadFromSlotInventory = posSlotSelected;
        _gameController.CallCloseEvent();
    }

    public void RemoveWeapon()
    {
        print($"DESCARTAR Arma [{_weaponSelected.index}]");
        _playerInfoController.RemoveItem(posSlotSelected);
        _gameController.CallCloseEvent();
    }


}
