using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{

    private float XP = 0;
    public float maxXp;
    public int level = 1;
    private ExperienceBar xpbar;
    private TextMeshProUGUI levelsText;
    private StatManager statManager;

    // Start is called before the first frame update
    private void Start()
    {
        levelsText = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        xpbar = FindAnyObjectByType<ExperienceBar>();
        statManager = GetComponent<StatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        xpbar.SetMaxExperience(maxXp);
        xpbar.setExperience(XP);
        levelsText.text = "LV." + level;

        if (XP >= maxXp)
        {
            if (XP > maxXp)
            {
                XP -= maxXp;
            }
            else
            {
                XP = 0;
            }
            LevelUp();
        }
    }
    private void LevelUp()
    {
        level++;
        maxXp *= 1.5f;
        statManager.RaiseStats();
    }
    public void GainXP(float xpAmount)
    {
        XP += xpAmount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        Debug.Log(collision.name);
        if (collision.gameObject.GetComponent<ExperienceScript>())
        {
            Debug.Log(collision.name + "william");
            var xp = collision.gameObject.GetComponent<ExperienceScript>();
            xp.pickUp();
        }
    }
}
