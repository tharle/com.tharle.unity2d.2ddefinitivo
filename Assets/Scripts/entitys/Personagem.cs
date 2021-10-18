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
    public string nome {
        get;
    }

    /// <summary>
    /// Vida maxima
    /// </summary>
    public int vidaMax {
        get;
    }
    
    /// <summary>
    /// <seealso cref="TypeJob">tipo de trabalho</seealso> do personagem.
    /// </summary>
    public TypeJob job {
        get;
    }
    
    /// <summary>
    /// Nome das sprites de animação.
    /// </summary>
    public string spritesName {
        get;
    }

    public Personagem(Index index, string nome, TypeJob job, int vidaMax, string spritesname){
        this.index = index;
        this.nome = nome;
        this.job = job;
        this.vidaMax = vidaMax;
        this.spritesName = spritesname;
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
