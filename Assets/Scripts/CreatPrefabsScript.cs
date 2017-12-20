using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreatPrefabsScript : MonoBehaviour {

    public GameObject prefab;
    List<GameObject> cubes;
    List<GameObject> sides;
    GameObject gameCtrl;
    public  int x, y;
    public List<GameObject> all;
    
	void Start () {
        cubes = new List<GameObject>();
        sides = new List<GameObject>();
        gameCtrl = GameObject.Find("GameCtrl");
        Spawn();
	}
	//生成
    public void Spawn()
    {
        int num = (x * y - 2 * (x + y) + 4)/2;
        for (int i = 0; i < num; i++)
        {
            int id = Random.Range(1, 10);
            GameObject obj = Instantiate(prefab);
            GameObject obj1 = Instantiate(prefab);
            obj.transform.SetParent(gameCtrl.transform);
            obj1.transform.SetParent(gameCtrl.transform);
            obj.GetComponentInChildren<TextMesh>().text = id.ToString();
            obj1.GetComponentInChildren<TextMesh>().text = id.ToString();
            obj.name = id.ToString();
            obj1.name = id.ToString();
           
            cubes.Add(obj);
            cubes.Add(obj1);
        }
        for (int i = 0; i < x*y-num; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(gameCtrl.transform);
            obj.GetComponent<MeshRenderer>().enabled = false;
            obj.name = "none";
            sides.Add(obj);
        }
        Create();
    }
    //布局
    public void Create()
    {
        for (int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                if (i == 0 || j == 0 || i == y - 1 || j == x - 1)
                { 
                    int num = Random.Range(0,sides.Count);
                    sides[num].transform.position = new Vector3((i-x/2)*1.1f,(j-2)*1.1f, 0);
                    all.Add(sides[num]);

                    sides.RemoveAt(num);
                } else
                {
                    int num = Random.Range(0, cubes.Count);
                    cubes[num].transform.position = new Vector3((i - x / 2) * 1.1f, (j -2) * 1.1f, 0);
                    all.Add(cubes[num]);

                    cubes.RemoveAt(num);
                }
            }
        }
    }
}
