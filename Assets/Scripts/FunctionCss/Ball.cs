using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Transform pos = null;
    PhysicsMaterial2D material = null;//材质球
    bool isAdd = false;
    /// <summary>
    /// 第一次碰撞加球
    /// </summary>
    public bool IsAdd { get => isAdd; set => isAdd = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// 设置弹力
    /// </summary>
    /// <param name="force"></param>
    public void SetElasticForce(float force)
    {
        if (material == null)
        {
            material = GetComponent<CircleCollider2D>().sharedMaterial;
        }
        material.bounciness = force;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        pos = collision.gameObject.transform.parent;
        if (collision.gameObject.name.Equals("Bottom"))
        {
            OnDestroy();
        }
        else if (collision.gameObject.name.Equals("Bottom1"))
        {
            OnDestroy();
            BottleManager.Instance.Reward(0, pos);
        }
        else if (collision.gameObject.name.Equals("Bottom2"))
        {
            OnDestroy();
            BottleManager.Instance.Reward(1, pos);
        }
        else if (collision.gameObject.name.Equals("Bottom3"))
        {
            OnDestroy();
            BottleManager.Instance.Reward(2, pos);
        }
        else if (collision.gameObject.name.Equals("Bottom4"))
        {
            OnDestroy();
            BottleManager.Instance.Reward(3, pos);
        }
        else if (collision.gameObject.name.Contains("Obstacle"))
        {
            if (isAdd)
            {
                isAdd = false;
                ObstacleManager.Instance.AddObstacle();
            }

        }
        AudioManager.Instance.PlayAudioEffect("Click");
    }
    private void OnDestroy()
    {
        PoolManager.Instance.Enter(gameObject);
    }
}
