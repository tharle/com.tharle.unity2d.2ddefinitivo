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
    private _DataBaseController _dataBaseController;
    private _WeaponController _weaponController;

    [Header("Inforções da arma")]
    public Weapon.Index WeaponIndex;
    private Weapon _weapon;

    [Header("Objetos de texto e informações")]
    public TextMeshProUGUI TxtWeaponName;
    public TextMeshProUGUI TxtTypeDamage;
    public TextMeshProUGUI TxtDamageMin;
    public TextMeshProUGUI TxtDamageMax;
    public TextMeshProUGUI TxtDamageMinCompare;
    public TextMeshProUGUI TxtDamageMaxCompare;
    public Image ImgWeapon;


    // Start is called before the first frame update
    void Start()
    {
        _dataBaseController = FindObjectOfType(typeof(_DataBaseController)) as _DataBaseController;
        _weaponController = FindObjectOfType(typeof(_WeaponController)) as _WeaponController;
    }

    // Update is called once per frame
    void Update()
    {
        if((_weapon == null) ||  (WeaponIndex != _weapon.index))
        {
            _weapon = _dataBaseController.BuscarArma(WeaponIndex);
            if(_weapon != null )
            {
                LoadInfosWeapon();
                LoadInfoDamageWeapon();
            }
        }
    }

    private void LoadInfosWeapon()
    {
        TxtWeaponName.text = _weapon.NameItem;
        ImgWeapon.sprite = _weapon.SpriteItem;
    }

    private void LoadInfoDamageWeapon()
    {
        TxtTypeDamage.text = _weapon.TypeDamageName;
        TxtDamageMin.text = _weapon.minDamage.ToString();
        TxtDamageMax.text = _weapon.maxDamage.ToString();

        Weapon weaponCurrent = _weaponController.armaEquipada;
        var weaponCurrentMinDamage = weaponCurrent != null ? weaponCurrent.minDamage : 0;
        var weaponCurrentMaxDamage = weaponCurrent != null ? weaponCurrent.maxDamage : 0;

        int damageMinCompare = _weapon.minDamage - weaponCurrentMinDamage;
        string damageMinCompareString = damageMinCompare < 0? "(-" : "(+";
        damageMinCompareString += Mathf.Abs(damageMinCompare) < 10? "0" : "";
        damageMinCompareString += Mathf.Abs(damageMinCompare).ToString() + ")";
        TxtDamageMinCompare.text = damageMinCompareString;
        TxtDamageMinCompare.color = damageMinCompare > 0?  _colorPlus : _colorMinus;
        TxtDamageMinCompare.color = damageMinCompare == 0?  Color.gray : TxtDamageMinCompare.color;
        
        int damageMaxCompare = _weapon.maxDamage - weaponCurrentMaxDamage;
        string damageMaxCompareString = damageMaxCompare < 0? "(-" : "(+";
        damageMaxCompareString += Mathf.Abs(damageMaxCompare) < 10? "0" : "";
        damageMaxCompareString += Mathf.Abs(damageMaxCompare).ToString() + ")";
        TxtDamageMaxCompare.text = damageMaxCompareString;
        TxtDamageMaxCompare.color = damageMaxCompare > 0?  _colorPlus : _colorMinus;
        TxtDamageMaxCompare.color = damageMaxCompare == 0?  Color.gray : TxtDamageMaxCompare.color;

        

    }

    public void UpgradeWeapon()
    {
        print($"MELHORAR Arma [{_weapon.index}]");
    }

    public void EquipWeapon()
    {
        print($"EQUIPAR Arma [{_weapon.index}]");
    }

    public void RemoveWeapon()
    {
        print($"DESCARTAR Arma [{_weapon.index}]");
    }


}
