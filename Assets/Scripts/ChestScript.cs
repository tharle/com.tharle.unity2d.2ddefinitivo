using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
 
    [Header("Animação")]
    public Sprite[] sprites;
    public bool open;

    [Header("Configuração de loot")]
    public GameObject[] loots;
    private bool empty;
    void Start(){
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.empty = false;
    }

    void Update(){

        if(this.empty && this.open) { // Nao tem  interação
            GetComponent<Collider2D>().enabled = false; // desliga a colisão
        }
        
    }

    public void Interacao(){
        if(this.open && !this.empty){ // Bau aberto?
            this.spriteRenderer.sprite = sprites[0]; // Fecha ele
        }else if(!this.empty){
            this.spriteRenderer.sprite = sprites[1]; // Abre ele
            StartCoroutine("Loot");
        }

        this.open = !this.open; // Seta a variavel o contrario do estado que estava
    }

    IEnumerator Loot(){
        this.empty = true;
        // -------------------------------------------------
        // Gestão de loot 
        // -------------------------------------------------
        int qntMoedasTotal = 0;
        if(loots != null && loots.Length > 0){
            foreach (GameObject loot in this.loots) {
                int quantidadeLoot= Random.Range(1, 10); // Multiplicador de moedas
                do{
                    GameObject tempLoot = Instantiate(loot, transform.position, transform.localRotation);
                    tempLoot.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10,10) * 10, 200)); // Animação de moeda saltando
                    yield return new WaitForSeconds(.1f); // Da um tempinho de uma antes de começar a outra
                    qntMoedasTotal++;
                } while(--quantidadeLoot > 0);
            }
        }

    }
}
