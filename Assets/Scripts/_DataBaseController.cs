using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _DataBaseController: MonoBehaviour
{

    [Header("Armazenamento de elementos")]
    private Dictionary<Weapon.Index, Weapon> weapons = new Dictionary<Weapon.Index, Weapon>();
    private Dictionary<Personagem.Index, Personagem> personagens = new Dictionary<Personagem.Index, Personagem>();

    private void Start(){
        //Load Personagens
        this.InitJobs();
        
        //Load Weapons
        this.InitAxes();
        this.InitBows();
        this.InitMaces();
        this.InitStaffs();
        this.InitSwords();
        // DontDestroyOnLoad(this.gameObject); // impede de ser destruido
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
        Sprite arrowSprite = Resources.Load<Sprite>($"{Bow.RESOURCE_SPRINT_ARROW}/arrow");
        this.weapons[Weapon.Index.BOW_WOOD] = new Bow(Weapon.Index.BOW_WOOD, nameBow, typeDamage, minDamage, maxDamage, spriteArco, arrowSprite);

        // Arco de ouro
        nameBow = "Arco de prata";
        typeDamage = TypeDamage.HOLY;
        minDamage = 5;
        maxDamage = 40;
        spriteArco = new Sprite[]{spriteBows[3], spriteBows[4], spriteBows[5]};
        arrowSprite = Resources.Load<Sprite>($"{Bow.RESOURCE_SPRINT_ARROW}/arrow_silver");
        this.weapons[Weapon.Index.BOW_SILVER] = new Bow(Weapon.Index.BOW_SILVER, nameBow, typeDamage, minDamage, maxDamage, spriteArco, arrowSprite);

        // Arco de ouro
        nameBow = "Arco de ouro";
        typeDamage = TypeDamage.NORMAL;
        minDamage = 25;
        maxDamage = 25;
        spriteArco = new Sprite[]{spriteBows[6], spriteBows[7], spriteBows[8]};
        arrowSprite = Resources.Load<Sprite>($"{Bow.RESOURCE_SPRINT_ARROW}/arrow_gold");
        this.weapons[Weapon.Index.BOW_GOLD] = new Bow(Weapon.Index.BOW_GOLD, nameBow, typeDamage, minDamage, maxDamage, spriteArco, arrowSprite);
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
        float lifeTimeProjetil = 1.8f;
        Sprite[] spritesStaffs = new Sprite[]{allSprites[0], allSprites[1], allSprites[2], allSprites[3]};
        this.weapons[Weapon.Index.STAFF_ARCANE] = new Staff(Weapon.Index.STAFF_ARCANE, nameStaff, typeDamage, minDamage, maxDamage, spritesStaffs, lifeTimeProjetil);

        // Cajado safira
        nameStaff = "Cajado de safira";
        typeDamage = TypeDamage.ICE;
        minDamage = 18;
        maxDamage = 18;
        lifeTimeProjetil = 1.4f;
        spritesStaffs = new Sprite[]{allSprites[8], allSprites[9], allSprites[10], allSprites[11]};
        this.weapons[Weapon.Index.STAFF_SAFIRA] = new Staff(Weapon.Index.STAFF_SAFIRA, nameStaff, typeDamage, minDamage, maxDamage, spritesStaffs, lifeTimeProjetil);

        // Cajado rubi
        nameStaff = "Cajado de rubi";
        typeDamage = TypeDamage.FIRE;
        minDamage = 15;
        maxDamage = 40;
        spritesStaffs = new Sprite[]{allSprites[12], allSprites[13], allSprites[14], allSprites[15]};
        lifeTimeProjetil = .8f;
        this.weapons[Weapon.Index.STAFF_RUBY] = new Staff(Weapon.Index.STAFF_RUBY, nameStaff, typeDamage, minDamage, maxDamage, spritesStaffs, lifeTimeProjetil);
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
        Weapon.Index weaponStarter = Weapon.Index.MACE_WOOD;
        this.personagens[index] = new Personagem(index, name, job, 25, weaponStarter, spritesName);

        index = Personagem.Index.WAR_GEORGE_GARE;
        name = "George Gare";
        job = TypeJob.WARRIOR;
        weaponStarter = Weapon.Index.SWORD_WOOD;
        spritesName = "Soldier";
        this.personagens[index] = new Personagem(index, name, job, 20, weaponStarter, spritesName);

        index = Personagem.Index.ELF_CATARINA_ELDERT;
        name = "Catarina Eldert";
        job = TypeJob.ELF;
        weaponStarter = Weapon.Index.BOW_WOOD;
        spritesName = "Elf Female";
        this.personagens[index] = new Personagem(index, name, job, 10, weaponStarter,spritesName);

        index = Personagem.Index.ELF_ALTERIEL_ELDERT;
        name = "Alteriel Eldert";
        job = TypeJob.ELF;
        weaponStarter = Weapon.Index.BOW_WOOD;
        spritesName = "Elf Male";
        this.personagens[index] = new Personagem(index, name, job, 15, weaponStarter,spritesName);

        index = Personagem.Index.KNI_ROBERTO_ROBALDO;
        name = "Roberto Robaldo";
        job = TypeJob.KNIGHT;
        weaponStarter = Weapon.Index.SWORD_IRON;
        spritesName = "Knight";
        this.personagens[index] = new Personagem(index, name, job, 20, weaponStarter,spritesName);

        index = Personagem.Index.CLE_MARCELO_ROSSI;
        name = "Padre Marcelo Rossi";
        job = TypeJob.CLERIC;
        weaponStarter = Weapon.Index.MACE_WOOD;
        spritesName = "Peasant";
        this.personagens[index] = new Personagem(index, name, job, 15, weaponStarter,spritesName);

        index = Personagem.Index.WIZ_FLIX;
        name = "Flix, the wonder";
        job = TypeJob.WIZZARD;
        weaponStarter = Weapon.Index.STAFF_ARCANE;
        spritesName = "Wizard 1";
        this.personagens[index] = new Personagem(index, name, job, 5, weaponStarter,spritesName);

        index = Personagem.Index.DWF_TINHO_JADEARM;
        name = "Tihno Jadearm";
        job = TypeJob.DWARF;
        weaponStarter = Weapon.Index.AXE_WOOD;
        spritesName = "Dwarf 1";
        this.personagens[index] = new Personagem(index, name, job, 15, weaponStarter,spritesName);
    }

    public Personagem BuscarPersonagem(Personagem.Index indexPersonagem)
    {
        if(this.personagens.ContainsKey(indexPersonagem)){
            return this.personagens[indexPersonagem];
        } else {
            return this.personagens[Personagem.Index.BAR_BORI_BLUECLAWS]; // Carregar barbaro personagem by default
        }
    }

    public Weapon BuscarArma(Weapon.Index indexArma)
    {
        if(this.weapons.ContainsKey(indexArma)){
            return this.weapons[indexArma];
        } else {
            return null;
        }
    }
}
