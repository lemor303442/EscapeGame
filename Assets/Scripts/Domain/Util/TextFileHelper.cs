using System.IO;
using System.Text;

public class TextFileHelper
{
    /// <summary>
    /// Reads the text file.
    /// </summary>
    /// <param name="path">Path.</param>
    public static string Read(string path)
    {
        FileInfo fileInfo = new FileInfo(Const.Path.ablsolutePath + path);
        string content = "";
        if (fileInfo.Exists)
        {
            StreamReader sr = new StreamReader(fileInfo.OpenRead(), Encoding.UTF8);
            content = sr.ReadToEnd();
            sr.Close();
        }
        else
        {
            UnityEngine.Debug.LogWarning("File does not exist: " + Const.Path.ablsolutePath + path);
        }
        return content;
    }

    /// <summary>
    /// Write the specified path and content.
    /// </summary>
    /// <returns>The write.</returns>
    /// <param name="path">Path.</param>
    /// <param name="content">Content.</param>
    public static void Write(string path, string content)
    {
        string[] directries = path.Split('/');
        for (int i = 0; i < directries.Length - 1; i++)
        {
            string tmpPath = "";
            for (int j = 0; j < i + 1; j++)
            {
                tmpPath += directries[j] + "/";
            }
            tmpPath = tmpPath.Remove(tmpPath.Length - 1, 1);
            if (!Directory.Exists(Const.Path.ablsolutePath + tmpPath))
            {
                Directory.CreateDirectory(Const.Path.ablsolutePath + tmpPath);
            }
        }
        FileInfo fileInfo = new FileInfo(Const.Path.ablsolutePath + path);
        FileStream fs;
        if (fileInfo.Exists)
        {
            fs = new FileStream(Const.Path.ablsolutePath + path, FileMode.Create);
        }
        else
        {
            fs = new FileStream(Const.Path.ablsolutePath + path, FileMode.CreateNew);
        }
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(content);
        sw.Flush();
        sw.Close();
    }

    /// <summary>
    /// If the file exists at given path.
    /// </summary>
    /// <returns><c>true</c>, if file exists, <c>false</c> otherwise.</returns>
    /// <param name="path">Path.</param>
    public static bool IsExist(string path){
        FileInfo fileInfo = new FileInfo(Const.Path.ablsolutePath + path);
        return fileInfo.Exists;
    }
}
