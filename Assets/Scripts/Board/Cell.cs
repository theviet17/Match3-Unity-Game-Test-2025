using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Cell : MonoBehaviour
{
    public int BoardX { get; private set; }

    public int BoardY { get; private set; }

    public Item Item { get; private set; }
    
    public Cell NeighbourUp { get; set; }

    public Cell NeighbourRight { get; set; }

    public Cell NeighbourBottom { get; set; }

    public Cell NeighbourLeft { get; set; }

    
    public bool IsEmpty => Item == null;

    public void Setup(int cellX, int cellY)
    {
        this.BoardX = cellX;
        this.BoardY = cellY;
    }

    public bool IsNeighbour(Cell other)
    {
        return BoardX == other.BoardX && Mathf.Abs(BoardY - other.BoardY) == 1 ||
            BoardY == other.BoardY && Mathf.Abs(BoardX - other.BoardX) == 1;
    }
    
    public void Free()
    {
        Item = null;
    }

    public void Assign(Item item)
    {
        Item = item;
        Item.SetCell(this);
    }

    public void ApplyItemPosition(bool withAppearAnimation)
    {
        Item.SetViewPosition(this.transform.position);

        if (withAppearAnimation)
        {
            Item.ShowAppearAnimation();
        }
    }

    internal void Clear()
    {
        if (Item != null)
        {
            Item.Clear();
            Item = null;
        }
    }

    internal bool IsSameType(Cell other)
    {
        return Item != null && other.Item != null && Item.IsSameType(other.Item);
    }

    internal void ExplodeItem()
    {
        if (Item == null) return;

        Item.ExplodeView();
        Item = null;

        //Item = null;
    }

    internal void AnimateItemForHint()
    {
        Item.AnimateForHint();
    }

    internal void StopHintAnimation()
    {
        Item.StopAnimateForHint();
    }

    internal void ApplyItemMoveToPosition()
    {
        Item.AnimationMoveToPosition();
    }
    public NormalItem.eNormalType GetNormalType()
    {
        string cellName = this.gameObject.name;
        switch (cellName)
        {
            case Constants.PREFAB_NORMAL_TYPE_ONE:
                return NormalItem.eNormalType.TYPE_ONE;
            case Constants.PREFAB_NORMAL_TYPE_TWO:
                return NormalItem.eNormalType.TYPE_TWO;
            case Constants.PREFAB_NORMAL_TYPE_THREE :
                return NormalItem.eNormalType.TYPE_THREE;
            case Constants.PREFAB_NORMAL_TYPE_FOUR:
                return NormalItem.eNormalType.TYPE_FOUR;
            case Constants.PREFAB_NORMAL_TYPE_FIVE:
                return NormalItem.eNormalType.TYPE_FIVE;
            case Constants.PREFAB_NORMAL_TYPE_SIX :
                return NormalItem.eNormalType.TYPE_SIX;
            case Constants.PREFAB_NORMAL_TYPE_SEVEN :
                return NormalItem.eNormalType.TYPE_SEVEN;
        }
        return NormalItem.eNormalType.TYPE_SEVEN;
    }
}
