using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon {
     // Start is called before the first frame update
    public const string RESOURCE_SPRINT_WEAPON="Weapons/W_Staffs";
    public const string RESOURCE_SPRINT_MAGIC="Weapons/Wizard Attack VFX";

    public Staff(Index index, string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites, Sprite iconItemSprite, float LifeTimeProjetil) : base(index, nameItem, typeDamage, minDamage, maxDamage, sprites, iconItemSprite) {
        this.LifeTimeProjetil = LifeTimeProjetil;
        TypeJobsAllowned = new TypeJob[]{TypeJob.WIZZARD};
    }

    public override TypeWeapon typeWeapon(){
        return  TypeWeapon.STAFF;
    }
}
