using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameController : MonoBehaviour{    
    
    [Header("Infos atuais do Player")]
    private _PlayerInfoController playerInfoController;

    [Header("Configuração de animações")]
    public GameObject fxMorte; // Animação de morte
    // private Dictionary<TypeDamage, GameObject> fxDano;// Animação de dano

    [Header("Armazenamento de Arnmas")]
    public Dictionary<Weapon.Index, Weapon> weapons = new Dictionary<Weapon.Index, Weapon>();

    [Header("Elementos UI")]
    public TextMeshProUGUI txtGold;

    [Header("Elementos Fade-In/Fade-Out")]
    public FadeScript fadeScript;

    // Start is called before the first frame update
    void Start(){
        this.fadeScript = FindObjectOfType(typeof(FadeScript)) as FadeScript;
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        this.InitItens();
        this.playerInfoController.RestaurarVida();
        DontDestroyOnLoad(this.gameObject); // impede de ser destruido
    }

    // Update is called once per frame
    void Update(){
        this.txtGold.text = this.playerInfoController.qntDinheiro.ToString("N0");
    }

    private void InitItens(){
        this.InitSwords();
    }

    private void InitSwords(){
        // Loading swords
        Sprite[] spriteSwords = Resources.LoadAll<Sprite>(Sword.RESOURCE_SPRINT_WEAPON);
        
        // Espada de normal
        string nameSword = "Espada de madeira";
        TypeDamage typeDamage = TypeDamage.NORMAL;
        int minDamage = 1;
        int maxDamage = 6;
        Sprite[] spriteEspada = new Sprite[]{spriteSwords[0], spriteSwords[1], spriteSwords[2]};
        this.weapons[Weapon.Index.SWORD_WOOD] = new Sword(nameSword, typeDamage, minDamage, maxDamage, spriteEspada);

        // Espada de aço
        nameSword = "Espada de aço";
        typeDamage = TypeDamage.NORMAL;
        minDamage = 4;
        maxDamage = 12;
        spriteEspada = new Sprite[]{spriteSwords[6], spriteSwords[7], spriteSwords[8]};
        this.weapons[Weapon.Index.SWORD_IRON] = new Sword(nameSword, typeDamage, minDamage, maxDamage, spriteEspada);

        // Espada de 
        nameSword = "Espada de Gelo";
        typeDamage = TypeDamage.ICE;
        minDamage = 15;
        maxDamage = 27;
        spriteEspada = new Sprite[]{spriteSwords[15], spriteSwords[16], spriteSwords[17]};
        this.weapons[Weapon.Index.SWORD_ICE] = new Sword(nameSword, typeDamage, minDamage, maxDamage, spriteEspada);
        
        // Espadão
        nameSword = "Espadão";
        typeDamage = TypeDamage.NORMAL;
        minDamage = 5;
        maxDamage = 40;
        spriteEspada = new Sprite[]{spriteSwords[18], spriteSwords[19], spriteSwords[20]};
        this.weapons[Weapon.Index.SWORD_BIG] = new Sword(nameSword, typeDamage, minDamage, maxDamage, spriteEspada);
        
        // Espada de 
        nameSword = "Espada de Demoniaca de fogo";
        typeDamage = TypeDamage.FIRE;
        minDamage = 22;
        maxDamage = 25;
        spriteEspada = new Sprite[]{spriteSwords[24], spriteSwords[25], spriteSwords[26]};
        this.weapons[Weapon.Index.SWORD_DEMON_FIRE] = new Sword(nameSword, typeDamage, minDamage, maxDamage, spriteEspada);

        // Espada de 
        nameSword = "Espada da luz, Ascalon";
        typeDamage = TypeDamage.HOLY;
        minDamage = 24;
        maxDamage = 24;
        spriteEspada = new Sprite[]{spriteSwords[27], spriteSwords[28], spriteSwords[29]};
        this.weapons[Weapon.Index.SWORD_ASCALON] = new Sword(nameSword, typeDamage, minDamage, maxDamage, spriteEspada);
        
    }
}
