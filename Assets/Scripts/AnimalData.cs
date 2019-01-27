using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AnimalSoundDefinition
{
    [SerializeField]
    public AnimatorOverrideController _animal;    
    [SerializeField]
    public AudioClip _pickupSound;
}

[CreateAssetMenu(fileName = "AnimalData.asset", menuName = "Animal/AnimalData")]
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
    [SerializeField] 
    private List<AnimalSoundDefinition> _animalSounds = new List<AnimalSoundDefinition>();

    public void SetVisuals(Animator animator, SpriteRenderer renderer)
    {
        animator.runtimeAnimatorController = SpriteDefinition.GetDefinition(_visuals);
        if (animator.runtimeAnimatorController == null)
        {
            animator.runtimeAnimatorController = _defaultVisuals;
        }
        renderer.material = PatternDefinition.GetDefinition(_patterns);
        if (renderer.material == null)
        {
            renderer.material = _defaultPattern;
        }
    }

    public AudioClip GetAnimalSound(string animalAnimName)
    {
        var definition = _animalSounds.FirstOrDefault(x => x._animal.name == animalAnimName);
        return definition != null ? definition._pickupSound : null;
    }
    
}
