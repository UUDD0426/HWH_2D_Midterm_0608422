using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    //陣列 在類型後面加上中括號
    //用於保存相同類型的複數資料
    [Header("要關閉的木頭")]
    public GameObject[] stones;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "斧頭")
            stones[0].SetActive(false);
        stones[1].SetActive(false);
        
    }
}
