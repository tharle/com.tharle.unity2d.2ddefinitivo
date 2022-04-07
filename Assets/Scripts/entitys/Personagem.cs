using System.Collections;
using System.Collections.Generic;

public class Personagem 
{

    /// <summary>
    /// <seealso cref="Index">Index</seealso> do pesonagem.
    /// </summary>
    public Index index {
        get;
    }

    /// <summary>
    /// Nome do personagem
    /// </summary>
    public string Nome {
        get;
    }

    /// <summary>
    /// Vida maxima
    /// </summary>
    public int VidaMax {
        get;
    }
    
    /// <summary>
    /// <seealso cref="TypeJob">tipo de trabalho</seealso> do personagem.
    /// </summary>
    public TypeJob Job {
        get;
    }
    
    /// <summary>
    /// Nome das sprites de animação.
    /// </summary>
    public string SpritesName {
        get;
    }

    public Weapon.Index IndexWeaponStarter {
        get;
    }

    public Personagem(Index index, string nome, TypeJob job, int vidaMax, Weapon.Index weaponStarter, string spritesname){
        this.index = index;
        this.Nome = nome;
        this.Job = job;
        this.VidaMax = vidaMax;
        this.IndexWeaponStarter = weaponStarter;
        this.SpritesName = spritesname;
    }

    public enum Index {
        BAR_BORI_BLUECLAWS,
        CLE_MARCELO_ROSSI,
        DWF_TINHO_JADEARM,
        ELF_ALTERIEL_ELDERT,
        ELF_CATARINA_ELDERT,
        KNI_ROBERTO_ROBALDO,
        WAR_GEORGE_GARE,
        WIZ_FLIX
        
        
    }
}
