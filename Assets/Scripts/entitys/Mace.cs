using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : Weapon {
    // Start is called before the first frame update
    public const string RESOURCE_SPRINT_WEAPON="Weapons/W_Maces";

    public Mace(Index index, string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites) : base(index, nameItem, typeDamage, minDamage, maxDamage, sprites) {
        TypeJobsAllowned = new TypeJob[]{TypeJob.CLERIC, TypeJob.BARBARIAN, TypeJob.KNIGHT, TypeJob.DWARF, TypeJob.WARRIOR};
    }

    public override TypeWeapon typeWeapon(){
        return  TypeWeapon.MACE;
    }
}
