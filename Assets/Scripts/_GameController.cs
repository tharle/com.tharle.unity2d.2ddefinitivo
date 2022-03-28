using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class _GameController : MonoBehaviour{    
    
    [Header("Infos atuais do Player")]
    private _PlayerInfoController playerInfoController;

    [Header("Configuração de animações")]
    public GameObject fxMorte; // Animação de morte
    // private Dictionary<TypeDamage, GameObject> fxDano;// Animação de dano

    [Header("Elementos UI")]
    public TextMeshProUGUI txtGold;

    [Header("Elementos Fade-In/Fade-Out")]
    public FadeScript fadeScript;
    
    [Header("Armazenamento de elementos")]
    public Dictionary<Weapon.Index, Weapon> weapons = new Dictionary<Weapon.Index, Weapon>();
    public Dictionary<Personagem.Index, Personagem> personagens = new Dictionary<Personagem.Index, Personagem>();

    // Start is called before the first frame update
    void Start(){
        this.fadeScript = FindObjectOfType(typeof(FadeScript)) as FadeScript;
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        this.InitItens();
        this.InitJobs();
        CarregarPersonagem();
        this.playerInfoController.RestaurarVida();
        DontDestroyOnLoad(this.gameObject); // impede de ser destruido
    }

    void CarregarPersonagem(){
        Personagem personagem = BuscarPersonagem(playerInfoController.indexPersonagem);
        this.playerInfoController.CarregarPersonagem(personagem);
    }

    // Update is called once per frame
    void Update(){
        this.txtGold.text = this.playerInfoController.qntDinheiro.ToString("N0");
    }

    private void InitItens(){
        this.InitAxes();
        this.InitBows();
        this.InitMaces();
        this.InitStaffs();
        this.InitSwords();
    }

    private void InitAxes(){
        // Loading Axe
        // TODO corrigir isso para nao fazer carregar as sprites de inicio
        Sprite[] allSprites = Resources.LoadAll<Sprite>(Axe.RESOURCE_SPRINT_WEAPON);
        
        // Machado de madeira
        string nameAxe = "Machado de madeira";
        TypeDamage typeDamage = TypeDamage.NORMAL;
        int minDamage = 4;
        int maxDamage = 16;
        Sprite[] spritesAxe = new Sprite[]{allSprites[0], allSprites[1], allSprites[2]};
        this.weapons[Weapon.Index.AXE_WOOD] = new Axe(Weapon.Index.AXE_WOOD, nameAxe, typeDamage, minDamage, maxDamage, spritesAxe);

        // Machado de ouro
        nameAxe = "Machado Dourado";
        typeDamage = TypeDamage.HOLY;
        minDamage = 22;
        maxDamage = 22;
        spritesAxe = new Sprite[]{allSprites[12], allSprites[13], allSprites[14]};
        this.weapons[Weapon.Index.AXE_GOLD] = new Axe(Weapon.Index.AXE_GOLD, nameAxe, typeDamage, minDamage, maxDamage, spritesAxe);

        // Machado demoniaco
        nameAxe = "Machado Demoniaco";
        typeDamage = TypeDamage.FIRE;
        minDamage = 10;
        maxDamage = 35;
        spritesAxe = new Sprite[]{allSprites[6], allSprites[7], allSprites[8]};
        this.weapons[Weapon.Index.AXE_DEMON] = new Axe(Weapon.Index.AXE_DEMON, nameAxe, typeDamage, minDamage, maxDamage, spritesAxe);
    }

    private void InitBows(){
        // Loading Bows
        // TODO corrigir isso para nao fazer carregar as sprites de inicio
        Sprite[] spriteBows = Resources.LoadAll<Sprite>(Bow.RESOURCE_SPRINT_WEAPON);
        
        // Arco de madeira
        string nameBow = "Arco de madeira";
        TypeDamage typeDamage = TypeDamage.NORMAL;
        int minDamage = 6;
        int maxDamage = 14;
        Sprite[] spriteArco = new Sprite[]{spriteBows[0], spriteBows[1], spriteBows[2]};
        this.weapons[Weapon.Index.BOW_WOOD] = new Bow(Weapon.Index.BOW_WOOD, nameBow, typeDamage, minDamage, maxDamage, spriteArco);

        // Arco de ouro
        nameBow = "Arco de prata";
        typeDamage = TypeDamage.HOLY;
        minDamage = 5;
        maxDamage = 40;
        spriteArco = new Sprite[]{spriteBows[3], spriteBows[4], spriteBows[5]};
        this.weapons[Weapon.Index.BOW_SILVER] = new Bow(Weapon.Index.BOW_SILVER, nameBow, typeDamage, minDamage, maxDamage, spriteArco);

        // Arco de ouro
        nameBow = "Arco de ouro";
        typeDamage = TypeDamage.NORMAL;
        minDamage = 25;
        maxDamage = 25;
        spriteArco = new Sprite[]{spriteBows[6], spriteBows[7], spriteBows[8]};
        this.weapons[Weapon.Index.BOW_GOLD] = new Bow(Weapon.Index.BOW_GOLD, nameBow, typeDamage, minDamage, maxDamage, spriteArco);
    }

    
    private void InitMaces(){
        // Loading Maças
        // TODO corrigir isso para nao fazer carregar as sprites de inicio
        Sprite[] allSprites = Resources.LoadAll<Sprite>(Mace.RESOURCE_SPRINT_WEAPON);
        
        // Machado de madeira
        string nameMace = "Maça de madeira";
        TypeDamage typeDamage = TypeDamage.HOLY;
        int minDamage = 3;
        int maxDamage = 10;
        Sprite[] spritesMaces = new Sprite[]{allSprites[0], allSprites[1], allSprites[2]};
        this.weapons[Weapon.Index.MACE_WOOD] = new Mace(Weapon.Index.MACE_WOOD, nameMace, typeDamage, minDamage, maxDamage, spritesMaces);

        // Maça dourada
        nameMace = "Maça Dourado";
        typeDamage = TypeDamage.HOLY;
        minDamage = 18;
        maxDamage = 18;
        spritesMaces = new Sprite[]{allSprites[9], allSprites[10], allSprites[11]};
        this.weapons[Weapon.Index.MACE_GOLD] = new Mace(Weapon.Index.MACE_GOLD, nameMace, typeDamage, minDamage, maxDamage, spritesMaces);

        // Maça esmeralda
        nameMace = "Maça esmeralda";
        typeDamage = TypeDamage.HOLY;
        minDamage = 15;
        maxDamage = 40;
        spritesMaces = new Sprite[]{allSprites[12], allSprites[13], allSprites[14]};
        this.weapons[Weapon.Index.MACE_ESMERALD] = new Mace(Weapon.Index.MACE_ESMERALD, nameMace, typeDamage, minDamage, maxDamage, spritesMaces);
    }
    private void InitStaffs(){
        // Loading Cajados
        // TODO corrigir isso para nao fazer carregar as sprites de inicio
        Sprite[] allSprites = Resources.LoadAll<Sprite>(Staff.RESOURCE_SPRINT_WEAPON);
        
        // Cajado de madeira
        string nameStaff = "Cajado quartzo";
        TypeDamage typeDamage = TypeDamage.ARCANE;
        int minDamage = 3;
        int maxDamage = 10;
        Sprite[] spritesMaces = new Sprite[]{allSprites[0], allSprites[1], allSprites[2], allSprites[3]};
        this.weapons[Weapon.Index.STAFF_ARCANE] = new Staff(Weapon.Index.STAFF_ARCANE, nameStaff, typeDamage, minDamage, maxDamage, spritesMaces);

        // Cajado safira
        nameStaff = "Cajado de safira";
        typeDamage = TypeDamage.ICE;
        minDamage = 18;
        maxDamage = 18;
        spritesMaces = new Sprite[]{allSprites[8], allSprites[9], allSprites[10], allSprites[11]};
        this.weapons[Weapon.Index.STAFF_SAFIRA] = new Staff(Weapon.Index.STAFF_SAFIRA, nameStaff, typeDamage, minDamage, maxDamage, spritesMaces);

        // Cajado rubi
        nameStaff = "Cajado de rubi";
        typeDamage = TypeDamage.FIRE;
        minDamage = 15;
        maxDamage = 40;
        spritesMaces = new Sprite[]{allSprites[12], allSprites[13], allSprites[14], allSprites[15]};
        this.weapons[Weapon.Index.STAFF_RUBY] = new Staff(Weapon.Index.STAFF_RUBY, nameStaff, typeDamage, minDamage, maxDamage, spritesMaces);
    }

    private void InitSwords(){
        // Loading swords
        // TODO corrigir isso para nao fazer carregar as sprites de inicio
        Sprite[] spriteSwords = Resources.LoadAll<Sprite>(Sword.RESOURCE_SPRINT_WEAPON);
        
        // Espada de normal
        string nameSword = "Espada de madeira";
        TypeDamage typeDamage = TypeDamage.NORMAL;
        int minDamage = 1;
        int maxDamage = 6;
        Sprite[] spriteEspada = new Sprite[]{spriteSwords[0], spriteSwords[1], spriteSwords[2]};
        this.weapons[Weapon.Index.SWORD_WOOD] = new Sword(Weapon.Index.SWORD_WOOD, nameSword, typeDamage, minDamage, maxDamage, spriteEspada);

        // Espada de aço
        nameSword = "Espada de aço";
        typeDamage = TypeDamage.NORMAL;
        minDamage = 4;
        maxDamage = 12;
        spriteEspada = new Sprite[]{spriteSwords[6], spriteSwords[7], spriteSwords[8]};
        this.weapons[Weapon.Index.SWORD_IRON] = new Sword(Weapon.Index.SWORD_IRON, nameSword, typeDamage, minDamage, maxDamage, spriteEspada);

        // Espada de 
        nameSword = "Espada de Gelo";
        typeDamage = TypeDamage.ICE;
        minDamage = 15;
        maxDamage = 27;
        spriteEspada = new Sprite[]{spriteSwords[12], spriteSwords[13], spriteSwords[14]};
        this.weapons[Weapon.Index.SWORD_ICE] = new Sword(Weapon.Index.SWORD_ICE, nameSword, typeDamage, minDamage, maxDamage, spriteEspada);
        
        // Espadão
        nameSword = "Espadão";
        typeDamage = TypeDamage.NORMAL;
        minDamage = 5;
        maxDamage = 40;
        spriteEspada = new Sprite[]{spriteSwords[18], spriteSwords[19], spriteSwords[20]};
        this.weapons[Weapon.Index.SWORD_BIG] = new Sword(Weapon.Index.SWORD_BIG, nameSword, typeDamage, minDamage, maxDamage, spriteEspada);
        
        // Espada de 
        nameSword = "Espada de Demoniaca de fogo";
        typeDamage = TypeDamage.FIRE;
        minDamage = 22;
        maxDamage = 25;
        spriteEspada = new Sprite[]{spriteSwords[24], spriteSwords[25], spriteSwords[26]};
        this.weapons[Weapon.Index.SWORD_DEMON_FIRE] = new Sword(Weapon.Index.SWORD_DEMON_FIRE, nameSword, typeDamage, minDamage, maxDamage, spriteEspada);

        // Espada de 
        nameSword = "Espada da luz, Ascalon";
        typeDamage = TypeDamage.HOLY;
        minDamage = 24;
        maxDamage = 24;
        spriteEspada = new Sprite[]{spriteSwords[27], spriteSwords[28], spriteSwords[29]};
        this.weapons[Weapon.Index.SWORD_ASCALON] = new Sword(Weapon.Index.SWORD_ASCALON, nameSword, typeDamage, minDamage, maxDamage, spriteEspada);
        
    }

    private void InitJobs(){
        // falta fazer iniciar o banco de jobs

        Personagem.Index index = Personagem.Index.BAR_BORI_BLUECLAWS;
        string name = "Bori Blueclaws";
        TypeJob job = TypeJob.BARBARIAN;
        string spritesName = "Barbarian1";
        this.personagens[index] = new Personagem(index, name, job, 25, spritesName);

        index = Personagem.Index.WAR_GEORGE_GARE;
        name = "George Gare";
        job = TypeJob.WARRIOR;
        spritesName = "Soldier";
        this.personagens[index] = new Personagem(index, name, job, 20,spritesName);

        index = Personagem.Index.ELF_CATARINA_ELDERT;
        name = "Catarina Eldert";
        job = TypeJob.ELF;
        spritesName = "Elf Female";
        this.personagens[index] = new Personagem(index, name, job, 10,spritesName);

        index = Personagem.Index.ELF_ALTERIEL_ELDERT;
        name = "Alteriel Eldert";
        job = TypeJob.ELF;
        spritesName = "Elf Male";
        this.personagens[index] = new Personagem(index, name, job, 15,spritesName);

        index = Personagem.Index.KNI_ROBERTO_ROBALDO;
        name = "Roberto Robaldo";
        job = TypeJob.KNIGHT;
        spritesName = "Knight";
        this.personagens[index] = new Personagem(index, name, job, 20,spritesName);

        index = Personagem.Index.CLE_MARCELO_ROSSI;
        name = "Padre Marcelo Rossi";
        job = TypeJob.CLERIC;
        spritesName = "Peasant";
        this.personagens[index] = new Personagem(index, name, job, 15,spritesName);

        index = Personagem.Index.WIZ_FLIX;
        name = "Flix, the wonder";
        job = TypeJob.WIZZARD;
        spritesName = "Wizard 1";
        this.personagens[index] = new Personagem(index, name, job, 5,spritesName);

        index = Personagem.Index.DWF_TINHO_JADEARM;
        name = "Tihno Jadearm";
        job = TypeJob.DWARF;
        spritesName = "Dwarf 1";
        this.personagens[index] = new Personagem(index, name, job, 15,spritesName);
    }

    /// <summary>
    /// Retorna o <seealso cref="Personagem"/> seletionado.
    /// </summary>
    /// <returns><seealso cref="Personagem"/></returns>
    public Personagem BuscarPersonagem(Personagem.Index indexPersonagem)
    {
        if(this.personagens.ContainsKey(indexPersonagem)){
            return this.personagens[indexPersonagem];
        } else {
            return null;
        }
    }
}
