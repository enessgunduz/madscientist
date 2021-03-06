using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterController : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
    public GameObject spawnPoint;
    public GameObject ustAna, portre;
    public GameObject yazi;
    bool mouseClicked = false;
    Animator _animator;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.D)){
            _animator.SetBool("yuruyorMu", false);
        }
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
            transform.transform.Translate(new Vector2(moveDirection,0)* maxSpeed* Time.deltaTime);
            _animator.SetBool("yuruyorMu", true);
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseClicked = true;
        }

        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }

        
        
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Deadzone")){
            transform.position = spawnPoint.transform.position;
        } else if(other.gameObject.CompareTag("newLevel")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        if(other.gameObject.CompareTag("hikayeAcici1")){
            mouseClicked = false;
            GameObject.FindGameObjectWithTag("hikayeAcici1").gameObject.SetActive(false);
            StartCoroutine(scene1Hikaye());   
        }
        if(other.gameObject.CompareTag("hikayeAcici2")){
            mouseClicked = false;
            GameObject.FindGameObjectWithTag("hikayeAcici2").gameObject.SetActive(false);
            StartCoroutine(scene2Hikaye());   
        }
        if(other.gameObject.CompareTag("hikayeAcici3")){
            mouseClicked = false;
            GameObject.FindGameObjectWithTag("hikayeAcici3").gameObject.SetActive(false);
            StartCoroutine(scene3Hikaye());   
        }
    }

    IEnumerator scene1Hikaye(){
        ustAna.SetActive(true);
        yazi.GetComponent<TextMeshProUGUI>().SetText("Dondurma, kafamdan atmaya ??al????t??????m k??t?? an??lar??n temeli. O g??n izinliydim ve k??z??mla parka gidece??ime s??z vermi??tim.");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        yazi.GetComponent<TextMeshProUGUI>().SetText("Fakat k??yametimin ba??lang??c?? da bu s??z oldu... S??rada gere??inden fazla kalm????t??k arkam??zda iri yar?? bir adam vard?? ve...");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        yazi.GetComponent<TextMeshProUGUI>().SetText("Ve s??rekli ??fkeli bir ??ekilde homurdanarak, ???Nerede bunlar???? diyordu. (A??lar) Bu adam??n hayat??m?? alt ??st edece??ini nereden bilebilirdim?");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        ustAna.SetActive(false);
    }
    IEnumerator scene3Hikaye(){
        ustAna.SetActive(true);
        yazi.GetComponent<TextMeshProUGUI>().SetText("Can??m k??z??m, e??er ya??asayd?? ??imdi burada onunla oynuyor olabilirdik... En sevdi??i ??eylerden biri sal??ncakta sallanmakt??.");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        yazi.GetComponent<TextMeshProUGUI>().SetText("Fakat o lanet olas??ca g??n... Ke??ke o g??n parka gitmeseydik. O adam??n bizi takip etti??ini farketti??imde art??k ??ok ge??ti. Yine ??fkeli bir ??ekilde ???Ne zaman bitecek???? diye say??kl??yordu.");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        yazi.GetComponent<TextMeshProUGUI>().SetText("Neden b??yle konu??tu??unu bilmiyordum. Fakat su??lu ne dondurmayd?? ne de park... O adam benden hayallerimi, k??z??m??, her ??eyimi ??ald??!");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        ustAna.SetActive(false);
    }
    IEnumerator scene2Hikaye(){
        ustAna.SetActive(true);
        yazi.GetComponent<TextMeshProUGUI>().SetText("G??zel Emily, can??m k??z??m, onu en son bisikletinin ??st??nde g??rm????t??m. Parkta s??k??l??nca ???Baba, bisikletle gezebilir miyim???? dedi??inde ke??ke olacaklar?? g??r??p, ???Hay??r, eve gidiyoruz??? deseydim fakat demedim...");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        yazi.GetComponent<TextMeshProUGUI>().SetText("Onun yerine ???Tamam sen gitmeye ba??la, bende arkanday??m??? dedim. Ve yine o lanet olas?? adam?? g??rd??m, k??z??m??n arkas??ndan ko??uyordu.");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        yazi.GetComponent<TextMeshProUGUI>().SetText("Bu sefer ???Kimsin sen???? diyecektim fakat... Fakat... (A??lar) Yapamad??m... Her ??ey i??in ??ok ge??ti...");
        yield return new WaitUntil(() => mouseClicked);
        mouseClicked=false;
        ustAna.SetActive(false);
    }
}
