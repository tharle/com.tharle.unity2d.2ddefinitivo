using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DanoInimigoControle : MonoBehaviour{
    [Header("Game ontroles")]
    private _GameController gameController;
    private Vector3 direcaoVisao; // Direcao de visao do inimigo
    private SpriteRenderer spriteRenderer; //Sprite do personagem
    private PlayerScript playerScript; // Script do personagem principal
    // private Animator animator; // Controller de animação 

    [Header("Configuração de resistências/fraquesas")]
    public float[] ajusteDano; // sistema de resistencia/fraquesa por tipo de dano

    [Header("Knockback")]
    public GameObject knockBackForce; //Força de repulsao
    public Transform knockBackPosition;

    
    [Header("Variaveis de fisica")]
    private float eixoX, eixoY; // eixos
    
    [Header("Combate")]
    public bool attacking;
    public bool lookLeft;
    public Color[] personagemCor; // Controle de cor do personagem


    //Controle de Vida. É setado publicamente
    [Header("Configuração de Vida")]
    public float pontosVidaInimigoMax;
    private float pontosVidaInimigoAtual;
    private bool tomandoDano; // Indica se o personagem tomou um Hit
    public GameObject barrasVida; // Todas as Barras de vida do personagem
    public Transform barraVida; // Objeto indicador da quantidade de vida
    public GameObject txtDano; // OBjeto que ira exibir o valor do dano tomado

    [Header("Animação")]
    public int idAnimation; // identificador da animação
    private Animator inimigoAnimator; // Parte de animacao do personagem

    [Header("Configuração de chão")]
    public Transform groundCheck; // Verifica se o inimigo está pisando no chao
    public LayerMask whaIsGround; // Coleção de camadas que identifica que é chão ou não.
    
    private void Start(){
        this.gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        this.playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        this.attacking = false;
        this.pontosVidaInimigoAtual = this.pontosVidaInimigoMax;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.color = this.personagemCor[0];
        this.barrasVida.SetActive(false);// Quando o personagem entra na cena, a barra ainda nao é exibida
        this.barraVida.localScale = new Vector3(1, 1, 1); // Reseta a Barra de vida
        this.txtDano.GetComponentInChildren<MeshRenderer>().sortingLayerName = "HUD";
        this.inimigoAnimator = GetComponent<Animator>();
        this.inimigoAnimator.SetInteger("idAnimation", idAnimation); // inicia personagem na animação idle
        // knockBackPosition.localPosition = new Vector3(konckBackX, knockBackPosition.localPosition .y, knockBackPosition.localPosition.z);
    }

    private void Update() {
        this.inimigoAnimator.SetBool("grounded", true);
    }

    void FixedUpdate() {
        verifyKnockBackPosition();
        verifyFlipPersonagem();
        VerifyBarraVidaPersonagem();
    }

    void VerifyBarraVidaPersonagem(){
        float percentualVidaPersonagem = this.pontosVidaInimigoAtual / this.pontosVidaInimigoMax;
        this.barraVida.localScale = new Vector3(percentualVidaPersonagem, this.barraVida.localScale.y, this.barraVida.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(this.isDead() ) {
            return;
        }

        switch(collider.tag){
            case "tagArma":
                WeaponInfo weaponInfo = collider.GetComponent<WeaponInfo>();

                inimigoAnimator.SetTrigger("hit");

                if(weaponInfo && this.gameController){
                    string damageType = this.gameController.damageTypes[weaponInfo.damageType];
                    float danoTotalArma = Mathf.Round(Random.Range(weaponInfo.damageMin, weaponInfo.damageMax));
                    print(" DANO DA ARMA RANDOM : "+ danoTotalArma);

                    // AnimacaoDano(collider); // Animação de dano normal
                    AnimacaoDano(collider);

                    danoTotalArma *= this.ajusteDano.Length > weaponInfo.damageType ? this.ajusteDano[weaponInfo.damageType] : 1;
                    print("Inimigo tomou "+ danoTotalArma + " de dano do tipo "+damageType+".");

                    DanoVida(danoTotalArma);
                }
            break;
            default:
            break;
        }
    }

    /// <summary>
    /// Método que instancia a animação da arma.
    /// </summary>
    /// <param name="collider">Colisor2D vindo do OnTrigger</param>
    private void AnimacaoDano(Collider2D collider){
        WeaponInfo weaponInfo = collider.GetComponent<WeaponInfo>();
        GameObject fxTemp = Instantiate(gameController.fxDano[weaponInfo.damageType], transform.position, transform.localRotation);
        Destroy(fxTemp, 1);
    }

    /// <summary>
    /// Método que subitrai da vida maxima do personagem.
    /// </summary>
    /// <param name="danoRecebido"></param>
    private void DanoVida(float danoRecebido)
    {
        if (!this.tomandoDano) 
        {
            this.pontosVidaInimigoAtual = Mathf.Round(this.pontosVidaInimigoAtual - danoRecebido);
            
            // Exibir texto de valor ao receber dano.
            GameObject txtDanoTemp = Instantiate(this.txtDano);
            //Reinicialliza a posicao do texto de dano em resposta a localizacao atual do personagem
            txtDanoTemp.GetComponentInChildren<RectTransform>().transform.localPosition = new Vector3(this.transform.localPosition.x - 0.3f, this.transform.localPosition.y + 0.5f, this.transform.localPosition.z);
            txtDanoTemp.GetComponentInChildren<TextMeshPro>().text = danoRecebido.ToString();

            int direcaoVisao = this.isPlayerLeft() ? 1 : -1;
            txtDanoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(50 * direcaoVisao, 150));
            Destroy(txtDanoTemp, 0.5f);

            if(isDead()) {
                Morreu();
            } else {
                StartCoroutine("TomandoDanoCorouTine");
                print("Restam " +this.pontosVidaInimigoAtual+ " ponto(s) de vida do Inimigo.");
                this.KnockBackPeronagem();
            }
        }
        
    }

    private void KnockBackPeronagem() {
        //Clona o objeto inicial e retorna
        //Nesse lógica aqui, é instanciado um objeto com força magnética contrária (repulsão)
        //Assim, tudo que é "RigedBody" é expelido
        GameObject temp = Instantiate(this.knockBackForce, this.knockBackPosition.position, this.knockBackPosition.localRotation);

        //Depois, é destruido o elemento para não ficar gerando repulsao.
        Destroy(temp, 0.02f);
    }

    /// <summary>
    ///  Método que recupera os pontos de vida peridos do personagem
    /// </summary>
    /// <param name="danoRecebido"> Quantidade pontos de vidas à serem recuperados até o <c> de vida do personagem</param>
    private void CurarVida(float danoRecebido)
    {
        this.pontosVidaInimigoAtual = Mathf.Round(this.pontosVidaInimigoAtual + danoRecebido); 
        print("Inimigo tem agora " +this.pontosVidaInimigoAtual+ " ponto(s) de vida.");
        if(this.pontosVidaInimigoAtual > this.pontosVidaInimigoMax)
        {
            this.pontosVidaInimigoAtual = 0;
            // Inimigo morreu
            print("Inimigo está com a vida cheia.");
        }else 
        {
            print("Inimigo tem agora " +this.pontosVidaInimigoAtual+ " ponto(s) de vida.");
        }
    }

    private void verifyFlipPersonagem(){
        if(this.lookLeft && this.transform.localScale.x > 0){
            flipPersonagem();
        } else if(!this.lookLeft && this.transform.localScale.x < 0){
            flipPersonagem();
        }
    }

    private void flipPersonagem(){
        if(!this.attacking){ // Nao girar personagem atacando
            // this.lookLeft = !this.lookLeft;

            //inverter o personagem
            this.transform.localScale = new Vector3(
                this.transform.localScale.x * -1, 
                this.transform.localScale.y,
                this.transform.localScale.z
            );

            this.barrasVida.transform.localScale = new Vector3(
                this.barrasVida.transform.localScale.x * -1, 
                this.barrasVida.transform.localScale.y,
                this.barrasVida.transform.localScale.z
            );
        }
    }

    private void verifyKnockBackPosition(){
        if((isPlayerLeft() && isKnockBackInRightPosition())
        || !(isPlayerLeft() || isKnockBackInRightPosition()) ){
            flipKnockbackPosition();
        }
    }

    private bool isPlayerLeft(){
        return this.playerScript.transform.position.x < this.transform.position.x;
    }

    private bool isKnockBackInRightPosition(){
        return this.lookLeft ? this.knockBackPosition.localPosition.x < 0 : this.knockBackPosition.localPosition.x > 0;
    }

    private void flipKnockbackPosition(){
        knockBackPosition.localPosition = new Vector3(this.knockBackPosition.localPosition.x * -1, knockBackPosition.localPosition .y, knockBackPosition.localPosition.z);
    }

    private void Morreu() {
        // Inimigo morreu
        this.pontosVidaInimigoAtual = 0;
        print("Inimigo morreu.");
        this.inimigoAnimator.SetInteger("idAnimation", 3); // Muda identificador para 3 (Animação de morte)
        StartCoroutine("Loot");
    }

    private bool isDead() {
        return this.pontosVidaInimigoAtual <= 0;
    }

    // -------------------------------------------------------
    // ROTINAS
    // -------------------------------------------------------

    /// <summary>
    /// Rotina para «piscar» o personagem quando ele toma dano
    /// </summary>
    /// <returns></returns>
    IEnumerator TomandoDanoCorouTine()
    {
        this.tomandoDano = true;
        this.barrasVida.SetActive(true);
        this.spriteRenderer.color = personagemCor[1];
        yield return new WaitForSeconds(0.3f);
        this.spriteRenderer.color = personagemCor[0];
        yield return new WaitForSeconds(0.3f);
        this.spriteRenderer.color = personagemCor[1];
        yield return new WaitForSeconds(0.3f);
        this.spriteRenderer.color = personagemCor[0];
        yield return new WaitForSeconds(0.3f);
        this.spriteRenderer.color = personagemCor[1];
        yield return new WaitForSeconds(0.3f);
        this.spriteRenderer.color = personagemCor[0];
        this.tomandoDano = false;
        this.barrasVida.SetActive(false);
    }


    /// <summary>
    /// Rotina para exibir as aminações de morte e loot
    /// </summary>
    /// <returns></returns>
    IEnumerator Loot(){
        yield return new WaitForSeconds(1); // Espera um segundo
        GameObject fxMorte = Instantiate(gameController.fxMorte, groundCheck.position, transform.localRotation); // Pega a animação de morte
        yield return new WaitForSeconds(.5f); // Espera mais un segundo
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(1f); // Espera mais un segundo
        Destroy(fxMorte); // Destroi animação depois de 1s
        Destroy(this.gameObject);// Destroi objeto da cena
    }
}
