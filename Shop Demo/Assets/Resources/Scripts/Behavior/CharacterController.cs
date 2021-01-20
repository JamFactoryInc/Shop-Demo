using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 input;
    public SpriteRenderer sr;
    public SpriteRenderer[] clothes;
    public bool inDialogue;
    public Mannequin activeMannequin;

    private Animator anim;
    private Config conf;
    // Start is called before the first frame update
    void Start()
    {
        input = rb.velocity;
        conf = Main.config;
        anim = GetComponent<Animator>();
        AddMoney(100);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inDialogue)
        {
            Move();
            SetDirection();
        }
        else
        {
            rb.velocity *= 0;
        }
        SetAnim();
        
    }

    void Move()
    {
        input.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = input.normalized * Main.config.movementSpeed;
        //Equip("tux_shirt");
        //Equip("cap");
        //Equip("tux_pants");
    }

    void SetDirection()
    {
        if (input.x != 0)
            transform.localScale = new Vector3(-Mathf.Clamp(input.x, -1, 1), 1, 1);
            //sr.flipX = input.x > 0;
    }

    public void SwapWithMannequin()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            activeMannequin.swapSprite[activeMannequin.selectedSwapSprite].color = new Color(1, 1, 1);
            activeMannequin.selectedSwapSprite = mod(activeMannequin.selectedSwapSprite + (int)Input.GetAxisRaw("Vertical"), 3);
            activeMannequin.swapSprite[activeMannequin.selectedSwapSprite].color = new Color(0, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {

            string playerClothes = Equip(activeMannequin.clothingIDs[activeMannequin.selectedSwapSprite + 1]);
            activeMannequin.Equip(playerClothes);
        }
    }

    int mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    void SetAnim()
    {
        anim.SetBool("moving", rb.velocity.magnitude > 0);
       
    }

    public string Equip(string clothingID)
    {
            Clothing newArticle = Clothing.clothes[clothingID];
            string ret = Main.save.player.clothes[newArticle.article];
            Main.save.player.clothes[newArticle.article] = clothingID;
            clothes[newArticle.article].sprite = ResourceLoader.clothingTextures[clothingID];
            return ret;
    }

    public void ForceEquip(string id, int slot)
    {
        clothes[slot].sprite = ResourceLoader.clothingTextures[id];
    }

    public static bool AddMoney(int money)
    {
        if (money >= 0 && Main.save.money + money <= 999 || ( money < 0 && Main.save.money >= money * -1))
            Main.save.money += money;
        else return false;
        UIController.UpdateMoney();
        return true;
    }   
}
