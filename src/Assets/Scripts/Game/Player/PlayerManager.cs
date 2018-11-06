using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Weapen;
using Game.Packet;

namespace Game.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public GameObject grePref;//手雷预制体
        public Transform grePos;//手雷生成坐标
        public Transform greParent;//手雷父节点
        public Light flashlight;//手电筒
        public Transform handWeapen;//装备武器
        public Transform floorWeapenParent;//地上武器父节点
        public Transform[] itemPre;//道具预制体
        public Image image;
        public int HPMax = 100;
        public int HP;

        private Animator anim;
        private WeapenManager weapenM;
        bool isEnd = false;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            HP = HPMax;
        }
        private void Start()
        {
            weapenM = WeapenManager.instance;
        }
        private void Update()
        {
            if (!GameManager.instance.stopAction)
            {
                //射击
                changeShotState();
                if (weapenM.bulletCapacity!=0 && !anim.GetCurrentAnimatorStateInfo(3).IsName("Rifle_Reload_2") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Rifle_Hit_L_1"))
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        anim.SetLayerWeight(2, 1);
                        shot();
                    }
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        weapenM.resetTime();
                        anim.SetLayerWeight(2, 0);
                        weapenM.singleShot = true;
                    }
                }
                if (weapenM.bulletCapacity == 0)
                {
                    weapenM.resetTime();
                    anim.SetLayerWeight(2, 0);
                    weapenM.singleShot = true;
                }
                //开手电筒
                FlashlightController();
                //换子弹
                if (((Input.GetKeyUp(KeyCode.R) && weapenM.bulletCapacity < weapenM.weapenProperty.bulletCapacityMax) || weapenM.bulletCapacity == 0) && weapenM.bulletPacketCap!=0)
                {
                    anim.SetTrigger(Hashs.isReload);
                    reload();
                }
            }
            if (HP<=0&&isEnd==false)
            {
                HP = 0;
                isEnd = true;
                End.EndLayerManager.instance.EndGame();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                switch (other.tag)
                {
                    case "Weapen":
                        handWeapen.GetChild(1).GetComponent<SphereCollider>().enabled = true;
                        handWeapen.GetComponentInChildren<Light>().enabled = true;
                        handWeapen.GetChild(1).position = other.transform.position;
                        handWeapen.GetChild(1).SetParent(floorWeapenParent);
                        other.GetComponent<SphereCollider>().enabled = false;
                        other.GetComponentInChildren<Light>().enabled = false;
                        other.transform.SetParent(handWeapen);
                        other.transform.localPosition = Vector3.zero;
                        weapenM.LoadProperty();
                        break;
                    case "Bullet":
                        anim.speed = 0;
                        var item = Instantiate(itemPre[0]);
                        PacketManager.instance.PickUpItem(item);
                        Destroy(other.gameObject);
                        anim.speed = 1;
                        break;
                    default:
                        break;
                }
            }
        }
        //扔手雷
        void ThrowGrenade()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
            {
                anim.SetBool(Hashs.isThrow, true);
            }
        }
        //开关手电
        void FlashlightController()
        {
            if (Input.GetKeyUp(KeyCode.G))
            {
                if (flashlight.enabled)
                {
                    flashlight.enabled = false;
                }
                else
                {
                    flashlight.enabled = true;
                }
            }
        }
        //近战
        void Melee()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Rifle_Melee_Hard"))
            {
                anim.SetBool(Hashs.isMelee, true);
            }
        }
        //被击
        public void Hurt(int damage)
        {
            HP -= damage;
            anim.SetTrigger(Hashs.isHit);
            if (HP<=0)
            {
                HP = 0;
                anim.SetTrigger(Hashs.isDead);
            }
        }
        //射击
        void shot()
        {
            if (handWeapen != null)
            {
                StartCoroutine(weapenM.shoot());
            }
        }
        //更改枪的状态
        void changeShotState()
        {
            if (Input.GetKeyUp(KeyCode.B) && handWeapen!=null)
            {
                switch (WeapenManager.instance.ShotState)
                {
                    case WeapenManager.shotState.IDLE:
                        WeapenManager.instance.ShotState = WeapenManager.shotState.SINGLE;
                        WeapenManager.instance.weapenState = "单发";
                        break;
                    case WeapenManager.shotState.SINGLE:
                        WeapenManager.instance.ShotState = WeapenManager.shotState.TRIPLE;
                        WeapenManager.instance.weapenState = "三连发";
                        break;
                    case WeapenManager.shotState.TRIPLE:
                        WeapenManager.instance.ShotState = WeapenManager.shotState.IDLE;
                        WeapenManager.instance.weapenState = "连发";
                        break;
                    default:
                        break;
                }
            }
        }
        //创建手雷
        void createGrenade()
        {
            var gre = Instantiate(grePref, grePos.position, Quaternion.identity, greParent);
            gre.GetComponent<Rigidbody>().AddForce(new Vector3(0, 2000, 2000));
            Destroy(gre, 3);
        }
        //换子弹
        void reload()
        {
            var tmpCount = 0;
            if (weapenM.bulletPacketCap>= weapenM.weapenProperty.bulletCapacityMax - weapenM.bulletCapacity)
            {
                for (int i = 0; i < PacketManager.instance.grid.Count; i++)
                {
                    if (PacketManager.instance.grid[i].childCount == 0) continue;
                    if (PacketManager.instance.grid[i].GetComponent<GridManager>().itemCount> weapenM.weapenProperty.bulletCapacityMax - weapenM.bulletCapacity+tmpCount)
                    {
                        PacketManager.instance.grid[i].GetComponent<GridManager>().itemCount -= weapenM.weapenProperty.bulletCapacityMax - weapenM.bulletCapacity - tmpCount;
                        PacketManager.instance.grid[i].GetComponentInChildren<Text>().text = PacketManager.instance.grid[i].GetComponent<GridManager>().itemCount.ToString();
                        break;
                    }
                    else
                    {
                        tmpCount = PacketManager.instance.grid[i].GetComponent<GridManager>().itemCount;
                        Destroy(PacketManager.instance.grid[i].GetChild(0).gameObject);
                    }
                }
                weapenM.bulletPacketCap -= weapenM.weapenProperty.bulletCapacityMax - weapenM.bulletCapacity;
                weapenM.bulletCapacity = weapenM.weapenProperty.bulletCapacityMax;
            }
            else
            {
                weapenM.bulletCapacity += weapenM.bulletPacketCap;
                weapenM.bulletPacketCap = 0;
                foreach (var item in PacketManager.instance.grid)
                {
                    if (item.GetComponent<GridManager>().itemCount != 0)
                    {
                        Destroy(item.GetChild(0).gameObject);
                        break;
                    }
                }
            }

        }
    }
}
