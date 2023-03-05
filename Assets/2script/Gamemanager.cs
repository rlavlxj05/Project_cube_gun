using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public player player;
    public Boss boss;
    public GameObject itemShop;
    public GameObject weaponShop;
    public GameObject startZon;
    public int stage;
    public float playerTime;
    public bool isBattle;
    public int EnemyCntA;
    public int EnemyCntB;
    public int EnemyCntC;
    public int EnemyCntD;

    public Transform[] enemyZones;
    public GameObject[] enemies;
    public List<int> enemyList;

    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject overpanel;
    public Text maxScoreTxt;
    public Text ScoreTxt;
    public Text stageTxt;
    public Text playTimeTxt;
    public Text playerHealthTxt;
    public Text playerAmmoTxt;
    public Text playerCoinTxt;
    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    public Image weaponRImg;
    public Text enemyATxt;
    public Text enemyBTxt;
    public Text enemyCTxt;
    public RectTransform bossHealthGroup;
    public RectTransform bossHealthBar;
    public Text curScoreText;
    public Text bestText;

    private void Awake()
    {
        enemyList = new List<int>();
        maxScoreTxt.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore"));
        if (PlayerPrefs.HasKey("MaxScore"))
            PlayerPrefs.SetInt("MaxScore",0);
    }

    public void GameStart()
    {
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gamePanel.SetActive(false);
        overpanel.SetActive(true);
        curScoreText.text = ScoreTxt.text;

        int maxScore = PlayerPrefs.GetInt("MaxScore");
        if (player.score > maxScore)
        {
            bestText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("MaxScore", player.score);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void StageStart()
    {
        itemShop.SetActive(false);
        weaponShop.SetActive(false);
        startZon.SetActive(false);

        foreach (Transform zone in enemyZones)
            zone.gameObject.SetActive(true);

        isBattle = true;
        StartCoroutine(InBattle());
    }

    public void StageEnd()
    {
        player.transform.position = Vector3.up * 0.8f;

        itemShop.SetActive(true);
        weaponShop.SetActive(true);
        startZon.SetActive(true);

        foreach (Transform zone in enemyZones)
            zone.gameObject.SetActive(false);

        isBattle = false;
        stage++;
    }

    IEnumerator InBattle()
    {
        if(stage % 5 == 0)
        {
            EnemyCntC++;
            GameObject instantEnemy = Instantiate(enemies[3], enemyZones[0].position, enemyZones[0].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.target = player.transform;
            enemy.manager = this;
            boss = instantEnemy.GetComponent<Boss>();
        }
        else
        {
            for (int index = 0; index < stage; index++)
            {
                int ran = Random.Range(0, 3);
                enemyList.Add(ran);

                switch (ran)
                {
                    case 0:
                        EnemyCntA++;
                        break;
                    case 1:
                        EnemyCntB++;
                        break;
                    case 2:
                        EnemyCntC++;
                        break;

                }
            }

            while (enemyList.Count > 0)
            {
                int ranZone = Random.Range(0, 4);
                GameObject instantEnemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.target = player.transform;
                enemy.manager = this;
                enemyList.RemoveAt(0);
                yield return new WaitForSeconds(4f);
            }
        }

        while (EnemyCntA + EnemyCntB + EnemyCntC + EnemyCntD > 0) 
        {
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        boss = null;
        StageEnd();
    }

    private void Update()
    {
        if (isBattle)
            playerTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        //상단
        ScoreTxt.text = string.Format("{0:n0}", player.score);
        stageTxt.text = "STAGE" + stage;

        int hour = (int)(playerTime / 3600);
        int min = (int)((playerTime - hour * 3600)/ 60);
        int second = (int)(playerTime % 60);
        playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);

        //플레이어
        playerHealthTxt.text = player.health + "/"+player.maxHealth;
        playerCoinTxt.text = string.Format("{0:n0}", player.coin);
        if (player.equipWeapon == null)
            playerAmmoTxt.text = " / " + player.ammo;
        else if (player.equipWeapon.type == Weapon.Type.Melee)
            playerAmmoTxt.text = " / " + player.ammo;
        else
            playerAmmoTxt.text = player.equipWeapon.curAmmo+ " / " + player.ammo;

        //무기
        weapon1Img.color = new Color(1, 1, 1, player.hasWeapons[0] ? 1 : 0);
        weapon2Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        weaponRImg.color = new Color(1, 1, 1, player.hasGrenades > 0 ? 1 : 0);

        //몬스터 숫자
        enemyATxt.text = EnemyCntA.ToString();
        enemyBTxt.text = EnemyCntB.ToString();
        enemyCTxt.text = EnemyCntC.ToString();

        //보스 체력
        if(boss != null)
        {
            bossHealthGroup.anchoredPosition = Vector3.down * 30;
            bossHealthBar.localScale = new Vector3((float)boss.curHealth / boss.mexHealth, 1, 1);

        }
        else
        {
            bossHealthGroup.anchoredPosition = Vector3.up * 200;
        }
    }

}

