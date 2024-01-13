using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private ObjectManager objectManager;
    public void getShop()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.shop.SetActive(!objectManager.shop.activeSelf);
    }
    public void getEquip()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.equip.SetActive(!objectManager.equip.activeSelf);
    }
    public void getSetting()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.setting.SetActive(!objectManager.setting.activeSelf);
    }
    public void getBag()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.bag.SetActive(!objectManager.bag.activeSelf);
    }
    public void backStart()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.level.SetActive(false);
        objectManager.rank.SetActive(false);
        objectManager.help.SetActive(false);
        objectManager.start.SetActive(true);
    }
    public void play()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.level.SetActive(true);
        objectManager.start.SetActive(false);
    }
    public void rank()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.rank.SetActive(true);
        objectManager.start.SetActive(false);
    }
    public void help()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.help.SetActive(true);
        objectManager.start.SetActive(false);
    }
    public void getCloseUiNote()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.uiNote.SetActive(false);
    }
    public void getSound()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.loadingData.players[objectManager.idPlayer].Sound = !objectManager.loadingData.players[objectManager.idPlayer].Sound;
        if (objectManager.loadingData.players[objectManager.idPlayer].Sound)
        {
            objectManager.isSound = true;
            objectManager.soundOff.SetActive(false);
            objectManager.soundOn.SetActive(true);
        }
        else
        {
            objectManager.isSound = false;
            objectManager.soundOn.SetActive(false);
            objectManager.soundOff.SetActive(true);
        }
        objectManager.loadingData.SavePlayersToFile();
    }
    public void getMusic()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.loadingData.players[objectManager.idPlayer].Music = !objectManager.loadingData.players[objectManager.idPlayer].Music;
        if (objectManager.loadingData.players[objectManager.idPlayer].Music)
        {
            objectManager.isMusic = true;
            objectManager.Aus.Play();
            objectManager.musicOff.SetActive(false);
            objectManager.musicOn.SetActive(true);
        }
        else
        {
            objectManager.isMusic = false;
            objectManager.Aus.Stop();
            objectManager.musicOn.SetActive(false);
            objectManager.musicOff.SetActive(true);
        }
        objectManager.loadingData.SavePlayersToFile();
    }
    public void getCloseUiBuy()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.uiBuy.SetActive(false);
    }
    public void getCloseUiNotBuy()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        objectManager.uiNotBuy.SetActive(false);
    }
    public void getBuy()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        if (objectManager.tankOrItem == 1)
        {
            objectManager.loadingData.players[objectManager.idPlayer].Gold -= objectManager.cost;
            objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();
            objectManager.loadingData.players[objectManager.idPlayer].Items[objectManager.idItem] += 1;
            objectManager.buttonsUseItem[objectManager.idItem].updateCount();
            objectManager.loadingData.SavePlayersToFile();
            objectManager.uiBuy.SetActive(false);
        }
        else
        {
            objectManager.loadingData.players[objectManager.idPlayer].Gold -= objectManager.cost;
            objectManager.gold.text = objectManager.loadingData.players[objectManager.idPlayer].Gold.ToString();

            objectManager.loadingData.players[objectManager.idPlayer].Equipments[objectManager.idItem] = 1;

            objectManager.equip.GetComponent<Equipment>().mode = 1;
            objectManager.equip.GetComponent<Equipment>().text.text = "Select";
            objectManager.equip.GetComponent<Equipment>().bt.color = new Color(5f / 255f, 1f, 0f, 1f);


            objectManager.loadingData.SavePlayersToFile();
            objectManager.uiBuy.SetActive(false);
        }

    }
    public void backMenuLevel()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
        Destroy(objectManager._level);
        if (objectManager._player)
            Destroy(objectManager._player);
        foreach (Transform child in objectManager.bullets.transform)
        {
            Destroy(child.gameObject);
        }
        objectManager.uiLose.SetActive(false);
        objectManager.uiWin.SetActive(false);
        objectManager.mainPlay.SetActive(false);
        objectManager.level.SetActive(true);
    }    

    public void openLevel()
    {
        if (objectManager.isSound)
            objectManager.Aus.PlayOneShot(objectManager.click);
            Destroy(objectManager._level);
        if (objectManager._player)
            Destroy(objectManager._player);
        foreach (Transform child in objectManager.bullets.transform)
        {
            Destroy(child.gameObject);
        }
        if (objectManager.idLV < 4 && objectManager.loadingData.players[objectManager.idPlayer].Levels[objectManager.idLV] == 1)
        {
            objectManager.countEne = 0;
            objectManager.killE = 0;
            objectManager.idLevel.text = objectManager.idLV.ToString();
            objectManager.level.SetActive(false);
            objectManager.mainPlay.SetActive(true);
            objectManager._level = Instantiate(objectManager.levels[objectManager.idLV], objectManager.levels[objectManager.idLV].transform.position, objectManager.levels[objectManager.idLV].transform.rotation);
            objectManager._player = Instantiate(objectManager.tank[objectManager.idTank], objectManager._position[objectManager.idLV], objectManager.tank[objectManager.idTank].transform.rotation);
            objectManager.player = objectManager._player.GetComponentInChildren<Player>();
            objectManager.virtualCamera.Follow = objectManager.player.transform;
            objectManager.uiWin.SetActive(false);
            objectManager.uiLose.SetActive(false);
            objectManager.countEne = objectManager.levels[objectManager.idLV].GetComponentsInChildren<Enemy>().Length;
            objectManager.killEne.text = $"{objectManager.killE}/{objectManager.countEne}";
            objectManager.idLevel.text = (objectManager.idLV + 1).ToString();
        }
        else
        {
            objectManager.level.SetActive(true);
            objectManager.mainPlay.SetActive(false);
            objectManager.uiWin.SetActive(false);
            objectManager.uiLose.SetActive(false);
            objectManager.uiNote.SetActive(true);
            objectManager.textNote.text = "The level has not been released yet.We will update as soon as possible!";
        }
    }

}
