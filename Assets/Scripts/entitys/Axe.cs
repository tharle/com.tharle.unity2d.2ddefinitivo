using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon {
    // Start is called before the first frame update
    public const string RESOURCE_SPRINT_WEAPON="Weapons/W_Axes";

    public Axe(Index index, string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites, Sprite iconItemSprite) : base(index, nameItem, typeDamage, minDamage, maxDamage, sprites, iconItemSprite) {
        TypeJobsAllowned = new TypeJob[]{TypeJob.BARBARIAN, TypeJob.WARRIOR, TypeJob.DWARF};
    }

    public override TypeWeapon typeWeapon(){
        return  TypeWeapon.AXE;
    }
}
