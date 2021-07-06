using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Slider expBar;
    [SerializeField] private Text lvlText;
    [SerializeField] private float[] maxCountLvlAndExpForHim;
    private int maxLvl;
    public static int curLevel;
    private void Awake()
    {
        GetComponent<Player>().OnExpChanged += ChangedExp;
    }

    private void Start()
    {
        curLevel = 0;
        maxLvl = maxCountLvlAndExpForHim.Count();
    }
    private void ChangedExp(float exp)
    {
        if (maxLvl != curLevel + 1)
        {
            expBar.SetValueWithoutNotify(exp);
            CheckLvl();
        }
    }

    private void CheckLvl()
    {
        try
        {
            if (expBar.value >= maxCountLvlAndExpForHim[curLevel + 1])
            {
                curLevel++;
                expBar.minValue = maxCountLvlAndExpForHim[curLevel];
                expBar.maxValue = maxCountLvlAndExpForHim[curLevel + 1];
                lvlText.text = $"Level: {curLevel + 1}";
            }
        }
        catch
        {
            expBar.minValue = 0;
            lvlText.text = $"Level: Max";
        }
    }
}
