using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCenaController : MonoBehaviour
{
    /// <summary>
    ///  <seealso cref="TypeScene"> Cenas </seealso> nas quais o jogador será transportado
    /// </summary>
    public TypeScene   cenaDestino;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Interacao(){
        SceneManager.LoadScene(cenaDestino.ToString());
    }
}
