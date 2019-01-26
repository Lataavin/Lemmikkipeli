using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalData : ScriptableObject
{
    [SerializeField]
    private List<PatternDefinition> _patterns = new List<PatternDefinition>();
    [SerializeField]
    private Material _defaultPattern;
    [SerializeField]
    private List<SpriteDefinition> _visuals = new List<SpriteDefinition>();
    [SerializeField]
    private AnimatorOverrideController _defaultVisuals;

    public void SetVisuals(Animator animator, SpriteRenderer renderer)
    {
        animator.runtimeAnimatorController = SpriteDefinition.GetDefinition(_visuals);
        if (animator.runtimeAnimatorController == null)
        {
            animator.runtimeAnimatorController = _defaultVisuals;
        }
        renderer.material = PatternDefinition.GetDefinition(_patterns);
        if (renderer.material = null)
        {
            renderer.material = _defaultPattern;
        }
    }
}
