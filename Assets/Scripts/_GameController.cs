using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameController : MonoBehaviour{    
    [Header("Configuração de tipos de danos")]
    public string[] damageTypes;

    [Header("Configuração de animações")]
    public GameObject[] fxDano; // Animação de dano
    public GameObject fxMorte; // Animação de morte

    [Header("Armazenamento de itens")]
    public int qntDinheiro; // Armazena a quantidade de dinheiro

    [Header("Elementos UI")]
    public TextMeshProUGUI txtGold;

    [Header("Elementos Fade-In/Fade-Out")]
    public FadeScript fadeScript;

    // Start is called before the first frame update
    void Start(){
        this.fadeScript = FindObjectOfType(typeof(FadeScript)) as FadeScript;
        this.fadeScript.StartFadeOut();
    }

    // Update is called once per frame
    void Update(){
        txtGold.text = qntDinheiro.ToString("N0");
    }
}
