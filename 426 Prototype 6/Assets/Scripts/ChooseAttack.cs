using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

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

    public void SetAttack(int attackType, int amount) {
        TextMeshProUGUI attack = null;
        string attackString = "";
        switch (attackType) {
            case 0:
                attack = laser;
                attackString = "Laser: ";
                break;
            case 1:
                attack = fireball;
                attackString = "Fireball: ";
                break;
            case 2:
                attack = bomb;
                attackString = "Bomb: ";
                break;
        }
        string[] parts = attack.text.Split(new string[] { ": " }, StringSplitOptions.None);
        int num = int.Parse(parts[1]);
        num += amount;
        attack.text = attackString + num;
    }

    // returns true if you cna attack and false if you can't
    public bool LaserAttack() {
        string[] parts = laser.text.Split(new string[] { ": " }, StringSplitOptions.None);
        int num = int.Parse(parts[1]);
        if (num == 0) {
            return false;
        }
        num--;
        laser.text = "Laser: " + num;
        return true;
    }

    public bool FireballAttack() {
        string[] parts = fireball.text.Split(new string[] { ": " }, StringSplitOptions.None);
        int num = int.Parse(parts[1]);
        if (num == 0) {
            return false;
        }
        num--;
        fireball.text = "Fireball: " + num;
        return true;
    }

    public bool BombAttack() {
        string[] parts = bomb.text.Split(new string[] { ": " }, StringSplitOptions.None);
        int num = int.Parse(parts[1]);
        if (num == 0) {
            return false;
        }
        num--;
        bomb.text = "Bomb: " + num;
        return true;
    }
}
