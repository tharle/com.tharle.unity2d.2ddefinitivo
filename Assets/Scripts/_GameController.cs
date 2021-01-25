using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameController : MonoBehaviour{
    [Header("Debug")]
    public int teste; // Variavel teste nao apagar denovo
    
    [Header("Configuração de tipos de danos")]
    public string[] damageTypes;

    [Header("Configuração de animações")]
    public GameObject[] fxDano; // Animação de dano
    public GameObject fxMorte; // Animação de morte

    // Start is called before the first frame update
    void Start(){
        this.teste = 0;
    }

    // Update is called once per frame
    void Update(){
        
    }
}
