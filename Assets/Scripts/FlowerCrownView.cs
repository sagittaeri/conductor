using UnityEngine;
using UnityEngine;

public class FlowerCrownView : MonoBehaviour {
    public FlowerColorer[] flower1s;
    public FlowerColorer[] flower2s;
    public FlowerColorer[] flower3s;

    public Color red;
    public Color blue;
    public Color magenta;
    public Color cyan;

    public void Reset() {
        HideFlowers(1);
        HideFlowers(2);
        HideFlowers(3);
    }

    public void HideFlowers(int set) {
        var flowers = flower1s;
        if (set == 2) {
            flowers = flower2s;
        } else if (set == 3) {
            flowers = flower3s;
        }
        
        foreach(var flower in flowers) {
            flower.gameObject.SetActive(false);
        }
    }

    public void SetFlowers(int set, Color color) {
        var flowers = flower1s;
        if (set == 2) {
            flowers = flower2s;
        } else if (set == 3) {
            flowers = flower3s;
        }
        Debug.Log("Got flowers");

        foreach(var flower in flowers) {
            Debug.Log("Setting flower");
            flower.gameObject.SetActive(true);
            Debug.Log("Setting flower");
            flower.SetColor(color);
            Debug.Log("Set flower");
        }
    }

    public void SetFlowers(string chain) {
        string[] flowers = chain.Split(',');

        Reset();
        for(int i = 0; i < flowers.Length; i++) {
            Debug.Log(flowers[i] + ": " + i);
            Color c = new Color(1,1,1,0);
            switch(flowers[i]) {
                case "red":
                    c = red;
                    break;
                case "green":
                    c = cyan;
                    break;
                case "blue":
                    c = blue;
                    break;
                case "magenta":
                    c = magenta;
                    break;
            }
            Debug.Log(c);
            SetFlowers(i + 1, c);
        }
    }
}