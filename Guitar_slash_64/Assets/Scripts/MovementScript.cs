using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovementScript : MonoBehaviour
{

    public GameObject WinGame;
    public GameObject GameOver;
    public GameObject Pause;
    public float health = 100;
    public GameObject Enemy;
    public bool Weapon = false;
    public int attackDamage;
    public float inputX;
    public float inputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public GameObject Guitar;
    public float desiredRotationSpeed;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;
    private float verticalVel;
    private Vector3 moveVector;
    public Transform spherecastSpawn;
    public LayerMask enemyLayer;
    public Image healthBar;
    public float startHealth;
    public AudioSource hamburger;
    public AudioSource dagger;


    void Start()
    {
        Guitar.SetActive(false);
        anim = GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }


    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        if (health > 0)
        {

            WeaponOn();
            InputMagnitude();
            isGrounded = controller.isGrounded;
            anim.SetBool("Grounded", true);

            if (isGrounded)
                verticalVel -= 0;
            else
                verticalVel -= 2;

            moveVector = new Vector3(0, verticalVel, 0);
            controller.SimpleMove(moveVector * Time.deltaTime * 2f);
            if (Weapon == false)
                Dagger();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Pause.SetActive(true);
            Time.timeScale = 0;
        }

        if (GameObject.Find("EnemyTarget") == null)
        {
            Win();
        }

    }
    void Dagger()
    {
        if (Input.GetButtonDown("Dagger"))
        {
            if (Weapon == false)
                dagger.Play();
            anim.SetBool("Roll", true);
            Guitar.GetComponent<GuitarHit>();
        }
        else
        {
            anim.SetBool("Roll", false);
        }

    }
    void PlayerMoveAndRotation()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        if (Speed > 0.01f)

            desiredMoveDirection = forward * inputZ + right * inputX;

        if (blockRotationPlayer == false)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);


    }

    void WeaponOn()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Weapon == false)
            {
                anim.SetBool("Weapon", true);
                Guitar.SetActive(true);
                Weapon = true;
            }
            else if (Weapon == true)
            {
                anim.SetBool("Weapon", false);
                Guitar.SetActive(false);
                Weapon = false;
            }
        }

        OnAttack();

    }
    public void Die()
    {
        anim.SetBool("Death", true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameOver.SetActive(true);

    }
    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        WinGame.SetActive(true);
    }
    void OnAttack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            anim.SetTrigger("Attack");

            //RaycastHit hit;
            //if (Physics.SphereCast(spherecastSpawn.position, 0.5f, spherecastSpawn.TransformDirection(Vector3.forward), out hit, 1.5f, enemyLayer) && Weapon == true)
            //{
            //    Debug.DrawRay(spherecastSpawn.position, spherecastSpawn.TransformDirection(Vector3.forward),Color.red);
            //    hit.transform.GetComponent<EnemyNavAgent>().OnHit(attackDamage);
            //}
        }
    }


    void InputMagnitude()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        anim.SetFloat("H", inputZ, 0.0f, Time.deltaTime * 2f);
        anim.SetFloat("V", inputX, 0.0f, Time.deltaTime * 2f);

        Speed = new Vector2(inputX, inputZ).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("Speed", Speed, 0.0f, Time.deltaTime);

            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("Speed", Speed, 0.0f, Time.deltaTime);

        }
    }
    public void StartG()
    {
        Guitar.GetComponent<Collider>().enabled = true;
    }
    public void EndG()
    {
        Guitar.GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FistEnemy")
        {
            health -= 5;
            healthBar.fillAmount = health / startHealth;
        }
        if (other.gameObject.tag == "Medical")
        {
            hamburger.Play();
            health += 5;
            healthBar.fillAmount = health / startHealth;
            Destroy(other.gameObject);
        }
    }
}
