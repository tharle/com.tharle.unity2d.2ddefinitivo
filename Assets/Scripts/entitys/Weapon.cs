using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Weapon : Item {

    /// <summary>
    /// <seealso cref="Index">Index</seealso> da arma.
    /// </summary>
    public Index index{
        get;
    }

    /// <summary>
    /// Tipo de dano vinculado a arma, variando de:  normal, corte, perfuração até danos magicos como: fogo, gelo, escuridão etc.
    /// </summary>
    public TypeDamage typeDamage {
        get;
    }

    /// <summary>
    /// Dano máximo que a arma irá causar
    /// </summary>
    public int maxDamage {
        get;
    }

    /// <summary>
    /// Dano minimo que a arma irá causar
    /// </summary>
    public int minDamage {
        get;
    }

    /// <summary>
    /// Animação da arma (normalmente são 3 quadros)
    /// </summary>
    /// <value>Sprite</value>
    public Sprite[] sprites{
        get;
    }

    /// <summary>
    /// Tempo de duracao do objeto em segundos do projetil (quando existir)
    /// </summary>
    public float LifeTimeProjetil = 60f;

    public float IdAnimationWeapon {
       get {
            switch (typeWeapon())
            {
                case TypeWeapon.BOW:
                    return 1;
                case TypeWeapon.STAFF:
                    return 2;
                default:
                    return 0;
            }
       }
    }

    public TypeJob[] TypeJobsAllowned = new TypeJob[]{};

    /// <summary>
    /// Tipo de item, que pode variar desde 
    /// <seealso cref="TypeWeapon.AXE">machados</seealso>,  
    /// <seealso cref="TypeWeapon.BOW"> arcos</seealso>,
    /// <seealso cref="TypeWeapon.MACE"> maças</seealso>
    /// e <seealso cref="TypeWeapon.SWORD"> espadas</seealso>.
    /// </summary>
    /// <returns name="TypeWeapon"> O tipo da arma</returns>
    public abstract TypeWeapon typeWeapon();

    public Weapon(Index index, string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites) : base(nameItem) 
    {
        this.index = index;
        this.typeDamage = typeDamage;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.sprites = sprites;
    }

    public bool IsAllowJobUseWeapon(TypeJob job){

        if(TypeJobsAllowned.Length <= 0) return true;

        return Array.Exists<TypeJob>(TypeJobsAllowned, typeJob => typeJob == job);
    }

    /// <summary>
    /// Tipo de item disponivel no sistema
    /// </summary>
    public enum TypeWeapon {
        AXE,
        BOW,
        MACE,
        STAFF,
        SWORD
    }
    public enum Index {
        _NONE_,
        AXE_WOOD,
        AXE_DEMON,
        AXE_GOLD,
        BOW_WOOD,
        BOW_SILVER,
        BOW_GOLD,
        MACE_WOOD,
        MACE_GOLD,
        MACE_ESMERALD,
        STAFF_ARCANE,
        STAFF_SAFIRA,
        STAFF_RUBY,
        SWORD_WOOD,
        SWORD_IRON,
        SWORD_ICE,
        SWORD_BIG,
        SWORD_DEMON_FIRE,
        SWORD_ASCALON
    }
}
