using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjetilController : MonoBehaviour
{

    
    [Header("Objetos internos")]
    private _WeaponController _weaponController;
    private Animator _animator; // Parte de animacao


    // Start is called before the first frame update
    void Start()
    {
        _weaponController = FindObjectOfType(typeof (_WeaponController)) as _WeaponController;
        _animator = GetComponent<Animator>();
        ShotMagicProjetil(_weaponController.armaEquipada);
        StartCoroutine(TimerForDestroyMagicProjetil(_weaponController.armaEquipada));
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        switch(collider.tag){
            case "tagInimigo":
                DestroyMagicProjetil(_weaponController.armaEquipada);
            break;
            default:
            break;
        }
    }

    private void ShotMagicProjetil(Weapon weapon)
    {
        _animator.Play($"SHOT_{weapon.index}");
    }

    private void DestroyMagicProjetil(Weapon weapon)
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);// Stop Object
        _animator.Play($"DESTROY_{weapon.index}"); // Play animation for destroy
        Destroy (gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);  // then detroy it
    }

    IEnumerator TimerForDestroyMagicProjetil(Weapon weapon)
    {   
        yield return new WaitForSeconds(weapon.LifeTimeProjetil);
        DestroyMagicProjetil(weapon);
    }
}
