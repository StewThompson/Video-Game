using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.UI;
using Unity.VisualScripting;
using UnityEngine;
using static MainCharacter;

public class Abilities : MonoBehaviour
{
    public GameObject fire;
    public GameObject tornado;
    public GameObject dummy; 
    public GameObject shield;
    public GameObject lightning;
    public GameObject[] buttons;
    private Dictionary<string, bool> AvailableAbilities = new Dictionary<string, bool> { { "fire", false },
    { "lightning", false },{ "ability", false },{ "shield", false },{ "tornado", true },{ "Ability 6", false },
    { "dummy", false },{ "Ability 8", false },{ "Ability 9", false },};
    private MainCharacter player;
    private float cooldown;
    private float cooldown2;
    private float shieldActiveTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<MainCharacter>();
        GameObject Logic = GameObject.Find("MainGameLogic");
        MainGameLogic mainGameLogic = Logic.GetComponent<MainGameLogic>();
        mainGameLogic.LevelUp += MainGameLogic_LevelUp;
    }

    private void MainGameLogic_LevelUp(object sender, MainGameLogic.LevelStats e)
    {
        if(e.level == 5) { for (int i = 0; i < buttons.Length; i++) { buttons[i].SetActive(true); } }
       
    }

    // Update is called once per frame
    void Update()
    {
        ZInputAbilities();
        XInputAbilities();


    }
    private void ZInputAbilities()
    {
        if (cooldown > 0)
        {
            //On Cooldown
            cooldown -= Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Z) && cooldown <= 0)
        {

            if (player.GetMana() == 0)
            { //No Mana Available
            }
            else { shootFire(); shootLightning(); activateShield(); }

        }
        DeactivateShield();
    }
    private void XInputAbilities()
    {
        if (Input.GetKey(KeyCode.X)&&cooldown2<=0)
        {
            
            Dash();Dummy();shootTornado();
        }
        else if(cooldown2>0) { cooldown2-=Time.deltaTime; }
        if (DummyAliveTime > 0) { DummyAliveTime -= Time.deltaTime; }
        else { DeactiveDummy(); }
    }
    private void shootFire()
    {
        if (AvailableAbilities["fire"])
        {
            float fireSpeed = 10;
            cooldown = 2;
            player.useMana(2);
            if (Input.mousePosition.x > 960)
            {
                GameObject fireball = Instantiate(fire, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), transform.rotation);
                Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
                fireballRb.velocity = new Vector2(fireSpeed, 0);
            }
            else
            {
                GameObject fireball = Instantiate(fire, new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z), new Quaternion(0, 180, 0, 0));
                Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
                fireballRb.velocity = new Vector2(-fireSpeed, 0);
            }
        }
    }
    private void shootLightning()
    {
        if (AvailableAbilities["lightning"])
        {
            
            cooldown = 2;
            player.useMana(2);
            if (Input.mousePosition.x > 960)
            {
                GameObject fireball = Instantiate(lightning, new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), transform.rotation);
               
            }
            else
            {
                GameObject fireball = Instantiate(lightning, new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z), new Quaternion(0, 180, 0, 0));
                
            }
        }
    }
    private void activateShield()
    {
        if (AvailableAbilities["shield"])
        {
            player.CanTakeDamage = false;
            shieldActiveTime = 5;
            cooldown = 10;
            shield.SetActive(true);
        }
    }
    private void DeactivateShield()
    {
        if (shieldActiveTime > 0) { shieldActiveTime -= Time.deltaTime; }
        else { player.CanTakeDamage = true; shield.SetActive(false); }

    }
    private void Dash()
    {
        if (AvailableAbilities["Ability 6"])
        {
            float maxDistance = 10;
            float teleportCooldown = 5;
            Vector2 mousePos = Input.mousePosition;
            Vector2 actual = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 currentPos = transform.parent.position;
            Vector2 teleportPos = (actual - currentPos).normalized;

            teleportPos *= TeleportDistance(maxDistance, teleportPos);
            transform.parent.position = new Vector3(transform.parent.position.x + teleportPos.x, transform.parent.position.y + teleportPos.y, transform.parent.position.z);
            cooldown2 = teleportCooldown;
        }
    }
    private float TeleportDistance(float maxTeleport, Vector2 direction)
    {


        Vector2 start = transform.parent.position;
        RaycastHit2D hit = Physics2D.Raycast(start, direction, maxTeleport, LayerMask.GetMask("Ground"));
        Debug.DrawRay(start, direction * maxTeleport, Color.red, 2.0f);

        if (hit.collider != null)
        {
            Debug.Log($"Hit: {hit.collider.name} at distance: {hit.distance}");
            return Mathf.Max(0, hit.distance - 0.1f); // Adding a small buffer
        }
        else
        {
            Debug.Log("No hit detected.");
        }

        return maxTeleport;


        /* float buffer = 0.1f;
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        Vector2 start = transform.parent.position;
        for (int i = 1; i <= maxTeleport;i++)
        {
            Vector2 currentPos =start;
            Vector2 testPoint = new Vector3(currentPos.x + direction.x*i,currentPos.y + direction.y*i,0);
            Collider[] intersecting = Physics.OverlapSphere(testPoint, 0.1f,groundLayer);
            Debug.Log("At distance " + i + " intersecting is " + intersecting.Length);
            if (intersecting.Length > 0)
            {
                Debug.DrawLine(start, testPoint, Color.red, 2.0f);
                return i - 1-buffer;
            }
            Debug.DrawLine(start, testPoint, Color.green, 2.0f);

        }
        Vector2 maxPoint = new Vector2(start.x + direction.x * maxTeleport, start.y + direction.y * maxTeleport);
        Debug.DrawLine(start, maxPoint, Color.blue, 2.0f);
        return maxTeleport;*/
    }
    //Need to change so that it goes where mouse is rather than 10 in the direction
    GameObject dum;
    public float DummyActivationTime = 15;
    private float DummyAliveTime;
    private void Dummy()
    {
        if (AvailableAbilities["dummy"])
        {
            float teleportCooldown = 30;
            Vector2 mousePos = Input.mousePosition;
            Vector2 actual = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 currentPos = transform.parent.position;
            Vector2 teleportPos = (actual - currentPos).normalized;

            teleportPos *= TeleportDistance(Vector2.Distance(actual, currentPos), teleportPos);
            dum = Instantiate(dummy);
            dum.transform.position = new Vector3(transform.position.x + teleportPos.x, transform.position.y + teleportPos.y, transform.position.z);
            cooldown2 = teleportCooldown;
            DummyAliveTime = DummyActivationTime;
        }
    }
    private void DeactiveDummy() { if (dum != null) { Destroy(dum); } }
  
    private void shootTornado()
    {
        if (AvailableAbilities["tornado"])
        {
            float tornadoSpeed = 10;
            cooldown2 = 2;
            player.useMana(2);
            if (Input.mousePosition.x > 960)
            {
                GameObject fireball = Instantiate(tornado, new Vector3(transform.position.x + 1.5f, transform.position.y+2, transform.position.z), transform.rotation);
                Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
                fireballRb.velocity = new Vector2(tornadoSpeed, 0);
            }
            else
            {
                GameObject fireball = Instantiate(tornado, new Vector3(transform.position.x - 1.5f, transform.position.y+2, transform.position.z), new Quaternion(0, 180, 0, 0));
                Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
                fireballRb.velocity = new Vector2(-tornadoSpeed, 0);
            }
        }
    }

    private void learnFire()
    {
        CloseFirstAbilities();
        AvailableAbilities["fire"] = true;
    }
    private void learnLightning()
    {
        CloseFirstAbilities();
        AvailableAbilities["lightning"] = true;
    }
    private void learnShield()
    {
        CloseFirstAbilities();
        AvailableAbilities["shield"] = true;
    }
    private void CloseFirstAbilities()
    {
        int numberOfAbilities = 3;
        for (int i = 0; i < numberOfAbilities; i++) { buttons[i].SetActive(false); }
    }
}
