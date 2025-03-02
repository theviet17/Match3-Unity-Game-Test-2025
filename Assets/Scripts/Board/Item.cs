using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class Item
{
    public Cell Cell { get; private set; }

    public Transform View { get; private set; }
    
    public SpriteCollection m_spriteCollection;

    public SpriteRenderer m_rend;
    
    private Board m_board;
    
    public void Init(SpriteCollection spriteCollection , GameObject prefab ,Board board)
    {
        m_board  = board;
        m_spriteCollection = spriteCollection;
        string emptyPrefab = Constants.PREFAB_EMPTY;
        
        View = GameObject.Instantiate(prefab).transform;
        m_rend = View.GetComponent<SpriteRenderer>();
        
        View.gameObject.SetActive(false);
    }
    public virtual void SetView()
    {
         
        string prefabname = GetPrefabName();
        Sprite sprite = GetSprite();
       // SS

        if (!string.IsNullOrEmpty(prefabname))
        {
            View.name = prefabname;
            View.gameObject.SetActive(true);
            View.transform.localScale = Vector3.one;
            // GameObject prefab = Resources.Load<GameObject>(prefabname);
            // if (prefab)
            // {
            //     View = GameObject.Instantiate(prefab).transform;
            // }
        }
        if (sprite != null)
        {
            m_rend.sprite = sprite;
        }
        //m_normalType = normalType;
    }

    protected virtual string GetPrefabName() { return string.Empty; }
    
    protected virtual Sprite GetSprite() { return null; }
    
    
    
    public virtual void SetCell(Cell cell)
    {
        Cell = cell;
    }

    internal void AnimationMoveToPosition()
    {
        if (View == null) return;

        View.DOMove(Cell.transform.position, 0.2f);
    }

    public void SetViewPosition(Vector3 pos)
    {
        if (View)
        {
            View.position = pos;
            
        }
    }

    public void SetViewRoot(Transform root)
    {
        if (View)
        {
            View.SetParent(root);
        }
    }

    public void SetSortingLayerHigher()
    {
        if (View == null) return;

        SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 1;
        }
    }


    public void SetSortingLayerLower()
    {
        if (View == null) return;

        SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 0;
        }

    }

    internal void ShowAppearAnimation()
    {
        if (View == null) return;

        Vector3 scale = View.localScale;
        View.localScale = Vector3.one * 0.1f;
        View.DOScale(scale, 0.1f);
    }

    internal virtual bool IsSameType(Item other)
    {
        return false;
    }

    internal virtual void ExplodeView()
    {
        if (View)
        {
            View.DOScale(0.1f, 0.1f)
                .OnComplete(
                () =>
                {
                    Release();
                }
                );
        }
    }
    public void Release()
    {
        if (this is BonusItem)
        {
            m_board.m_bonusItemPool.Release((BonusItem)this);
        }
        else if (this is NormalItem)
        {
            m_board.m_normalItemPool.Release((NormalItem)this);
        }
        this.View.gameObject.SetActive(false);
    }



    internal void AnimateForHint()
    {
        if (View)
        {
            View.DOPunchScale(View.localScale * 0.1f, 0.1f).SetLoops(-1);
        }
    }

    internal void StopAnimateForHint()
    {
        if (View)
        {
            View.DOKill();
        }
    }

    internal void Clear()
    {
        Cell = null;

        if (View)
        {
            GameObject.Destroy(View.gameObject);
            View = null;
        }
    }
}
