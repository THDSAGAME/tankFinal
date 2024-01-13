using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int idPlayer;
    [SerializeField] private int _typePlayer;
    [SerializeField] private float blood;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator anm;
    [SerializeField] private Blood bl;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private bool isDie;
    [SerializeField] public GameObject _bullet1, _bullet3, _light1, _light3, headGun, player, _bl, pa;
    [SerializeField] public bool isShield;
    int move;
    float horizontalInput;
    float verticalInput;
    
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float speed, speedBullet;

    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        _bl.SetActive(false);
        move = 8;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 velocity = new Vector2(horizontalInput, verticalInput);
        if (velocity.x == 0f || velocity.y == 0f)
            _rb.velocity = velocity * speed;
        if (verticalInput == 0f)
        {
            if (horizontalInput == -1f)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
                move = 4;
            }   
            else if (horizontalInput == 1f)
            {

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90f);
                move = 6;
            }    
        }
        if (horizontalInput == 0f)
        {
            if (verticalInput == 1f)
            {

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                move = 8;
            }
            else if (verticalInput == -1f)
            {

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -180f);
                move = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBullet;
            if (_typePlayer == 1)
            {
                newBullet = Instantiate(_bullet1, transform.position, _bullet1.transform.rotation);
                _light1.SetActive(true);
                Invoke("resetLight1", 0.3f);
            }
            else
            {
                newBullet = Instantiate(_bullet3, transform.position, _bullet3.transform.rotation);
                _light3.SetActive(true);
                Invoke("resetLight3", 0.3f);
            }
            newBullet.transform.parent = objectManager.bullets.transform;
            if (objectManager.isSound)
                objectManager.Aus.PlayOneShot(objectManager.ban);
            if (move == 2)
            {
                newBullet.GetComponent<Bullet>().velocity = new Vector3(0, -speedBullet, 0);
                newBullet.transform.position = new Vector3(transform.position.x, transform.position.y - 0.7f, newBullet.transform.position.z);
                newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, -180f);
            }
            else if (move == 8)
            {
                newBullet.GetComponent<Bullet>().velocity = new Vector3(0, speedBullet, 0);
                newBullet.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, newBullet.transform.position.z);
                newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, 0f);
            }
            else if (move == 4)
            {
                newBullet.GetComponent<Bullet>().velocity = new Vector3(-speedBullet, 0, 0);
                newBullet.transform.position = new Vector3(transform.position.x - 0.7f, transform.position.y, newBullet.transform.position.z);
                newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, 90f);
            }
            else
            {
                newBullet.GetComponent<Bullet>().velocity = new Vector3(speedBullet, 0, 0);
                newBullet.transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y, newBullet.transform.position.z);
                newBullet.transform.eulerAngles = new Vector3(newBullet.transform.eulerAngles.x, newBullet.transform.eulerAngles.y, -90f);
            }
            newBullet.GetComponent<Bullet>().setVelocity();
        }
        if (!isDie)
            pa.transform.position = transform.position + new Vector3(0, 3.48f, 0);
        if (!isDie && blood < 0)
        {
            anm.Play("die");
            if (objectManager.isSound)
                objectManager.Aus.PlayOneShot(objectManager.no);
            headGun.SetActive(false);
            Invoke("_des", 1f);
            isDie = true;
            Invoke("setLose", 0.3f);
        }
    }
    void setLose()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.sLose);
        objectManager.uiLose.SetActive(true);
        objectManager.loadingData.players[objectManager.idPlayer].Point += objectManager.killE;
        objectManager.loadingData.players[objectManager.idPlayer].Gold += objectManager.killE * 10;
        objectManager.loadingData.SavePlayersToFile();
        objectManager.loadingData.resetRank(false);
        objectManager.textLose.text = $"- Kill: {objectManager.killE}/{objectManager.countEne}\n- Gold: +{objectManager.killE * 10}\n- Point: +{objectManager.killE}";
        objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();
        objectManager.point.text = objectManager.loadingData.players[objectManager.idPlayer].Point.ToString();
    }    
    void resetLight1()
    {
        _light1.SetActive(false);
    }
    void resetLight3()
    {
        _light3.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_bl.activeSelf && collision.CompareTag("Ene"))
        {
            _bl.SetActive(true);
            Invoke("closeBlood", 1f);
        }
        //if (!isDie && collision.CompareTag("Bullet"))
        //{
        //    anm.Play("die");
        //    headGun.SetActive(false);
        //    Invoke("_des", 1f);
        //    isDie = true;
        //}
    }
    void _des()
    {
        player.SetActive(false);
        Destroy(player);
    }
    void closeBlood()
    {
        _bl.SetActive(false);
    }
    public void getBullet()
    {
        blood -= 40f;
        bl.blood = blood;

    }
    void getUseHp()
    {
        blood += 40f;
        if (blood > 100f)
            blood = 100f;
        bl.blood = blood;
    }
    public void useHp()
    {
        _bl.SetActive(true);
        Invoke("getUseHp", 0.3f);
        Invoke("closeBlood", 1.2f);
    }    
    void _useShield()
    {
        isShield = false;
    }    
    public void useShield()
    {
        isShield = true;
        Invoke("_useShield", 2.4f);
    }
    public void _useTime()
    {
        objectManager.isTime = false;
    }
    public void useTime()
    {
        objectManager.isTime = true;
        Invoke("_useTime", 3f);
    }    
}
