using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ChooseAttack : MonoBehaviour
{
    // attacks
    public TextMeshProUGUI laser;
    public TextMeshProUGUI fireball;
    public TextMeshProUGUI bomb;

    // colors
    public Color defaultColor;
    public Color selected;

    // private
    private TextMeshProUGUI previous = null;
    private List<TextMeshProUGUI> attacks;
    private int currIndex = 0; // laser

    void Start()
    {
        previous = laser;
        attacks = new List<TextMeshProUGUI> {laser, fireball, bomb};

        laser.color = selected;
        fireball.color = defaultColor;
        bomb.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            if (currIndex - 1 >= 0) {
                currIndex--;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            if (currIndex + 1 < 3) {
                currIndex++;
            }
        }

        if (attacks[currIndex] != previous) {
            attacks[currIndex].color = selected;
            previous.color = defaultColor;
            previous = attacks[currIndex];
        }
    }

    // returns 0: laser, 1: fireball, 2: bomb
    public int GetCurrentAttack() {
        return currIndex;
    }
}
