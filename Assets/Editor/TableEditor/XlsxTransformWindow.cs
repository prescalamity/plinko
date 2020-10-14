using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Excel;
using LitJson;
using System;
using UnityEditor;
using OfficeOpenXml;
public class XlsxTransformWindow : EditorWindow
{
    [UnityEditor.MenuItem("Tools/Xlsx转换工具")]
    static void OpenWin()
    {
        XlsxTransformWindow win = EditorWindow.GetWindow<XlsxTransformWindow>("Xlsx转换工具");
        win.Show();
    }
    string _loadPath;
    string loadPath
    {
        get
        {
            if (_loadPath == null)
            {
                _loadPath = PlayerPrefs.GetString(UnityEngine.Application.productName + "xlsxTfPath", UnityEngine.Application.dataPath);
            }
            return _loadPath;
        }
        set
        {
            _loadPath = value;
            PlayerPrefs.SetString(UnityEngine.Application.productName + "xlsxTfPath", value);
        }
    }
    string tempPath;
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("文档所在文件夹");
        loadPath = GUILayout.TextField(loadPath);
        if (GUILayout.Button("选择目录"))
        {
            loadPath = EditorUtility.OpenFolderPanel("选择文件夹", loadPath, "excel");
            //			FolderBrowserDialog fb = new FolderBrowserDialog();   //创建控件并实例化
            //			fb.Description = "选择文件夹";
            //			fb.RootFolder = Environment.SpecialFolder.Desktop;//loadPath;//Environment.SpecialFolder.MyComputer;  //设置默认路径
            //			fb.ShowNewFolderButton = false;   //创建文件夹按钮关闭
            //			//如果按下弹窗的OK按钮
            //			if (fb.ShowDialog () == DialogResult.OK) {
            //				//接受路径
            //				tempPath = fb.SelectedPath; 
            //			}
            //			//将路径中的 \ 替换成 /            由于unity路径的规范必须转
            //			loadPath= tempPath.Replace(@"\", "/");
            ////			print(UnityPath);
            ////			loadPath = 
            //			tempPath = loadPath;
        }
        GUILayout.EndHorizontal();
        if (GUILayout.Button("转换"))
        {
            Transform();
            AssetDatabase.Refresh();
        }
        GUILayout.TextArea(err, GUILayout.Height(500));
    }
    public string uncode(string source)
    {
        return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase).Replace(
            source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
    }
    int tInd = 0;
    string err = "";

    void Transform()
    {
        if (!Directory.Exists(Application.dataPath + "/Scripts/TableCS"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Scripts/TableCS");
        }
        if (!Directory.Exists(loadPath + "/table"))
        {
            Directory.CreateDirectory(loadPath + "/table");
        }
        string[] ss = Directory.GetFiles(loadPath);
        for (int k = 0; k < ss.Length; k++)
        {
            if (Path.GetExtension(ss[k]) == ".xlsx")
            {
                try
                {
                    using (ExcelPackage package = new ExcelPackage(new FileInfo(ss[k])))
                    {
                        ExcelWorksheet worksheet_Data = package.Workbook.Worksheets["Sheet1"];
                        //根据sheet名获取Sheet
                        int rows = worksheet_Data.Dimension.End.Row; //行
                        int columns = worksheet_Data.Dimension.End.Column; //列

                        JsonData jd = new JsonData();
                        jd["list"] = new JsonData();
                        JsonData jd1;
                        string csName = Path.GetFileNameWithoutExtension(ss[k]);
                        string csString = classFormat.Replace("{0}", csName);
                        string[] proNames = new string[columns];
                        string[] proTypes = new string[columns];
                        string[] _proTypes = new string[columns];
                        for (int i = 1; i < columns + 1; i++)
                        {
                            string proString = "" + proFormat;
                            for (int j = 1; j < 4; j++)
                            {
                                string nvalue = Convert.ToString((worksheet_Data.GetValue(j, i)));
                                if (j == 2)
                                {
                                    _proTypes[i - 1] = nvalue;
                                }
                                if (nvalue.Equals("string_E"))
                                {
                                    nvalue = "double";
                                }
                                if (j == 1)
                                {
                                    proNames[i - 1] = nvalue;
                                }
                                else if (j == 2)
                                {
                                    proTypes[i - 1] = nvalue;
                                }
                                proString = proString.Replace("{" + (j - 1) + "}", nvalue);
                            }
                            csString = csString.Replace("{1}", proString + @"
	{1}");
                        }
                        csString = csString.Replace("{1}", "");
                        File.WriteAllText(Application.dataPath + "/Scripts/TableCS/" + csName + ".cs", csString);
                        int tempInt;
                        Double tempFloat;
                        bool tempBool;
                        List<int> tempListInt;
                        List<double> tempListDouble;
                        List<string> tempListString;
                        string _js = "";
                        for (int i = 4; i < rows + 1; i++)
                        {
                            jd1 = new JsonData();
                            for (int j = 1; j < columns + 1; j++)
                            {
                                string nvalue = Convert.ToString((worksheet_Data.GetValue(i, j)));
                                //							if(i==0){
                                switch (_proTypes[j - 1])
                                {
                                    case "string":
                                        jd1[proNames[j - 1]] = nvalue;
                                        break;
                                    case "int":
                                        tempInt = 0;
                                        int.TryParse(nvalue, out tempInt);
                                        jd1[proNames[j - 1]] = tempInt;
                                        break;
                                    case "float":
                                    case "double":
                                        tempFloat = 0;
                                        Double.TryParse(nvalue, out tempFloat);
                                        jd1[proNames[j - 1]] = tempFloat;
                                        break;
                                    case "bool":
                                        jd1[proNames[j - 1]] = nvalue.Replace("\"", "");
                                        break;
                                    case "string_E":
                                        string _bigInt = "";
                                        if (nvalue.Contains("E"))
                                        {
                                            int plusIndex = nvalue.IndexOf('+');
                                            string bigInt = nvalue.Remove(plusIndex, 1);
                                            string[] _strs = bigInt.Split('E');
                                            float q = 0;
                                            float.TryParse(_strs[0], out q);
                                            int h = int.Parse(_strs[1]);
                                            q = q * 10000;
                                            h = h - 4;
                                            _bigInt = "" + (int)q;
                                            for (int l = 0; l < h; l++)
                                            {
                                                _bigInt += "0";
                                            }
                                        }
                                        else
                                        {
                                            _bigInt = nvalue;
                                        }
                                        double d = double.Parse(_bigInt);
                                        jd1[proNames[j - 1]] = d;
                                        break;
                                }
                                _js += jd1.ToString();
                            }
                            jd["list"].Add(jd1);
                        }
                        File.WriteAllText(loadPath + "/table/" + csName + ".txt", uncode(jd.ToJson()));
                        Debug.LogError("输出" + csName + "完成" + 1);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                    err = e.ToString();
                }
            }
        }
    }

    private Decimal ChangeDataToD(string strData)
    {
        Decimal dData = 0.0M;
        if (strData.Contains("E"))
        {
            dData = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));

        }
        else
        {
            dData = Decimal.Parse(strData);
        }
        return dData;
    }

    string proFormat = @"/// <summary>
	/// {2}
	/// </summary>
	public {1} {0};";
    string classFormat = @"using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class {0}{
	{1}
}
[System.Serializable]
      public class {0}List{
      	public List<{0}> list;
          
          public {0}List (){}
      	static {0}List _inst;
      	public static {0}List inst{
      		get{
      			if (_inst == null) {
                    TextAsset ta = (TextAsset)Resources.Load(""Table/table/{0}"");
      				if (ta != null && !string.IsNullOrEmpty (ta.text)) {
      					_inst = JsonHelper.FromJson<{0}List> (ta.text);
      				} else {
      					_inst = new {0}List ();
      				}
      			}
      			return _inst;
      		}
      	}
      }";
}
