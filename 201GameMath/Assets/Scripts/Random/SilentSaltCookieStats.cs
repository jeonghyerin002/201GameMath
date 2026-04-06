using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;


public class SilentSaltCookieStats : MonoBehaviour
{
    public int totalHits = 0;
    public int criticalHits = 0;
    public float targetRate = 0.3f;

    int damage = 30;
    int criticalDamage = 60;
    int currentDamage;

    public TextMeshProUGUI totalAttackCountText;
    public TextMeshProUGUI criticalCountText;
    public TextMeshProUGUI criticalPercentText;
    public TextMeshProUGUI currentCriticalPercentText;

    [Header("Professor Setting")]
    public int professorHealth = 300;
    public TextMeshProUGUI professorHealthText;
    public int currentProfessorHealth;
    public GameObject ProfessorImage;

    Image pr;
    Transform pt;
    bool isColorChanging = false;

    [Header("Grade Setting")]
    public float gradeF = 0.5f;
    public float gradeC = 0.3f;
    public float gradeB = 0.15f;
    public float gradeA = 0.05f;

    float currentF, currentC, currentB, currentA;

    [Header("Grade UI")]
    public TextMeshProUGUI gradeFText;
    public TextMeshProUGUI gradeCText;
    public TextMeshProUGUI gradeBText;
    public TextMeshProUGUI gradeAText;

    int dropGradeF = 0;
    int dropGradeC = 0;
    int dropGradeB = 0;
    int dropGradeA = 0;

    public TextMeshProUGUI dropGradeFText;
    public TextMeshProUGUI dropGradeCText;
    public TextMeshProUGUI dropGradeBText;
    public TextMeshProUGUI dropGradeAText;

    [Header("이히힝")]
    float line_1 = 0.5f;
    float line_2 = 0.5f;
    public GameObject line_1Text;
    public GameObject line_2Text;
    bool isLine_1 = false;
    bool isLine_2 = false;


    void Start()
    {
        currentProfessorHealth = professorHealth;
        pr = ProfessorImage.GetComponent<Image>();
        pt = ProfessorImage.GetComponent<Transform>();

        InitItemRate();
        InitText();
    }

    void InitText()
    {
        professorHealthText.text = ($"{currentProfessorHealth} / {professorHealth}");

        totalAttackCountText.text = ("공격 횟수 : " + totalHits);
        criticalCountText.text = ("치명타 횟수 : " + criticalHits);
        criticalPercentText.text = ("설정한 크리티컬 확률 :" + targetRate);
        currentCriticalPercentText.text = ("발생한 크리티컬 확률 :" + (float)criticalHits / totalHits);

        gradeFText.text = $"F : {currentF * 100:F1}%";
        dropGradeFText.text = $"F : {dropGradeF}";

        gradeCText.text = $"C : {currentC * 100:F1}%";
        dropGradeCText.text = $"C : {dropGradeC}";

        gradeBText.text = $"B : {currentB * 100:F1}%";
        dropGradeBText.text = $"B : {dropGradeB}";

        gradeAText.text = $"A : {currentA * 100:F1}%";
        dropGradeAText.text = $"A : {dropGradeA}";
    }

    public void OnAttack()
    {
        if (currentProfessorHealth <= 0)
        {
            currentProfessorHealth = professorHealth;
            UpdateUI();
            return;
        }

        RollCrit();

        if (currentProfessorHealth <= 0)
        {
            currentProfessorHealth = 0;
            DropItem();
        }

        UpdateUI();
    }

    public bool RollCrit()
    {
        totalHits++;
        float currentRate = 0f;
        
        if (criticalHits > 0)
            currentRate = (float)criticalHits / totalHits;

        if (currentRate < targetRate && (float)(criticalHits + 1) / totalHits > targetRate)
        {
            currentDamage = criticalDamage;
            currentProfessorHealth -= currentDamage;

            criticalHits++;
            return true;
        }

        if (Random.value < targetRate)
        {
            currentDamage = criticalDamage;
            currentProfessorHealth -= currentDamage;

            criticalHits++;
            return true;
        }

        currentDamage = damage;
        currentProfessorHealth -= currentDamage;

        return false;
    }

