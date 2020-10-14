using UnityEngine;
using DG.Tweening;
using UnityEngine;
public class MoveBucket : MonoBehaviour
{
    //Vector3 leftPos = new Vector3(-280, -380, 0);
    //Vector3 rightPos = new Vector3(280, -380, 0);
    readonly int leftX = -250;
    readonly int rightX = 250;
    readonly int y = -500;
    int _forward = 1;
    float speed = 1;
    public float _speed = 1;
    public static MoveBucket _instance;
    public static MoveBucket Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("MoveBucket").GetComponent<MoveBucket>();
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TableGameConfig config = TableGameConfigList.inst.list[0];
        speed = (float)config.MoveSpeed;
        _speed = speed;
        //gameObject.transform.localPosition = leftPos;
        //transform.DOLocalMove(rightPos, 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="multiple"></param>
    public void SetSpeed(float multiple)
    {
        _speed = speed * multiple;
    }
    // Update is called once per frame
    void Update()
    {
        if (_forward > 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + Time.deltaTime * _speed, y, 0);
            if (transform.localPosition.x >= rightX)
            {
                _forward = -1;
            }
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x - Time.deltaTime * _speed, y, 0);
            if (transform.localPosition.x <= leftX)
            {
                _forward = 1;
            }
        }

    }
}
