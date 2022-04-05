using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjetilController : MonoBehaviour
{

    
    [Header("Objetos internos")]
    private _WeaponController _weaponController;
    private Animator _animator; // Parte de animacao do personagem


    // Start is called before the first frame update
    void Start()
    {
        _weaponController = FindObjectOfType(typeof (_WeaponController)) as _WeaponController;
        _animator = GetComponent<Animator>();
        var idAnimationProjectil = GetIdAnimationProjectil(_weaponController.armaEquipada);
        _animator.SetInteger("IdAnimationProjectil", idAnimationProjectil);
    }

    private int GetIdAnimationProjectil(Weapon weaponEquipada)
    {
        switch (weaponEquipada.index)
        {
            case Weapon.Index.STAFF_RUBY:
                return 1;
            case Weapon.Index.STAFF_SAFIRA:
                return 2;
            default:
                return 0;
        }
    }
}