    public void DropItem()
    {
        if (currentProfessorHealth != 0)
            return;

        Simulate();
    }

    void InitItemRate()
    {
        currentF = gradeF;
        currentC = gradeC;
        currentB = gradeB;
        currentA = gradeA;
    }
    string Line()
    {
        isLine_1 = false;
        isLine_2 = false;

        float r = Random.value;
        string result = string.Empty;

        if (r < line_1)
        {
            result = "line_1";
            isLine_1 = true;
        }
        else if (r > line_2)
        {
            result = "line_2";
            isLine_2 = true;
        }
        return result;
    }

    string Simulate()
    {
        float r = Random.value;
        string result = string.Empty;

        if (r < currentF)
        {
            result = "F";
            dropGradeF++;
            gradeFText.text = $"F : {currentF * 100:F1}%";
            dropGradeFText.text = $"F : {dropGradeF}";
        }
        else if (r < currentF + currentC)
        {
            result = "C";
            dropGradeC++;
            gradeCText.text = $"C : {currentC * 100:F1}%";
            dropGradeCText.text = $"C : {dropGradeC}";
        }
        else if (r < currentF + currentC + currentB)
        {
            result = "B";
            dropGradeB++;
            gradeBText.text = $"B : {currentB * 100:F1}%";
            dropGradeBText.text = $"B : {dropGradeB}";
        }
        else if (r < currentF + currentC + currentB + currentA)
        {
            result = "A";
            dropGradeA++;
            gradeAText.text = $"A : {currentA * 100:F1}%";
            dropGradeAText.text = $"A : {dropGradeA}";
        }

        if (result == "A")
            InitItemRate();
        else
        {
            currentF -= 0.005f;
            currentC -= 0.005f;
            currentB -= 0.005f;
            currentA += 0.015f;

            currentF = Mathf.Max(0, currentF);
            currentC = Mathf.Max(0, currentC);
            currentB = Mathf.Max(0, currentB);

            gradeFText.text = $"B : {currentF * 100:F1}%";
            gradeCText.text = $"C : {currentC * 100:F1}%";
            gradeBText.text = $"B : {currentB * 100:F1}%";
            gradeAText.text = $"A : {currentA * 100:F1}%";

        }
        return result;

    }

    void UpdateUI()
    {
        professorHealthText.text = ($"{currentProfessorHealth} / {professorHealth}");

        totalAttackCountText.text = ("공격 횟수 : " + totalHits);
        criticalCountText.text = ("치명타 횟수 : " + criticalHits);
        criticalPercentText.text = ("설정한 크리티컬 확률 : " + targetRate);
        currentCriticalPercentText.text = ("발생한 크리티컬 확률 : " + (float)criticalHits / totalHits);
        
        if (!isColorChanging)
            StartCoroutine(FlashRed());

        StartCoroutine(AppearLine());

    }
    IEnumerator FlashRed()
    {
        Line();

        isColorChanging = true;

        Color originColor = pr.color;
        Vector2 originPos = pt.transform.position;

        pr.color = Color.red;
        pt.transform.position = originPos + new Vector2(0, 5f);

        yield return new WaitForSeconds(0.1f);

        pr.color = originColor;
        pt.transform.position = originPos;

        isColorChanging = false;

    }
    IEnumerator AppearLine()
    {
        if (isLine_1)
        {
            line_1Text.gameObject.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            line_1Text.gameObject.SetActive(false);

            isLine_1 = false;
        }
        else if (isLine_2)
        {
            line_2Text.gameObject.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            line_2Text.gameObject.SetActive(false);

            isLine_2 = false;
        }
    }

}
