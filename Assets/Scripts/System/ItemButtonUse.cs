using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButtonUse : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text text;
    [SerializeField] private int id;
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private GameObject item;
    bool isUse;
    // Start is called before the first frame update
    void Start()
    {
        updateCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = FindObjectOfType<Player>();
    }
    public void updateCount()
    {
        text.text = objectManager.loadingData.players[objectManager.idPlayer].Items[id].ToString();
    }    
    public void getUseItem()
    {
        if (!isUse && objectManager.item[id] && objectManager.player)
        {
            if (objectManager.isSound)
                objectManager.Aus.PlayOneShot(objectManager.use);
            item = Instantiate(objectManager.item[id], objectManager.player.transform.position + objectManager.item[id].transform.position, objectManager.item[id].transform.rotation);
            item.transform.parent = objectManager.player.pa.transform;
            isUse = true;
            
            if (id == 0)
            {
                objectManager.player.useHp();
                Invoke("_reset", 1.2f);
            }    
            else if (id == 3)
            {
                objectManager.player.useTime();
                Invoke("_reset", 3f);
            }   
            else if (id == 4)
            {
                objectManager.player.useShield();
                Invoke("_reset", 2.4f);
            }
            else
            {
                Invoke("_reset", 2f);
            }
        }    
    }
    void _reset()
    {
        Destroy(item);
        isUse = false;
    }    
}
