using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {
    [Header("Objetos externos")]
    public GameObject painelFume; // Game object do painel que contem a imagem de fade

    [Header("Imagens e configurações de fade-in/fade-out")]
    public Image fume; // Objeto imagem 
    public Color[] corTransicao; // [0] cor opaca - [1] cor transparente
    public float step; // Quantidade de transparencia aplicada a cada frame

    private void Start() {
        this.fume.color = corTransicao[0];
    }


    /// <summary>
    /// Inicia evento de fade-in da tela
    /// </summary>
    /// <returns></returns>
    public void StartFadeIn() {
        StartCoroutine("FadeIn");
    }

    /// <summary>
    /// Inicia evento de fade-out da tela
    /// </summary>
    /// <returns></returns>
    public void StartFadeOut() {
        StartCoroutine("FadeOut");
    }

    
    /// <summary>
    /// A tela está completamente escura?
    /// </summary>
    /// <returns>true se sim, false se não</returns>
    public bool IsBlackout(){
        return this.fume.color.a <= 0.99;
    }

    /// <summary>
    /// A tela está completamente transparente?
    /// </summary>
    /// <returns>true se sim, false se não</returns>
    public bool IsTransparent(){
        return this.fume.color.a <= 0.01;
    }

    // Currotinas
    IEnumerator FadeIn() {
        this.painelFume.SetActive(true);
        for(float i = 0; i <= 1; i+=step) {
            this.fume.color = Color.Lerp(corTransicao[0], corTransicao[1], i);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(.5f); // Da um tempo entre fade In e o Fade out
    }

    IEnumerator FadeOut() {
        for(float i = 0; i <= 1; i+=step) {
            this.fume.color = Color.Lerp(corTransicao[1], corTransicao[0], i);
            yield return new WaitForEndOfFrame();
        }

        this.painelFume.SetActive(false);
    }

}
