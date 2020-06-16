using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chancontroller : MonoBehaviour
{
    //Script control of the character's movement And storing items in the inventory.
    public float speed = 4.0f;
    public float runspeed = 10.0f;
    public float rotSpeed = 80.0f;
    public float rot = 0.0f;
    
    public float gravity = 8.0f;
    private bool Run;
    private bool Slide;
    private float inputH;
    private float inputV;
    
    public UI ui;
    Vector3 moveDir = Vector3.zero;
    Vector3 runDir = Vector3.zero;
    Vector3 slideDir = Vector3.zero;

    CharacterController controller;
    Animator anim;
    Chancontroller noscript;

    public Inventory inventory;
    public InventorySlot iSlot;
    private IInventoryItem mCurrentItem = null;

    public GameObject Hand;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        noscript = GetComponent<Chancontroller>();
        Run = false;

    }

    
    void Update()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDir = new Vector3(0, 0, 0.1f);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                

            }
        

            else if (Input.GetKey(KeyCode.S))
            {
                moveDir = new Vector3(0, 0, -0.1f);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                IInventoryItem item = inventory.ItemTop();
                print("use item" + item);
                if (item != null)
                {
                    if (mCurrentItem != null)
                    {
                        DropCurrentItem();
                    }

                    inventory.UseItem(item);
                    inventory.RemoveItem(item);
                    ui.HP1.fillAmount += 0.1f;
                }
                else
                {
                    ui.dontuseitem();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (mCurrentItem != null)
                {
                    print("Drop item" + mCurrentItem);
                    DropCurrentItem();
                }
            }
            else
            {
                moveDir = new Vector3(0, 0, 0);
            }
            if (Input.GetKeyDown("f1"))
            {
                anim.Play("WAIT01", -1, 0f);
            }
            if (Input.GetKeyDown("f2"))
            {
                anim.Play("WAIT02", -1, 0f);
            }
            if (Input.GetKeyDown("f3"))
            {
                anim.Play("WAIT03", -1, 0f);
            }
            if (Input.GetKeyDown("f4"))
            {
                anim.Play("WAIT04", -1, 0f);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Run = true;
                runDir.y -= gravity * Time.deltaTime;
                controller.Move(runDir * Time.deltaTime);
            }
            else
            {
                Run = false;
                moveDir.y -= gravity * Time.deltaTime;
                controller.Move(moveDir * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("Jump", true);
            }
            else 
            {
                anim.SetBool("Jump", false);
            }
            if (Input.GetKey(KeyCode.F))
            {
                anim.SetBool("Slide", true);
                //slideDir.y -= gravity * Time.deltaTime; //So the character is on the floor And may cause bugs
                controller.Move(slideDir * Time.deltaTime);

            }
            else
            {
                anim.SetBool("Slide", false);
            }
           
        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);


        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        //Debug.Log(controller.isGrounded);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);

        anim.SetBool("Run", Run);
        if (Run)
        {
            runDir = new Vector3(0, 0, 1.5f);
            runDir *= speed;
            runDir = transform.TransformDirection(runDir);
        }
        if (Slide)
        {
            slideDir = new Vector3(0, 0, 1.5f);
            slideDir *= speed;
            slideDir = transform.TransformDirection(slideDir);

        }
        else
        {
            slideDir = new Vector3(0, 0, 0);
        }

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
            item.OnPickup();
        }
        if (hit.gameObject.tag == "KEY")
        {
            print("GET KEY!!");
            UI.count += 1;
            key();
            hit.gameObject.SetActive(false);
        }

    }
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (mCurrentItem != null)
        {
            SetItemActive(mCurrentItem, false);
        }

        IInventoryItem item = e.Item;
        SetItemActive(item, true);
        mCurrentItem = e.Item;
    }
    private void SetItemActive(IInventoryItem item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }
    public void DropCurrentItem()
    {
        anim.SetInteger("condition", 2);
        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = null;

        Rigidbody rbItem = goItem.AddComponent<Rigidbody>();
        if (rbItem != null)
        {
            rbItem.AddForce(transform.forward * 20.5f, ForceMode.Force);

            Invoke("DoDropItem", 0.75f);
        }
    }

    public void DoDropItem()
    {
        Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());
        mCurrentItem = null;
    }
    public void key()
    {
        if (UI.count == 5)
        {
            print("YOU WIN");
        }
    }
    public void hit()
    {
        anim.Play("DAMAGED00", -1, 0f);
    }
    public void Dead()
    {
        anim.SetBool("Dead", true);
    }
    public void notDead()
    {
        anim.SetBool("Dead", false);
    }
    public void notcontrol()
    {
        controller.enabled = false;
    }
}
