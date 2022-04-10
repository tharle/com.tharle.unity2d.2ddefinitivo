using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class _GameController : MonoBehaviour{    
    
    [Header("Infos atuais do Player")]
    private _PlayerInfoController playerInfoController;
    private _DataBaseController _dataBase;

    [Header("Configuração de animações")]
    public GameObject fxMorte; // Animação de morte

    [Header("Elementos UI")]
    public TextMeshProUGUI txtGold;

    [Header("Elementos Fade-In/Fade-Out")]
    public FadeScript fadeScript;

    // Start is called before the first frame update
    void Start(){
        this.fadeScript = FindObjectOfType(typeof(FadeScript)) as FadeScript;
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        this._dataBase = FindObjectOfType(typeof(_DataBaseController)) as _DataBaseController;
        this.playerInfoController.RestaurarVida();
        DontDestroyOnLoad(this.gameObject); // impede de ser destruido
    }

    // Update is called once per frame
    void Update(){
        this.txtGold.text = this.playerInfoController.qntDinheiro.ToString("N0");
    }

    void CarregarPersonagem(){
        Personagem personagem = _dataBase.BuscarPersonagem(playerInfoController.indexPersonagem);
        this.playerInfoController.CarregarPersonagemInfos(personagem);
    }

    /// <summary>
    /// Valida se a arma seleciona faz sentido com o TypeJob do personagem:
    /// Se sim: Não faz nada
    /// Se não: Troca pra arma WeaponStarter dp TypeJob
    /// </summary>
    public void ValidarPersonagemEArmaEquipada()
    {
        Personagem personagem = _dataBase.BuscarPersonagem(playerInfoController.indexPersonagem);
        Weapon armaEquipada = _dataBase.BuscarArma(this.playerInfoController.indexArma);

        print("VALIDANDO ARMA EQUIPADA");
        
        if(personagem == null) return;
        print($" Quantidade de jobos possiveis -> {armaEquipada?.TypeJobsAllowned.Length}");
        if(armaEquipada == null || !armaEquipada.IsAllowJobUseWeapon(personagem.Job))
        {
            print($"O personagem {personagem.Nome} não pode usar o item equipado {armaEquipada?.index}");
            print($"Mudando para {personagem.IndexWeaponStarter}");
            playerInfoController.indexArma = personagem.IndexWeaponStarter;
        }
    }
}
