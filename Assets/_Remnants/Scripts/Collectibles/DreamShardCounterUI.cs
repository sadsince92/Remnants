
using UnityEngine;
using TMPro;

public class DreamShardCounterUI : MonoBehaviour
{
    #region UI References
    [SerializeField] private TextMeshProUGUI dreamShardText;
    #endregion

    #region Counter
    private int dreamShardCount = 0;
    #endregion

    private void Start()
    {
        UpdateCounter();
    }

    public void AddDreamShard()
    {
        dreamShardCount++;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        dreamShardText.text = dreamShardCount.ToString();
    }
}
