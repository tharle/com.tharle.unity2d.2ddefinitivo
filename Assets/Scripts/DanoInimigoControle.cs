using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoInimigoControle : MonoBehaviour{
    private _GameController gameController;
    // Start is called before the first frame update
    void Start(){
        this.gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
    }

    void OnTriggerEnter2D(Collider2D collider){
        switch(collider.tag){
            case "tagArma":
                WeaponInfo weaponInfo = collider.GetComponent<WeaponInfo>();

                if(weaponInfo && this.gameController){
                    int danoTomado = weaponInfo.damage;
                    string damageType = this.gameController.damageTypes[weaponInfo.damageType];

                    print("Tomei "+ danoTomado + " de dano do tipo "+damageType+".");
                }
            break;
            default:
            break;
        }
    }
}
