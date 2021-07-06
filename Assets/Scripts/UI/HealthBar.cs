using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image bar;
    private void Awake()
    {
        GetComponentInParent<Character>().OnHealthChanged += ChangedHealthBar;
    }
    public void ChangedHealthBar(float hp)
    {
        bar.fillAmount = hp;
    }
}
