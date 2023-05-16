//using System.Text.Json;
//using System.Text.Json.Serialization;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Xml;
using System.Xml.Serialization;
using static System.Environment; //using static: Importiert den Inhalt einer Klasse (keines Namespaces) in die bestehenden Klassen (hier Program). Alle Member sind dann in der Klasse verwendbar

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		string desktop = GetFolderPath(SpecialFolder.DesktopDirectory);
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//SystemJson();

		//NewtonsoftJson();

		//Xml();

		//CSV();
	}

	public static void SystemJson()
	{
		string desktop = GetFolderPath(SpecialFolder.DesktopDirectory);
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new PKW(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//JsonSerializerOptions options = new(); //Einstellungen müssen beim De-/Serialisieren übergeben werden
		//options.WriteIndented = true; //JSON schön schreiben
		//options.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Kreisbezüge ignorieren

		//string json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson, options);

		//////////////////////////////////////////////////////////

		////Json per Hand durchgehen (ohne ein Objekt zu erstellen)
		//JsonDocument doc = JsonDocument.Parse(readJson);
		//JsonElement.ArrayEnumerator ae = doc.RootElement.EnumerateArray();
		//foreach (JsonElement je in ae)
		//{
		//	Console.WriteLine(je.GetProperty("ID").GetInt32());

		//	Fahrzeug f = je.Deserialize<Fahrzeug>(); //Einzelnes Json Element zu Fahrzeug konvertieren
		//	Console.WriteLine(f.MaxV);
		//}

		//Edit -> Paste Special -> Paste Json as Classes
	}

	public static void NewtonsoftJson()
	{
		string desktop = GetFolderPath(SpecialFolder.DesktopDirectory);
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new PKW(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//JsonSerializerSettings settings = new();
		//settings.Formatting = Formatting.Indented; //JSON schön schreiben
		//settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Kreisbezüge ignorieren
		//settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung beachten beim De-/serialisieren

		//string json = JsonConvert.SerializeObject(fahrzeuge, settings);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson, settings);

		////////////////////////////////////////////////////////////

		//JToken document = JToken.Parse(readJson);
		//foreach (JToken jt in document)
		//{
		//	Console.WriteLine(jt["MaxV"].Value<int>()); //Mit [] auf Felder zugreifen (statt GetProperty()), mit Value<T> konvertieren (statt Get...())

		//	Fahrzeug f = JsonConvert.DeserializeObject<Fahrzeug>(jt.ToString());
		//}

		//Edit -> Paste Special -> Paste Json as Classes
	}

	public static void Xml()
	{
		string desktop = GetFolderPath(SpecialFolder.DesktopDirectory);
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			xml.Serialize(sw, fahrzeuge);
		} //Hier wird der Stream geschlossen (Dispose wird aufgerufen)

		using StreamReader sr = new StreamReader(filePath);
		List<Fahrzeug> readFzg = xml.Deserialize(sr) as List<Fahrzeug>; //as-Cast: gibt keine Exception wenn der Cast invalide ist (gibt null zurück)
																		//List<Fahrzeug> readFzg2 = (List<Fahrzeug>) xml.Deserialize(sr); //normaler Cast: wirft eine Exception, wenn der Cast invalide ist

		/////////////////////////////////////////////////////////////////////////

		XmlDocument doc = new XmlDocument();
		doc.LoadXml(File.ReadAllText(filePath));

		foreach (XmlNode node in doc.ChildNodes[1]) //Hier Header überspringen mit [1]
		{
			Console.WriteLine(node.ChildNodes[1].InnerText);

			Console.WriteLine(node.ChildNodes.OfType<XmlNode>().First(e => e.Name == "MaxV").InnerText); //Node per Name finden

			Console.WriteLine(node.Attributes[0].Value);
		}
	}

	public static void CSV()
	{
		string desktop = GetFolderPath(SpecialFolder.DesktopDirectory);
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		// CSV generieren
		//foreach (Fahrzeug f in fahrzeuge)
		//{
		//	string s = f.GetType().GetProperties().Aggregate("", (agg, prop) => agg + prop.GetValue(f) + ";").TrimEnd(';') + "\n";
		//	File.AppendAllText(filePath, s);
		//}

		List<Fahrzeug> readFzg = new();
		TextFieldParser tfp = new TextFieldParser(filePath);
		//tfp.ReadLine(); //Header überspringen falls vorhanden
		tfp.SetDelimiters(";");
		while (!tfp.EndOfData)
		{
			Fahrzeug f = new();
			string[] fields = tfp.ReadFields();
			f.ID = int.Parse(fields[0]);
			f.MaxV = int.Parse(fields[1]);
			f.Marke = Enum.Parse<FahrzeugMarke>(fields[2]);
			readFzg.Add(f);
		}
	}
}

//Vererbung mit System.Text.Json
//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]
public class Fahrzeug
{
	//Xml
	//[XmlIgnore] //Feld ignorieren
	[XmlAttribute]
	public int ID { get; set; }

	//Newtonsoft.Json
	//[JsonIgnore] //Feld ignorieren
	//[JsonProperty("Maximalgeschwindigkeit")]
	public int MaxV { get; set; }

	//System.Text.Json
	//[JsonIgnore] //Feld ignorieren
	//[JsonPropertyName("M")] //Name vom Feld bearbeiten
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int iD, int maxV, FahrzeugMarke marke)
	{
		ID = iD;
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        //Für XML
    }
}

public class PKW : Fahrzeug
{
	public PKW(int iD, int maxV, FahrzeugMarke marke) : base(iD, maxV, marke) { }

    public PKW() { }
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}