using UnityEngine;
using System.Collections;

public class GridCell : MonoBehaviour {
	public Transform CellPrefab;
	public Vector3 Size;
	public Transform[,]Grid;
   

	// Use this for initialization
	void Start () {
		CreateGrid();
		SetRandomNumbers();
		SetAdjacents();
	}
	void CreateGrid(){
		Grid = new Transform[(int)Size.x,(int)Size.z];
		for (int x = 0;x<Size.x;x++){
		  	for (int z =0;z<Size.z;z++){
				float height = Random.value/2;
				Transform newCell;
				newCell =(Transform)Instantiate(CellPrefab,new Vector3(x,height,z),Quaternion.identity);
				newCell.name = string.Format("({0},0,{1})",x,z);
				newCell.parent = transform;
                newCell.GetComponent<Cell>().Position = new Vector3(x,0,z);
				Grid[x,z]= newCell;
			}
		}
	}
	void SetRandomNumbers(){
		foreach(Transform child in transform){
			int weight = Random.Range(0,10);
			float red = Random.value;
			float green = Random.value;
			float blue = Random.value;
			Vector4 cellColor = new Vector4(red,green,blue,1);
		    child.GetComponentInChildren<TextMesh>().text = weight.ToString();
			child.GetComponentInChildren<TextMesh>().color = cellColor;
			child.GetComponent<Cell>().Weight = weight;
		}
	}
	
	void SetAdjacents(){
		for (int x = 0;x<Size.x;x++){
		  	for (int z =0;z<Size.z;z++){
				Transform cell;
				cell = Grid[x,z];
				Cell cScript = cell.GetComponent<Cell>();
				if(x-1 >= 0){
					cScript.Adjacents.Add(Grid[x-1,z]);
			 	}
				if(x+1 < Size.x){
					cScript.Adjacents.Add(Grid[x+1,z]);
			 	}
				if(z-1 >= 0){
					cScript.Adjacents.Add(Grid[x,z-1]);
			 	}
				if(z+1 < Size.z){
					cScript.Adjacents.Add(Grid[x,z+1]);
			 	}
				//http://docs.unity3d.com/Documentation/ScriptReference/Array.Sort.html
				cScript.Adjacents.Sort(SortByLowestWeight);
			}
		}
	}
	//http://msdn.microsoft.com/en-us/library/0e743hdt.aspx
	int SortByLowestWeight(Transform inputA,Transform inputB){
		int a = inputA.GetComponent<Cell>().Weight;
		int b = inputB.GetComponent<Cell>().Weight;
		return a.CompareTo(b);
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.F1)){
			Application.LoadLevel(0);
		}
	}
}


