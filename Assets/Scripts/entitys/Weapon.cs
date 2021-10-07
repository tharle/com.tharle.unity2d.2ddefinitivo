using System.Collections;
using System.Collections.Generic;
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
    /// Tipo de item, que pode variar desde 
    /// <seealso cref="TypeWeapon.AXE">machados</seealso>,  
    /// <seealso cref="TypeWeapon.BOW"> arcos</seealso>,
    /// <seealso cref="TypeWeapon.MACE"> maças</seealso>
    /// e <seealso cref="TypeWeapon.SWORD"> espadas</seealso>.
    /// </summary>
    /// <returns name="TypeWeapon"> O tipo da arma</returns>
    public abstract TypeWeapon typeWeapon();

    public Weapon(string nameItem, TypeDamage typeDamage, int minDamage, int maxDamage, Sprite[] sprites) : base(nameItem) {
        this.typeDamage = typeDamage;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.sprites = sprites;
    }

    /// <summary>
    /// Tipo de item disponivel no sistema
    /// </summary>
    public enum TypeWeapon {
        AXE,
        BOW,
        MACE,
        SWORD
    }
    public enum Index {
        SWORD_WOOD,
        SWORD_IRON,
        SWORD_ICE,
        SWORD_BIG,
        SWORD_DEMON_FIRE,
        SWORD_ASCALON
    }
}
