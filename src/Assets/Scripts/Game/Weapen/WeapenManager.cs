using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

namespace Game.Weapen
{
    public class WeapenManager : Instance.Instance<WeapenManager>
    {
        public GameObject bulletPref;//子弹预制体
        public Transform bulletParent;//子弹父节点
        public Transform bulletPos;//子弹生成坐标
        public float bulletSpeed = 80000;//子弹射速
        public bool singleShot = true;//单发开关
        public Transform weapenStar;//准星
        public int bulletCapacity;//子弹当前容量
        public int bulletPacketCap;//弹夹当前容量
        public string weapenState = "连发";//武器当前状态
        public weapenStruct weapenProperty;
        public AudioClip[] music;

        private GameObject bullet;
        private float time = 0;
        public struct weapenStruct
        {
            public int ID;
            public double ShotSpeed;//开枪间隔
            public int Damage;//伤害
            public double MaxDistance;//射程
            public int bulletCapacityMax;//一个弹夹容量
        }
        public enum shotState
        {
            IDLE,
            SINGLE,
            TRIPLE
        }
        public shotState ShotState
        {
            get;
            set;
        }
        private new void Awake()
        {
            base.Awake();
            LoadProperty();
            bulletPacketCap = 0;
            bulletCapacity = 0;
        }
        public void resetTime()
        {
            time = (float)weapenProperty.ShotSpeed;
        }
        //加载枪支信息
        public void LoadProperty()
        {
            var weapen = transform.GetChild(1);
            var json = JsonMapper.ToObject<Dictionary<string, weapenStruct>>(Resources.Load<TextAsset>("weapen").text);
            time = (float)json[weapen.name].ShotSpeed;
            weapenProperty.ID = json[weapen.name].ID;
            weapenProperty.ShotSpeed = json[weapen.name].ShotSpeed;
            weapenProperty.Damage = json[weapen.name].Damage;
            weapenProperty.MaxDistance = json[weapen.name].MaxDistance;
            weapenProperty.bulletCapacityMax = json[weapen.name].bulletCapacityMax;
        }
        //射击
        public IEnumerator shoot()
        {
            time += Time.deltaTime;

            switch (ShotState)
            {
                case shotState.IDLE:
                    if (time >= weapenProperty.ShotSpeed)
                    {
                        time = 0;
                        createBullet();
                        yield return null;
                    }
                    break;
                case shotState.SINGLE:
                    if (singleShot)
                    {
                        createBullet();
                        singleShot = false;
                        yield return null;
                    }
                    break;
                case shotState.TRIPLE:
                    if (singleShot)
                    {
                        if (bulletCapacity>3)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                createBullet();
                                singleShot = false;
                                yield return null;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < bulletCapacity; i++)
                            {
                                createBullet();
                                singleShot = false;
                                yield return null;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        //创建子弹
        void createBullet()
        {
            AudioSource.PlayClipAtPoint(music[weapenProperty.ID - 1], transform.position + transform.forward, GameManager.instance.musicEvent.value);
            bulletCapacity--;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2) + weapenStar.localPosition);
            if (Physics.Raycast(ray, out hit, 10000f, ~(1 << 8 | 1 << 11)))
            {
                bullet = Instantiate(bulletPref, bulletPos.position, Quaternion.identity, bulletParent);
                bullet.transform.forward = bulletPos.forward;
                Destroy(bullet, 3);
                print(hit.collider.name);
                bullet.GetComponent<Rigidbody>().AddForce((hit.point - bulletPos.position).normalized * bulletSpeed);
                if (hit.collider.tag == "Enemy" && hit.collider is CapsuleCollider)
                {
                    hit.transform.GetComponent<Enemy.EnemyManager>().Hurt(instance.weapenProperty.Damage);
                }
            }
        }
    }
}
