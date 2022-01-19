using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon {
     // Start is called before the first frame update
    public const string RESOURCE_SPRINT_WEAPON="Weapons/W_Staffs";

    public Staff(string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites) : base(nameItem, typeDamage, minDamage, maxDamage, sprites) {
    }

    public override TypeWeapon typeWeapon(){
        return  TypeWeapon.STAFF;
    }
}