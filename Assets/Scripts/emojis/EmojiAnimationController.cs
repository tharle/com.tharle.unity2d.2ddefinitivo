using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiAnimationController  : MonoBehaviour {
    [Header("Objetos componentes")]
    private Animator emojiAnimator; // Parte de animacao do personagem
    
    [Header("Caracteristicas")]
    public bool show;
    public string tipoEmoji;

    void Start() {
        this.emojiAnimator = GetComponent<Animator>();
        show = true;
    }

    void Update() {
        this.emojiAnimator.SetBool("show", this.show);
    }

    public void FinalizarEmoji () {
        this.show = false;
        print("Set on destroy no Objeto Emoji " + tipoEmoji + "!");
        Destroy(this.gameObject, .6f);
    }
}