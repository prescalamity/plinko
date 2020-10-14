using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : Inst<ObstacleManager>
{
    List<GameObject> obstaclesGoList = new List<GameObject>();
    /// <summary>
    /// 障碍对象脚本集合
    /// </summary>
    List<Obstacle> csLists = new List<Obstacle>();
    /// <summary>
    /// 障碍数据
    /// </summary>
    List<_Obstacle> _obstacles = new List<_Obstacle>();
    /// <summary>
    /// 总权重
    /// </summary>
    int totalWeight = 0;
    /// <summary>
    /// 金币和美金权重和
    /// </summary>
    int weight2 = 0;
    /// <summary>
    /// 障碍表数据
    /// </summary>
    List<TableObstacle> TableObstacles = new List<TableObstacle>();
    /// <summary>
    /// 隐藏的障碍id
    /// </summary>
    List<int> hideId = new List<int>();
    /// <summary>
    /// 黑色障碍最多个数
    /// </summary>
    int obsMax = 0;
    /// <summary>
    /// 场上黑色障碍个数
    /// </summary>
    int obsCount = 0;
    /// <summary>
    /// 高级金币美金场上最多数量
    /// </summary>
    int seniorCoinMax, seniorDollarMax = 0;
    /// <summary>
    /// Ads刷新金币美金权重
    /// </summary>
    int obsCoinWeight, obsDollarWeight, obsTotalWeight = 0;
    /// <summary>
    /// Ads全部黑色障碍刷新金币美金权重
    /// </summary>
    int _obsCoinWeight, _obsDollarWeight, _obsTotalWeight = 0;

    private void Start()
    {
        DelegateCenter.addCoinOrDollarBlock += SetCoinOrDollarObs;
        DelegateCenter.coinReplaceAllBlock += AllObsTo;
    }
    public void Init(GameObject obstacles)
    {
        TableObstacles = TableObstacleList.inst.list;
        obsMax = TableObstacles[2].Max;
        foreach (var item in TableObstacles)
        {
            totalWeight += item.Weight;
            if (item.Type != 2)
            {
                weight2 += item.Weight;
            }
        }
        for (int i = 1; i < 49; i++)
        {
            GameObject obstacle = obstacles.transform.Find("Obstacle" + i).gameObject;
            obstaclesGoList.Add(obstacle);
            Obstacle _obstacle = obstacle.GetComponent<Obstacle>();
            csLists.Add(_obstacle);
        }
        if (GameData.Obstacles.Equals(""))
        {
            InitObstacle();
        }
        else
        {
            RestorationObstacle();
        }
        seniorCoinMax = TableObstacles[3].Max;
        seniorDollarMax = TableObstacles[4].Max;

        List<TableBuff> buffs = TableBuffList.inst.list;
        string[] strs = StringUtils.Str(buffs[1].Weight, '_');
        obsCoinWeight = int.Parse(strs[0]);
        obsDollarWeight = int.Parse(strs[1]);
        obsTotalWeight = obsCoinWeight + obsDollarWeight;
        strs = StringUtils.Str(buffs[4].Weight, '_');
        _obsCoinWeight = int.Parse(strs[0]);
        _obsDollarWeight = int.Parse(strs[1]);
        _obsTotalWeight = _obsCoinWeight + _obsDollarWeight;
    }
    /// <summary>
    /// 初始化障碍物
    /// </summary>
    void InitObstacle()
    {
        int rowNumner = 0;
        for (int i = 0; i < csLists.Count; i++)
        {
            int id = i;
            rowNumner = csLists[id].rowNumber;
            ObstacleType obstacleType;
            if (id == 0 || id == 5 || id == 36 || id == 40)
            {
                obstacleType = GetObstacle2Type();
            }
            else
            {
                obstacleType = GetObstacleType(rowNumner);
            }
            Obstacle obstacle = csLists[i];
            int hp = TableObstacles[(int)obstacleType].Hp;
            obstacle.SetHp(hp, hp);
            obstacle.Init(obstacleType, id);
        }
    }
    /// <summary>
    /// 随机障碍类型
    /// </summary>
    /// <returns></returns>
    ObstacleType GetObstacleType(int rowNumber)
    {
        ObstacleType obstacleType = ObstacleType.Coin;
        if (!IsMaxObs(rowNumber) && obsCount != obsMax)
        {
            TableObstacle tableObstacle = null;
            int index = Random.Range(0, totalWeight);
            int weight = 0;
            for (int j = 0; j < TableObstacles.Count; j++)
            {
                tableObstacle = TableObstacles[j];
                weight += tableObstacle.Weight;
                if (index < weight)
                {
                    obstacleType = (ObstacleType)tableObstacle.Type;
                    break;
                }
            }
        }
        else if (obsCount == obsMax || IsMaxObs(rowNumber))
        {
            obstacleType = GetObstacle2Type();
        }
        if (GameControl.Instance.IsLimit() && obstacleType == ObstacleType.Dollar)
        {
            obstacleType = ObstacleType.Coin;
        }
        if (obstacleType == ObstacleType.Obstacle)
            obsCount++;
        return obstacleType;
    }
    /// <summary>
    /// 随机金币美金类型
    /// </summary>
    /// <returns></returns>
    ObstacleType GetObstacle2Type()
    {
        TableObstacle tableObstacle = null;
        ObstacleType obstacleType = ObstacleType.Coin;
        int weight = 0;
        int index = Random.Range(0, weight2);
        for (int i = 0; i < TableObstacles.Count; i++)
        {
            tableObstacle = TableObstacles[i];
            weight += tableObstacle.Weight;
            if (index < weight)
            {
                obstacleType = (ObstacleType)tableObstacle.Type;
                break;
            }
        }
        if (GameControl.Instance.IsLimit() && obstacleType == ObstacleType.Dollar)
        {
            obstacleType = ObstacleType.Coin;
        }
        return obstacleType;
    }
    /// <summary>
    /// 是否一行黑色障碍物达到最多限制
    /// </summary>
    /// <param name="rowNumber">行数</param>
    /// <returns></returns>
    bool IsMaxObs(int rowNumber)
    {
        bool isMax = false;
        int count = 0;
        foreach (Obstacle item in csLists)
        {
            if (item.rowNumber == rowNumber && item.obstacleType == ObstacleType.Obstacle)
            {
                count++;
                if (count >= 3)
                {
                    isMax = true;
                    break;
                }
            }
        }
        return isMax;
    }
    /// <summary>
    /// 恢复障碍
    /// </summary>
    void RestorationObstacle()
    {
        _obstacles = JsonHelper.FromJson<List<_Obstacle>>(GameData.Obstacles);
        foreach (_Obstacle item in _obstacles)
        {
            Obstacle _ob = csLists[item.id];
            _ob.id = item.id;
            _ob.obstacleType = item.obstacleType;
            _ob.SetHp(item.hp, item.currentHp);
            _ob.Init(_ob.obstacleType, _ob.id);
            if (item.obstacleType == ObstacleType.Obstacle)
            {
                obsCount++;
            }
        }
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    /// <param name="id"></param>
    public void Hide(int id)
    {
        hideId.Add(id);
        obstaclesGoList[id].SetActive(false);
    }
    /// <summary>
    /// 保存数据
    /// </summary>
    public void SaveData()
    {
        _obstacles.Clear();
        foreach (Obstacle item in csLists)
        {
            _Obstacle _Obstacle = new _Obstacle
            {
                id = item.id,
                obstacleType = item.obstacleType,
                hp = item.hp,
                currentHp = item.currentHp
            };
            _obstacles.Add(_Obstacle);
        }
        GameData.Obstacles = JsonHelper.ToJson(_obstacles);
    }
    /// <summary>
    /// 添加障碍
    /// </summary>
    public void AddObstacle()
    {
        if (hideId.Count > 0)
        {
            int index = Random.Range(0, hideId.Count);
            int id = hideId[index];
            int rowNumber = csLists[id].rowNumber;
            ObstacleType obstacleType;
            if (id == 0 || id == 5 || id == 36 || id == 40)
            {
                obstacleType = GetObstacle2Type();
            }
            else
            {
                obstacleType = GetObstacleType(rowNumber);
            }
            Obstacle obstacle = csLists[id];
            int hp = TableObstacles[(int)obstacleType].Hp;
            obstacle.SetHp(hp, hp);
            obstacle.Init(obstacleType, id);
            hideId.Remove(id);
        }
    }
    List<int> ADOList = new List<int>();//↓用
    List<int> _ADOList = new List<int>();//↓用
    /// <summary>
    /// 刷新美金障碍
    /// </summary>
    /// <param name="count">数量</param>
    public void AddDollarObstacle(int count)
    {
        ADOList = GetObsIndex(ObstacleType.Obstacle);
        if (ADOList.Count == count)//要增加数量刚好和黑色障碍相等
        {
            foreach (int id in ADOList)
            {
                AddDollarObs(id);
            }
        }
        else if (ADOList.Count > count)//要增加数量小于黑色障碍
        {
            _ADOList.Clear();
            for (int i = 0; i < ADOList.Count; i++)
            {
                _ADOList.Add(ADOList[i]);
            }
            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, _ADOList.Count);
                int id = _ADOList[index];
                _ADOList.RemoveAt(index);
                AddDollarObs(id);
            }
        }
        else//要增加数量大于黑色障碍
        {
            if (ADOList.Count == 0)//没有黑色障碍
            {
                SurplusDollar(count);
            }
            else//有黑色障碍
            {
                int _count = count - ADOList.Count;//除去黑色障碍剩余数量
                foreach (int id in ADOList)
                {
                    AddDollarObs(id);
                }
                SurplusDollar(_count);
            }
        }
    }
    List<int> SDList = new List<int>();//↓用
    List<int> _SDList = new List<int>();//↓用
    /// <summary>
    /// 剩余的美金
    /// </summary>
    /// <param name="_count">数量</param>
    void SurplusDollar(int _count)
    {
        if (_count > hideId.Count)//剩余数量大于空位
        {
            _count -= hideId.Count;
            foreach (int id in hideId)
            {
                AddDollarObs(id);
            }
            hideId.Clear();
            SDList = GetObsIndex(ObstacleType.Coin);
            if (SDList.Count == 0) return;
            for (int i = 0; i < SDList.Count; i++)
            {
                for (int j = i + 1; j < SDList.Count - 1; j++)
                {
                    if (csLists[SDList[i]].currentHp > csLists[SDList[j]].currentHp)
                    {
                        int cur = SDList[i];
                        SDList[i] = SDList[j];
                        SDList[j] = cur;
                    }
                }
            }
            for (int i = 0; i < _count; i++)
            {
                int id = SDList[0];
                AddDollarObs(id);
                SDList.RemoveAt(0);
                if (SDList.Count == 0) return;
            }
        }
        else//剩余数量小于空位
        {
            _SDList.Clear();
            for (int i = 0; i < hideId.Count; i++)
            {
                _SDList.Add(hideId[i]);
            }
            for (int i = 0; i < _count; i++)
            {
                int index = Random.Range(0, _SDList.Count);
                int id = _SDList[index];
                _SDList.RemoveAt(index);
                AddDollarObs(id);
                hideId.Remove(id);
                if (hideId.Count == 0) return;
            }
        }
    }
    /// <summary>
    /// 添加美金障碍
    /// </summary>
    /// <param name="id"></param>
    void AddDollarObs(int id)
    {
        Obstacle obstacle = csLists[id];
        int hp = TableObstacles[(int)ObstacleType.Dollar].Hp;
        obstacle.SetHp(hp, hp);
        obstacle.Init(ObstacleType.Dollar, id);
    }
    List<int> obsIndex = new List<int>();//↓用
    /// <summary>
    /// 获取某类障碍全部的下标
    /// </summary>
    /// <param name="obstacleType">类型</param>
    /// <returns></returns>
    List<int> GetObsIndex(ObstacleType obstacleType)
    {
        obsIndex.Clear();
        for (int i = 0; i < csLists.Count; i++)
        {
            ObstacleType type = csLists[i].obstacleType;
            if (obstaclesGoList[i].activeInHierarchy && type == obstacleType)
            {
                obsIndex.Add(i);
            }
        }
        return obsIndex;
    }
    List<int> cdIndexs = new List<int>();//↓用
    List<int> _cdIndexs = new List<int>();//↓用
    /// <summary>
    /// 升级金币障碍
    /// </summary>
    /// <param name="count">数量</param>
    public void UpgradCoin(int count)
    {
        cdIndexs = GetObsIndex(ObstacleType.SeniorCoin);
        if (cdIndexs.Count >= seniorCoinMax) return;
        cdIndexs = GetObsIndex(ObstacleType.Coin);
        if (cdIndexs.Count != 0)
        {
            _cdIndexs.Clear();
            for (int i = 0; i < cdIndexs.Count; i++)
            {
                _cdIndexs.Add(cdIndexs[i]);
            }
            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, _cdIndexs.Count);
                Obstacle obstacle = csLists[_cdIndexs[index]];
                int hp = TableObstacles[(int)ObstacleType.SeniorCoin].Hp;
                obstacle.SetHp(hp, hp);
                obstacle.Init(ObstacleType.SeniorCoin, obstacle.id);
                _cdIndexs.RemoveAt(index);
                if (_cdIndexs.Count == 0)
                {
                    break;
                }
            }
        }
    }
    /// <summary>
    /// 升级美金障碍
    /// </summary>
    /// <param name="count">数量</param>
    public void UpgradDollar(int count)
    {
        cdIndexs = GetObsIndex(ObstacleType.SeniorDollar);
        if (cdIndexs.Count >= seniorDollarMax) return;
        cdIndexs = GetObsIndex(ObstacleType.Dollar);
        if (cdIndexs.Count != 0)
        {
            _cdIndexs.Clear();
            for (int i = 0; i < cdIndexs.Count; i++)
            {
                _cdIndexs.Add(cdIndexs[i]);
            }
            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, _cdIndexs.Count);
                Obstacle obstacle = csLists[_cdIndexs[index]];
                int hp = TableObstacles[(int)ObstacleType.SeniorDollar].Hp;
                obstacle.SetHp(hp, hp);
                obstacle.Init(ObstacleType.SeniorDollar, obstacle.id);
                _cdIndexs.RemoveAt(index);
                if (_cdIndexs.Count == 0)
                {
                    break;
                }
            }
        }
    }
    List<int> AOTList = new List<int>();//↓用
    List<int> _AOTList = new List<int>();//↓用
    /// <summary>
    /// 全部黑的障碍刷新成
    /// </summary>
    public void AllObsTo()
    {
        int ran = 0;
        int hp = 0;
        ObstacleType type = 0;
        AOTList = GetObsIndex(ObstacleType.Obstacle);
        if (AOTList.Count != 0)
        {
            for (int i = 0; i < AOTList.Count; i++)
            {
                Obstacle obstacle = csLists[AOTList[i]];
                ran = Random.Range(0, _obsTotalWeight);
                if (ran < _obsCoinWeight)
                    type = ObstacleType.Coin;
                else
                    type = ObstacleType.Dollar;
                hp = TableObstacles[(int)type].Hp;
                obstacle.SetHp(hp, hp);
                obstacle.Init(type, obstacle.id);
            }
        }
    }
    List<int> OTCODList = new List<int>();//↓用
    List<int> _OTCODList = new List<int>();//↓用
    List<int> OTCLists1 = new List<int>();//↓
    /// <summary>
    /// 广告随机刷新金币美金
    /// </summary>
    /// <param name="count">数量</param>
    public void ObsToCoinOrDollar(int count)
    {
        OTCODList = GetObsIndex(ObstacleType.Obstacle);
        if (count > OTCODList.Count)
        {
            if (OTCODList.Count != 0)
            {
                int _count = count - OTCODList.Count;
                for (int i = 0; i < OTCODList.Count; i++)
                {
                    SetCoinOrDollarObs(csLists[OTCODList[i]]);
                }
                SetCoinOrDollarObs(_count);
            }
            else
            {
                SetCoinOrDollarObs(count);
            }
        }
        else
        {
            for (int i = 0; i < OTCODList.Count; i++)
            {
                OTCLists1.Add(OTCODList[i]);
            }
            for (int i = 0; i < count; i++)
            {
                int index = Random.Range(0, OTCLists1.Count);
                SetCoinOrDollarObs(csLists[OTCLists1[index]]);
                OTCLists1.RemoveAt(index);
            }
        }
    }
    /// <summary>
    /// 设置金币或美金障碍
    /// </summary>
    /// <param name="obstacle"></param>
    void SetCoinOrDollarObs(Obstacle obstacle)
    {
        int ran = 0;
        int hp = 0;
        ObstacleType type = 0;
        if (GameControl.Instance.IsLimit())
        {
            type = ObstacleType.Coin;
        }
        else
        {
            ran = Random.Range(0, obsTotalWeight);
            if (ran < obsCoinWeight)
                type = ObstacleType.Coin;
            else
                type = ObstacleType.Dollar;
        }
        hp = TableObstacles[(int)type].Hp;
        obstacle.SetHp(hp, hp);
        obstacle.Init(type, obstacle.id);
    }
    List<int> OTCLists2 = new List<int>();//↓
    /// <summary>
    /// 设置金币或美金障碍
    /// </summary>
    /// <param name="_count">数量</param>
    void SetCoinOrDollarObs(int _count)
    {
        int ran = 0;
        if (_count < hideId.Count)
        {
            OTCLists2.Clear();
            for (int i = 0; i < hideId.Count; i++)
            {
                OTCLists2.Add(hideId[i]);
            }
            for (int i = 0; i < _count; i++)
            {
                ran = Random.Range(0, OTCLists2.Count);
                int id = OTCLists2[ran];
                SetCoinOrDollarObs(csLists[id]);
                hideId.Remove(id);
                OTCLists2.RemoveAt(ran);
            }
        }
        else
        {
            for (int i = 0; i < hideId.Count; i++)
            {
                SetCoinOrDollarObs(csLists[hideId[i]]);
            }
            hideId.Clear();
        }
    }
    int r = 0;
    /// <summary>
    /// 炸弹
    /// </summary>
    /// <param name="id">炸弹id</param>
    public void Bomb(int id)
    {
        if (r == 0)
        {
            r = (int)TableObstacleList.inst.list[5].Rewad;
            //r = 500;
        }
        for (int i = 0; i < csLists.Count; i++)
        {
            if (obstaclesGoList[i].activeInHierarchy && csLists[i].id != id)
            {
                Vector3 v1 = obstaclesGoList[i].transform.localPosition;
                Vector3 v2 = obstaclesGoList[id].transform.localPosition;
                float jl = (v1 - v2).magnitude;
                if (jl <= r)
                {
                    //Debug.LogError("i="+i+"id="+id+"r=" + r + "jl=" + jl);
                    csLists[i].CalculateHpAddMoney();
                }
            }
        }
    }
    List<int> ids = new List<int>();
    /// <summary>
    /// 加炸弹
    /// </summary>
    public void AddBomb()
    {
        ids = GetObsIndex(ObstacleType.Obstacle);
        if (ids.Count != 0)
        {
            int index = Random.Range(0, ids.Count);
            int id = ids[index];
            Obstacle obstacle = csLists[id];
            ObstacleType type = ObstacleType.Prop;
            int hp = TableObstacles[(int)type].Hp;
            obstacle.SetHp(hp, hp);
            obstacle.Init(type, obstacle.id);
        }
        else if (hideId.Count != 0)
        {
            int index = Random.Range(0, hideId.Count);
            int id = hideId[index];
            Obstacle obstacle = csLists[id];
            ObstacleType type = ObstacleType.Prop;
            int hp = TableObstacles[(int)type].Hp;
            obstacle.SetHp(hp, hp);
            obstacle.Init(type, id);
            hideId.Remove(id);
        }
        else
        {
            ids = GetObsIndex(ObstacleType.Coin);
            if (ids.Count != 0)
            {
                int index = Random.Range(0, ids.Count);
                int id = ids[index];
                Obstacle obstacle = csLists[id];
                ObstacleType type = ObstacleType.Prop;
                int hp = TableObstacles[(int)type].Hp;
                obstacle.SetHp(hp, hp);
                obstacle.Init(type, obstacle.id);
            }
            else
            {
                ids = GetObsIndex(ObstacleType.Dollar);
                int index = Random.Range(0, ids.Count);
                int id = ids[index];
                Obstacle obstacle = csLists[id];
                ObstacleType type = ObstacleType.Prop;
                int hp = TableObstacles[(int)type].Hp;
                obstacle.SetHp(hp, hp);
                obstacle.Init(type, id);
            }
        }
    }
}
