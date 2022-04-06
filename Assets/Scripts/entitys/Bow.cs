using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

    public const string RESOURCE_SPRINT_WEAPON="Weapons/W_Bows";
    public const string RESOURCE_SPRINT_ARROW="Weapons/Arrow";

    public Sprite ArrowSprite;

    public Bow(Index index, string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites, Sprite ArrowSprite) : base(index, nameItem, typeDamage, minDamage, maxDamage, sprites) {
        this.ArrowSprite = ArrowSprite;
        this.LifeTimeProjetil = 2.5f;
    }

    public override TypeWeapon typeWeapon(){
        return  TypeWeapon.BOW;
    }
}
