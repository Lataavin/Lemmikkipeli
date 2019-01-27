using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI _scoreLabel;
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        _scoreLabel.text = string.Format("Score: {0}, Combo: {1}", WorldManager.instance.GameScore, WorldManager.instance.ExtraScore);
    }
}
