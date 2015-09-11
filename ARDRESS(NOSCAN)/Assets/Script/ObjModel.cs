public class ObjModel{
	int id;
	string name, obj, mtl, png;	
	public ObjModel(){}
	public ObjModel(int id, string name, string obj, string mtl, string png) {
		this.id = id;
		this.name = name;
		this.obj = obj;
		this.mtl = mtl;
		this.png = png;
	}
	public int Id 
	{
		get 
		{ 
			return id; 
		}
		set 
		{
			id = value; 
		}
	}
	public string Name 
	{
		get 
		{ 
			return name; 
		}
		set 
		{
			name = value; 
		}
	}
	public string Obj 
	{
		get 
		{ 
			return obj; 
		}
		set 
		{
			obj = value; 
		}
	}
	public string Mtl 
	{
		get 
		{ 
			return mtl; 
		}
		set 
		{
			mtl = value; 
		}
	}
	public string Png 
	{
		get 
		{ 
			return png; 
		}
		set 
		{
			png = value; 
		}
	}
}
