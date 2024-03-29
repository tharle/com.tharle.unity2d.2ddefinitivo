using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    public const string RESOURCE_SPRINT_WEAPON="Weapons/W_Swords";
    
    // TODO Arrumar para perminir index
    public Sword(Index index, string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites) : base(index, nameItem, typeDamage, minDamage, maxDamage, sprites){
        TypeJobsAllowned = new TypeJob[]{TypeJob.KNIGHT, TypeJob.WARRIOR};
    }

    public override TypeWeapon typeWeapon() {
        return TypeWeapon.SWORD;
    }
}
