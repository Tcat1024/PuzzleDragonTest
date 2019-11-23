using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum EBoardItemType
{
    FireBall,
    WaterBall,
    WindBall,
    LightBall,
    DarkBall,
    HealBall,
    PoisonBall,
    VenomBall,
    DisturbBall,



    Count
}


public interface IBoardItemFactory
{
    IBoardItem Generate();
}

public class BoardItemFactory : Singleston<BoardItemFactory>
{
    private Dictionary<EBoardItemType, IBoardItemFactory> m_RegistedFactories = new Dictionary<EBoardItemType, IBoardItemFactory>();

    private void Init()
    {
        m_RegistedFactories.Add(EBoardItemType.FireBall, new BallFactory<Ball>(EBoardItemType.FireBall));
        m_RegistedFactories.Add(EBoardItemType.WaterBall, new BallFactory<Ball>(EBoardItemType.WaterBall));
        m_RegistedFactories.Add(EBoardItemType.WindBall, new BallFactory<Ball>(EBoardItemType.WindBall));
        m_RegistedFactories.Add(EBoardItemType.LightBall, new BallFactory<Ball>(EBoardItemType.LightBall));
        m_RegistedFactories.Add(EBoardItemType.DarkBall, new BallFactory<Ball>(EBoardItemType.DarkBall));
        m_RegistedFactories.Add(EBoardItemType.HealBall, new BallFactory<Ball>(EBoardItemType.HealBall));
        m_RegistedFactories.Add(EBoardItemType.PoisonBall, new BallFactory<Ball>(EBoardItemType.PoisonBall));
        m_RegistedFactories.Add(EBoardItemType.VenomBall, new BallFactory<Ball>(EBoardItemType.VenomBall));
        m_RegistedFactories.Add(EBoardItemType.DisturbBall, new BallFactory<Ball>(EBoardItemType.DisturbBall));
    }


    public IBoardItem Generate(EBoardItemType itemType)
    {
        IBoardItemFactory factory;
        if(m_RegistedFactories.TryGetValue(itemType, out factory))
        {
            return factory.Generate();
        }
        return null;
    }


    public BoardItemFactory() { Init(); }
}

public class BallFactory<TBall> : IBoardItemFactory where TBall : Ball, new()
{
    private EBoardItemType m_ProductType;

    public IBoardItem Generate()
    {
        var ret = new TBall();
        ret.type = m_ProductType;
        return ret;
    }

    public BallFactory(EBoardItemType tarType)
    {
        m_ProductType = tarType;
    }
}




public interface IBoardItem
{
    EBoardItemType type { get; }
}



public class Ball : IBoardItem
{
    public EBoardItemType type { get; set; }
}

public class Board
{
    private IBoardItem[,] m_BoardItems;


    public void Init(int width, int height)
    {
        m_BoardItems = new IBoardItem[height, width];
    }

}

public class Battle
{
    private Board m_CurBoard;
}

public class BattleController
{
    private Battle m_CurBattle;

    public void Init(Battle tarbattle)
    {
        m_CurBattle = tarbattle;
    }

    public void Start()
    {

    }

    public void Step()
    {

    }

    public bool Move(Vector2Int pos, Vector2Int dir)
    {


        return true;
    }

    private void RandomInitBalls(IBoardItem[,] itemList, List<EBoardItemType> canDropBoardItemTypes, bool allowCancel, bool shouldUseAllType)
    {

    }

    private bool Regularization(IBoardItem[,] itemList)
    {
        bool ret = false;
        for (int x = 0; x < itemList.GetLength(1); ++x)
        {
            int empty = -1;
            for(int y = 0; y < itemList.GetLength(0); ++y)
            {
                if(itemList[y, x] != null)
                {
                    if(empty >= 0)
                    {
                        itemList[empty, x] = itemList[y, x];
                        itemList[y, x] = null;
                        ++empty;
                        ret = true;
                    }
                }
                else
                {
                    if (empty < 0)
                        empty = y;
                }
            }
        }
        return ret;
    }

    private bool[,] m_DispelCheckState;
    private List<List<Tuple<Vector2Int, IBoardItem>>> TryDispel(IBoardItem[,] itemList, int minDispelCount)
    {
        List<List<Tuple<Vector2Int, IBoardItem>>> ret = new List<List<Tuple<Vector2Int, IBoardItem>>>();
        int width = itemList.GetLength(1);
        int height = itemList.GetLength(0);
        if (null == m_DispelCheckState || m_DispelCheckState.GetLength(0) != height || m_DispelCheckState.GetLength(1) != width)
        {
            m_DispelCheckState = new bool[height, width];
        }
        else
        {
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    m_DispelCheckState[y, x] = false;
                }
            }
        }
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                var dispel = TryDispel(x, y, itemList, minDispelCount);
                if (null != dispel)
                {
                    ret.Add(dispel);
                }
            }
        }


        return ret;
    }

    List<Tuple<Vector2Int, IBoardItem>> TryDispel(int tarX, int tarY, IBoardItem[,] itemList, int minDispelCount)
    {
        if (m_DispelCheckState[tarY, tarX])
            return null;
        m_DispelCheckState[tarY, tarX] = true;
        var item = itemList[tarY, tarX];
        if (null == item)
            return null;
        if (tarX + minDispelCount < 1 + itemList.GetLength(1))
        {

        }
        for (int y = 0; y < itemList.GetLength(0); ++y)
        {
            for (int x = tarX; x < itemList.GetLength(1); ++x)
            {
                var item = itemList[x, y];
                if (null == item)
                    continue;
            }
        }


        return ret;
    }

    private bool Drop(IBoardItem[,] itemList, List<EBoardItemType> canDropBoardItemTypes)
    {

    }

    private bool IsSameItem(IBoardItem a, IBoardItem b)
    {
        if (a == null || b == null)
            return false;
        return a.type == b.type;
    }
}