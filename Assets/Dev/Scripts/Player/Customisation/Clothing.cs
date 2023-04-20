using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothing : MonoBehaviour
{
    public GameObject hair;
    public GameObject doneHair;
    public bool hairDone;
    public GameObject shirt;
    public GameObject doneShirt;
    public bool shirtDone;
    public GameObject pants;
    public GameObject donePants;
    public bool pantsDone;
    public GameObject shoes;
    public GameObject doneShoes;
    public bool shoesDone;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OnValidate()
    {
        if(hairDone == false)
        {
            if(doneHair != null)
            {
                GameObject.Destroy(doneHair);
            }
            GameObject hairModel = GameObject.Instantiate<GameObject>(hair);
            doneHair = hairModel;
            hairModel.transform.SetParent(transform);
            hairModel.transform.localPosition = Vector3.zero;
            hairDone = true;
        }
        if(shirtDone == false)
        {
            if(doneShirt != null)
            {
                GameObject.Destroy(doneShirt);
            }
            GameObject shirtModel = GameObject.Instantiate<GameObject>(shirt);
            doneShirt = shirtModel;
            shirtModel.transform.SetParent(transform);
            shirtModel.transform.localPosition = Vector3.zero;
            shirtDone = true;
        }
        if(pantsDone == false)
        {
            if(donePants != null)
            {
                GameObject.Destroy(donePants);
            }
            GameObject pantsModel = GameObject.Instantiate<GameObject>(pants);
            donePants = pantsModel;
            pantsModel.transform.SetParent(transform);
            pantsModel.transform.localPosition = Vector3.zero;
            pantsDone = true;
        }
        if(shoesDone == false)
        {
            if(doneShoes != null)
            {
                GameObject.Destroy(doneShoes);
            }
            GameObject shoesModel = GameObject.Instantiate<GameObject>(shoes);
            doneShoes = shoesModel;
            shoesModel.transform.SetParent(transform);
            shoesModel.transform.localPosition = Vector3.zero;
            shoesDone = true;
        }
    }
}
