using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectilController : MonoBehaviour
{
    [Header("Objetos internos")]
    private Animator _animator; // Parte de animacao
    private _WeaponController _weaponController;
    private SpriteRenderer _spriteRenderer;

    private string _spriteNameSelected=""; 

    // Start is called before the first frame update
    void Start()
    {
        _weaponController = FindObjectOfType(typeof (_WeaponController)) as _WeaponController;
        _spriteRenderer = FindObjectOfType(typeof (SpriteRenderer)) as SpriteRenderer;
        _animator = GetComponent<Animator>();
        LoadArrowAnimation(_weaponController.armaEquipada);

        Destroy(this.gameObject, _weaponController.armaEquipada.LifeTimeProjetil);
    }
    
    private void LateUpdate() 
    {
        LoadArrowAnimation(_weaponController.armaEquipada);
    }

    private void LoadArrowAnimation(Weapon weaponEqipada)
    {
        if (weaponEqipada is Bow)
        {
            var bowEquipado = weaponEqipada as Bow;
            if(!_spriteNameSelected.Equals(bowEquipado.ArrowSprite.name))
            {
                print($"LoadArrowAnimation: {bowEquipado.ArrowSprite.name}");
                _spriteNameSelected = bowEquipado.ArrowSprite.name;
                _spriteRenderer.sprite = bowEquipado.ArrowSprite;

            }
        }
    }
}
