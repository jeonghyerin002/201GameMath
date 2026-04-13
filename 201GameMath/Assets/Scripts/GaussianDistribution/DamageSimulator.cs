using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DamageSimulator : MonoBehaviour
{
    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI logDisplay;
    public TextMeshProUGUI resultDisplay;
    public TextMeshProUGUI rangeDisplay;

    int level = 1;
    float totalDamage = 0, baseDamage = 20f;
    int attackCount = 0;

    string weaponName;
    float stdDevMult, critRate, critMult;

    void Start()
    {
        SetWeapon(0);
    }
    void ResetData()
    {
        totalDamage = 0;
        attackCount = 0;
        level = 1;
        baseDamage = 20f;
    }
    public void SetWeapon(int id)
    {
        ResetData();
        if (id == 0)
            SetStatus("단검", 0.1f, 0.4f, 1.5f);
        else if (id == 1)
            SetStatus("장검", 0.2f, 0.3f, 4.0f);
        else
            SetStatus("도끼", 0.3f, 0.2f, 3.0f);

        logDisplay.text = string.Format("{0} 장착!", weaponName);
        UpdataUI();


    }
    public void SetStatus(string _name, float _stdDev, float _critRate, float _critMult)
    {
        weaponName = _name;
        stdDevMult = _stdDev;
        critRate = _critRate;
        critMult = _critMult;
    }
    public void LevelUp()
    {
        totalDamage = 0;
        attackCount = 0;
        level++;
        baseDamage = level * 20f;
        logDisplay.text = string.Format("레벨업! 현재 레벨 : {0}", level);
        UpdataUI();
    }
    public void OnAttack()
    {
        //정규분포 데미지 계산
        float sd = baseDamage * stdDevMult;
        float normalDamage = GetNormalStdDevDamage(baseDamage, sd);

        //치명타 판정
        bool isCrit = Random.value < critRate;
        float finalDamage = isCrit ? normalDamage * critMult : normalDamage;

        //통계 누적
        attackCount++;
        totalDamage += finalDamage;

        //로그 및 UI 업데이트
        string critMark = isCrit ? "<color=red>[치명타!]<color> ":"";
        logDisplay.text = string.Format("{0}데미지 : {1F1", critMark, finalDamage);
        UpdataUI();
    }
    void UpdataUI()
    {
        statusDisplay.text = string.Format("Level : {0} / 무기 : {1}\n기본 데미지 : {2} / 치명타 : {3}% (x{4}))",
            level, weaponName, baseDamage, critRate * 100, critMult);
        rangeDisplay.text = string.Format("예상 일반 데미지 범위 : [{0:F1} ~ {1:F1}]",
            baseDamage - (3 * baseDamage * stdDevMult),
            baseDamage + (3 * baseDamage * stdDevMult));

        float dpa = attackCount > 0 ? totalDamage / attackCount : 0;
        resultDisplay.text = string.Format("누적 데미지 : {0:F1}\n공격 횟수 : {1}\n평균 DPA : {2:F2}",
            totalDamage, attackCount, dpa);
    }
    float GetNormalStdDevDamage(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
        return mean + stdDev * randStdNormal;
    }
}
