using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour{

    // Variaveis componentes
    [Header("Objetos componentes")]
    private Animator playerAnimator; // Parte de animacao do personagem
    private Rigidbody2D playerRigidbody; // Parte física do personagem
    private Vector3 direcaoVisao; // Direcao de visao do personagem


    // Variaveis de fisica
    [Header("Variaveis de fisica")]
    private float eixoX, eixoY; // eixos

    // Paramêtros publicos
    public int idAnimation; // identificador da animação
    public Transform groundCheck; //Objeto que verifica colisao com o chao;
    public Transform hand; // Nossa mao
    public GameObject objetoInteracao;
    public Collider2D colliderStand, colliderCrounch; // Colisores em pé e abaixado
    public LayerMask whatIsGround; // Indica o que é superficie para o teste da layer Chao
    public LayerMask interacao; // Indica quais objetos sao interagiveis 


    [Header("Sitema de armas")]
    public GameObject[] weaponAnimations;

    public float speed; // Velocidade do personagem
    public float jumpForce; // Força do pulo do personagem
    public bool grounded; // Inicia se o pj está em alguma superfície 
    public bool attacking; // Indica se o pj está atacando
    public bool lookLeft; //Indica se o personagem tá virado para a esquerda
    
    // -----------------------------------------------
    // FUNÇÕES DO UNITY
    // -----------------------------------------------

    // Start is called before the first frame update
    void Start(){
        this.playerAnimator = GetComponent<Animator>();
        this.playerRigidbody = GetComponent<Rigidbody2D>();
        this.updateDirecaoVisao();
        this.resetGameObjects(this.weaponAnimations);
    }

    //Adicionando comentario teste GIT

    //Mesma coisa que o update, porém ele tem uma taxa de atualização fixa de 0.02s (taxa de atualização física)
    void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
        this.playerRigidbody.velocity = new Vector2(this.eixoX * this.speed, this.playerRigidbody.velocity.y);
        
        if(this.eixoY < 0 && this.grounded ){
            colliderCrounch.enabled = true;
            colliderStand.enabled = false;
        } else{ 
            colliderCrounch.enabled = false;
            colliderStand.enabled = true;
        }

        this.interagir();
    }

    // Update is called once per frame, taxa de atualização mais rápida
    void Update() {
        this.eixoX = Input.GetAxisRaw("Horizontal");
        this.eixoY = Input.GetAxisRaw("Vertical");

        //Flip do personagem
        if(this.eixoX > 0 && this.lookLeft){
            flipPersonagem();
        }else if(this.eixoX < 0 && !this.lookLeft){
            flipPersonagem();
        }

        // Configuracao de animacao
        if(this.eixoY < 0){
            this.idAnimation = 2; // Abaixado
            this.eixoX = this.grounded ? 0 : this.eixoX; //Bloqueia andar abaixado
        } else if(this.eixoX != 0) {
            this.idAnimation = 1; // Caminhando
        }else {
            this.idAnimation = 0; // Parado
        }


        if(Input.GetButtonDown("Fire1") && this.eixoY >= 0 && !this.attacking ){
            if(objetoInteracao == null){
                this.playerAnimator.SetTrigger("atack"); // Animação de ataque
            }else {
                objetoInteracao.SendMessage("interacao", SendMessageOptions.DontRequireReceiver);
            }
        }

        if(Input.GetButtonDown("Jump") && grounded && !this.attacking){
            this.playerRigidbody.AddForce(new Vector2(0, this.jumpForce)); // Animação de pulo
        }

        if(this.attacking && this.grounded){
            this.eixoX = 0;
        }

        // Atualiza os paramêtros para ativas as animações
        this.playerAnimator.SetBool("grounded", this.grounded);
        this.playerAnimator.SetInteger("idAnimation", this.idAnimation);
        this.playerAnimator.SetFloat("speedY", playerRigidbody.velocity.y);
    }

    // FUNÇÕES PARA TRATAR COLISOES

    // Ao entrar na colisão
    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("tagCaixa")) {
            // print("COLIDIU");
        }
    }


    // Durante a colisão
    private void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("tagCaixa")) {
            // print("AI MEU DEUS TÁ COLIDINDO");
        }
    }

    // Ao sair da colisão
    private void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.CompareTag("tagCaixa")) {
            // print("DESCOLIDIU");
        }
    }

    // FUNÇÕES PARA TRATAR Gatilhos

    // Ao entrar no Gatilho
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("tagCaixa")) {
            // print("DEU GATILHO");
        }
    }


    // Durante o Gatilho
    private void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.CompareTag("tagCaixa")) {
            // print("AI MEU DEUS TÁ DANDO GATILHO");
        }
    }

    // Ao sair do Gatilho
    private void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.CompareTag("tagCaixa")) {
            // print("SEM GATILHO");
        }
    }


    // -----------------------------------------------
    // FUNÇÕES DO PERSONALISADAS
    // -----------------------------------------------

    private void flipPersonagem(){
        if(!this.attacking){ // Nao girar personagem atacando
            this.lookLeft = !this.lookLeft;

            //inverter o sinal do scaleX
            this.transform.localScale = new Vector3(
                this.transform.localScale.x * -1, 
                this.transform.localScale.y,
                this.transform.localScale.z
            );

            updateDirecaoVisao();
        }
    }

    // Atualiza a visao do personagem
    private void updateDirecaoVisao (){
            this.direcaoVisao.x = this.transform.localScale.x;
    }

    private void interagir(){
        // Castando um Raio de HIT da posição da mão do personagem até a direita Vector3(1, 0, 0)
        RaycastHit2D rayCastHit = Physics2D.Raycast(this.hand.position, this.direcaoVisao,.1f, this.interacao);
        // Debug.DrawRay(this.hand.position, this.direcaoVisao * .1f, Color.red);

        if(rayCastHit) {
            objetoInteracao = rayCastHit.collider.gameObject;
        }else{
            objetoInteracao = null;
        }
    }

    public void setAttack(int attackingValue){
        this.attacking = attackingValue > 0;

        if(!this.attacking){
            this.weaponAnimations[2].SetActive(false);
        }
    }

    private void activeWeaponAnimation(int idWeaponAnimation){
        this.resetGameObjects(this.weaponAnimations);
        this.weaponAnimations[idWeaponAnimation].SetActive(true);
    }

    private void resetGameObjects(GameObject[] gameObjects){
        foreach (var gameObject in gameObjects){
            gameObject.SetActive(false);
        }
    }


}
