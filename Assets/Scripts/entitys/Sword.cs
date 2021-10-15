using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    public const string RESOURCE_SPRINT_WEAPON="Weapons/W_Swords";
    
    // TODO Arrumar para perminir index
    public Sword(string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites) : base(nameItem, typeDamage, minDamage, maxDamage, sprites){
    }

    public override TypeWeapon typeWeapon() {
        return TypeWeapon.SWORD;
    }
}
