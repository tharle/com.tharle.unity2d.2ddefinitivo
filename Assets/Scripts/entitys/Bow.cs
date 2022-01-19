using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

    public const string RESOURCE_SPRINT_WEAPON="Weapons/W_Bows";

    public Bow(string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites) : base(nameItem, typeDamage, minDamage, maxDamage, sprites) {
    }

    public override TypeWeapon typeWeapon(){
        return  TypeWeapon.BOW;
    }
}
