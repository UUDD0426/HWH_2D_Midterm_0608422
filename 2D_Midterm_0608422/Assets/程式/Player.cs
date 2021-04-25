using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("移動速度"), Range(0, 300)]
    public float speed = 15.5f;
    [Header("名字"),]
    public string  cName = "龍";
    [Header("虛擬搖桿")]
    public FixedJoystick joystick;
    [Header("變形元件")]
    public Transform tra;
    [Header("動畫元件")]
    public Animator ani;
    [Header("偵測範圍")]
    public float rangeAttack = 2.5f;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("撿取音效")]
    public AudioClip soundAttack;


    private void OnDrawGizmos()
    {
        //指定圖示顏色(紅,綠,藍,透明)
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        //繪製圖示 球體(中心點,半徑)
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float  h = joystick.Horizontal;

        tra.Translate(h * speed * Time.deltaTime, 0, 0);
        ani.SetFloat("水平", h);
    }

    public void Attack()
    {

        //音效來源,撥放一次(音效片段,音量)
        aud.PlayOneShot(soundAttack, 0.5f);

        //2D物理 圓形碰撞(中心點,半徑,方向,距離,圖層編號)
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, rangeAttack, -transform.up, 0, 1 << 8);

        //如果 碰到物件存在 並且 碰到的物件 標籤 為道具 就取得道具腳本並呼叫掉落道具方法
        if (hit && hit.collider.tag == "道具") hit.collider.GetComponent<Item>().DropProp();

    }
    private void Update()
    {
        Move();

    }
    [Header("撿蘋果音效")]
    public AudioClip soundEat;
    [Header("蘋果數量")]
    public Text textCoin;


    private int coin;


    //觸發事件-進入:兩個物件必須有一個勾選 Is Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "蘋果")
        {
            coin++;
            aud.PlayOneShot(soundEat);
            Destroy(collision.gameObject);
            textCoin.text = "蘋果:" + coin;
        }
    }

}
