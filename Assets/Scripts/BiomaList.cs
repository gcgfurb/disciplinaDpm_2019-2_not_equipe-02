﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BiomaList : MonoBehaviour {

    private Bioma[] biomas;
    private Plant[] plants;
    public GameObject[] buttons;

    void Start() {
        biomas = JsonReader.LoadBiomas();
        plants = JsonReader.LoadPlants();

        LoadBiomaButtons();
    }

    private void LoadBiomaButtons() {
        for(int i = 0; i < biomas.Length; i++) {
            buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(biomas[i].image);
            buttons[i].GetComponentInChildren<Text>().text = biomas[i].name;
        }
    }

    public void LoadScene(Text name) {
        GameObject biomaGameObject = new GameObject("Bioma");
        Bioma bioma = GetBiomaByName(name.text);

        BiomaController controller = biomaGameObject.AddComponent<BiomaController>();
        controller.bioma = bioma;
        controller.plantsList = plants;

        BiomaState state = biomaGameObject.AddComponent<BiomaState>();
        state.bioma = bioma;

        DontDestroyOnLoad(controller);
        DontDestroyOnLoad(state);
        SceneManager.LoadScene("Bioma");
    }

    private Bioma GetBiomaByName(string name) {
        foreach(Bioma bioma in biomas) {
            if(bioma.name == name)
                return bioma;
        }
        return null;
    }
}
