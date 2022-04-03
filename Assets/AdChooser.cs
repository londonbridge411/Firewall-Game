using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AdChooser : MonoBehaviour
{
    public bool isPanel;
    public Sprite buttonSprite;
    public Material[] possibleAdsMaterials;
    public Sprite[] possibleAdsImages;

    // Start is called before the first frame update
    void Start()
    {
        if (isPanel)
        {
            int number = Random.Range(0, possibleAdsImages.Length);
            print("NUMBER " + number);
            Image image = GetComponent<Image>();
            image.sprite = possibleAdsImages[number];
            GameObject a = new GameObject();
            a.AddComponent<Image>();
            a.GetComponent<Image>().sprite = buttonSprite;
            a.transform.parent = transform;
            a.GetComponent<RectTransform>().anchoredPosition = new Vector3(image.sprite.rect.width, image.sprite.rect.height, 0);
            print(image.sprite.rect.width + " x " + image.sprite.rect.height);
        }
        else
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material = possibleAdsMaterials[Random.Range(0, possibleAdsMaterials.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isPanel)
            {
                int number = Random.Range(0, possibleAdsImages.Length);
                print("NUMBER " + number);
                Image image = GetComponent<Image>();
                image.sprite = possibleAdsImages[number];
                GameObject a = new GameObject();
                a.AddComponent<Image>();
                a.GetComponent<Image>().sprite = buttonSprite;
                a.transform.parent = transform;
                a.GetComponent<RectTransform>().anchoredPosition = new Vector3(image.sprite.rect.width, image.sprite.rect.height, 0);
                print(image.sprite.rect.width + " x " + image.sprite.rect.height);
            }
            else
            {
                MeshRenderer renderer = GetComponent<MeshRenderer>();
                renderer.material = possibleAdsMaterials[Random.Range(0, possibleAdsMaterials.Length)];
            }
        }
    }
}
