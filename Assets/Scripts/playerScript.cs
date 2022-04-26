using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour{

    // Variaveis componentes
    [Header("Scripts externos")]
    private _GameController gameController;
    private _EmojiController emojiController;
    private _PlayerInfoController playerInfoController;
    private _WeaponController weaponController;

    [Header("Objetos componentes")]
    private Animator playerAnimator; // Parte de animacao do personagem
    private Rigidbody2D playerRigidbody; // Parte física do personagem
    private Vector3 direcaoVisao; // Direcao de visao do personagem
    private SpriteRenderer playerSpriteRenderer; // Sprites do personagem
    


    // Variaveis de fisica
    [Header("Variaveis de fisica")]
    private float eixoX, eixoY; // eixos

    // Paramêtros publicos
    public int idAnimation; // identificador da animação
    public Transform groundCheck; //Objeto que verifica colisao com o chao;
    public Transform hand; // Nossa mao
    public GameObject objetoInteracao; // Objeto que está interajindo (normalmente é verificado via triggers e camadas)
    public Collider2D colliderStand, colliderCrounch; // Colisores em pé e abaixado
    public LayerMask whatIsGround; // Indica o que é superficie para o teste da layer Chao
    public LayerMask interacao; // Indica quais objetos sao interagiveis 


    [Header("Informacoes atuais do player")]

    public float speed; // Velocidade do personagem
    public float jumpForce; // Força do pulo do personagem
    public bool grounded; // Inicia se o pj está em alguma superfície 
    public bool attacking; // Indica se o pj está atacando
    public bool lookLeft; //Indica se o personagem tá virado para a esquerda

    [Header("Controlle emoji")]

    public bool exibirEmoji;

    private GameObject emoji;
    
    // -----------------------------------------------
    // FUNÇÕES DO UNITY
    // -----------------------------------------------

    // Start is called before the first frame update
    void Start(){
        this.gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        this.emojiController = FindObjectOfType(typeof(_EmojiController)) as _EmojiController;
        this.playerInfoController = FindObjectOfType(typeof(_PlayerInfoController)) as _PlayerInfoController;
        this.weaponController = FindObjectOfType(typeof(_WeaponController)) as _WeaponController;

        this.playerAnimator = GetComponent<Animator>();
        this.playerRigidbody = GetComponent<Rigidbody2D>();
        this.playerSpriteRenderer = GetComponent<SpriteRenderer>();

        this.UpdateDirecaoVisao();
        this.exibirEmoji = false;
    }

    //Adicionando comentario teste GIT

    //Mesma coisa que o update, porém ele tem uma taxa de atualização fixa de 0.02s (taxa de atualização física)
    void FixedUpdate() {  
        if(gameController.CurrentGameState != GameState.GAME_PLAY) return;

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);
        this.playerRigidbody.velocity = new Vector2(this.eixoX * this.speed, this.playerRigidbody.velocity.y);
        
        if(this.eixoY < 0 && this.grounded ){
            colliderCrounch.enabled = true;
            colliderStand.enabled = false;
        } else{ 
            colliderCrounch.enabled = false;
            colliderStand.enabled = true;
        }

        this.Interagir();
    }

    // Update is called once per frame, taxa de atualização mais rápida
    void Update() {
        if(gameController.CurrentGameState != GameState.GAME_PLAY) return;

        this.eixoX = Input.GetAxisRaw("Horizontal");
        this.eixoY = Input.GetAxisRaw("Vertical");

        //Flip do personagem
        if(this.eixoX > 0 && this.lookLeft){
            FlipPersonagem();
        }else if(this.eixoX < 0 && !this.lookLeft){
            FlipPersonagem();
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

        this.Interacao();

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

        //Animação da arma
        this.playerAnimator.SetFloat("IdAnimationWeapon", this.weaponController.GetIdAnimationWeapon());
        
        
    }

    // FUNÇÕES PARA TRATAR COLISOES

    // Ao entrar na colisão
    // private void OnCollisionEnter2D(Collision2D col) {
    //     // evento de colisao
    // }


    // // Durante a colisão
    // private void OnCollisionStay2D(Collision2D col) {
    //     if(col.gameObject.CompareTag("tagCaixa")) {
    //         // print("AI MEU DEUS TÁ COLIDINDO");
    //     }
    // }

    // // Ao sair da colisão
    // private void OnCollisionExit2D(Collision2D col) {
    //     if(col.gameObject.CompareTag("tagCaixa")) {
    //         // print("DESCOLIDIU");
    //     }
    // }

    // FUNÇÕES PARA TRATAR Gatilhos

    // Ao entrar no Gatilho
    private void OnTriggerEnter2D(Collider2D col) {
        switch(col.gameObject.tag){
            case "tagColetavel": // Verifica se colide com uma moeda
                col.gameObject.SendMessage("Coletar", SendMessageOptions.DontRequireReceiver); // Esse segundo parametro evita qualquer erro caso o objeto em questão nao tenha a função
                break;
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

    /// <summary>
    ///  Função para mudar o material do personagem e da arma. 
    ///  Usado para saber se o material do personagem vai ser sensivel a luz ou não. 
    ///  Nesse exemplo ele é usado ao entrar e sair em uma caverna.
    /// </summary>
    /// <param name="novoMaterial"></param>
    public void SetMaterial(Material novoMaterial){
        this.playerSpriteRenderer.material = novoMaterial; // Muda material do pernsonagem
        // Muda o material de todas os efeitos de armas
        this.weaponController.SetMaterial(novoMaterial);
    }

    /// <summary>
    /// Seta evento para "atacando"
    /// </summary>
    /// <param name="attackingValue">Valor do ataque</param>
    public void setAttack(int attackingValue){ // TODO mexer nisso depois nas animações para corrigir o nome
        this.attacking = attackingValue > 0;

        if(!this.attacking){
             this.weaponController.ResetWeaponsAnimationsForEqupedWeapon();
        }
    }

    /// <summary>
    /// Seta a arma que será equipada
    /// </summary>
    /// <param name="indexArma"> <seealso cref="Weapon.Index"/> da arma</param>
    public void EquiparArma(Weapon.Index indexArma){
        this.playerInfoController.SelectWeapon(indexArma);
    }

    // -----------------------------------------------
    // FUNÇÕES PRIVADAS
    // -----------------------------------------------
    private void Interacao(){
         if(Input.GetButtonDown("Fire1") && this.eixoY >= 0 && !this.attacking ){
            if(objetoInteracao == null){
                this.playerAnimator.SetTrigger("atack"); // Animação de ataque
            }else {
                objetoInteracao.SendMessage("Interacao", SendMessageOptions.DontRequireReceiver);
            }
        }

        this.ControlarEmojiPlayer(objetoInteracao != null);
    }

    private void ControlarEmojiPlayer(bool exibirEmoji) {
        //Configuracao de balao
        if(exibirEmoji && this.emoji == null) {
            this.emoji = Instantiate(this.emojiController.emojiAlert, this.transform, false);
        } else if(!exibirEmoji && this.emoji != null) {
            this.emoji.SendMessage("FinalizarEmoji", SendMessageOptions.DontRequireReceiver);
            this.emoji = null; // Perder a referencia
        }
    }


    private void FlipPersonagem(){
        if(!this.attacking){ // Nao girar personagem atacando
            this.lookLeft = !this.lookLeft;

            //inverter o sinal do scaleX
            this.transform.localScale = new Vector3(
                this.transform.localScale.x * -1, 
                this.transform.localScale.y,
                this.transform.localScale.z
            );

            UpdateDirecaoVisao();
        }
    }

    // Atualiza a visao do personagem
    private void UpdateDirecaoVisao (){
            this.direcaoVisao.x = this.transform.localScale.x;
    }

    private void Interagir(){
        // Castando um Raio de HIT da posição da mão do personagem até a direita Vector3(1, 0, 0)
        RaycastHit2D rayCastHit = Physics2D.Raycast(this.hand.position, this.direcaoVisao,.1f, this.interacao);
        // Debug.DrawRay(this.hand.position, this.direcaoVisao * .1f, Color.red);

        if(rayCastHit) {
            objetoInteracao = rayCastHit.collider.gameObject;
        }else{
            objetoInteracao = null;
        }
    }

    public void RestAnimationPlayer()
    {
        playerAnimator.Play("Idle");
        this.attacking = false;
    }
    

}
