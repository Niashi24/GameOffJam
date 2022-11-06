using System.Collections;
using System.Collections.Generic;
using LS.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

public class BatShootPercentSupplier : MonoBehaviour, IValueSupplier<float>
{
    [SerializeField]
    [Required]
    BatShooter _batShooter;

    public float Value
    {
        get
        {
            if (_batShooter is null) return default;
            return _batShooter.ChargePercent;
        }

        set {}
    }
}
