using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KichenObjectSO input;
    public KichenObjectSO output;

    public int cuttingProgressMax;
}
