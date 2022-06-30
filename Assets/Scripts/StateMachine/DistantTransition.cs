using System;
using UnityEngine;

public class DistantTransition : ConditionalTransition
{
    [SerializeField] private float _searchRadius;
    
    protected override bool CheckCondition()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _searchRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Player player) && player == Target)
                return true;
        }

        return false;
    }

    private void OnValidate()
    {
        _searchRadius = Math.Max(_searchRadius, 0);
    }
}
