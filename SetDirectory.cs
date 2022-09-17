using String = MyLib.String;

namespace MyLib
{
    public class SetDirectory
    {
        public string? Path { get; private set; }
        public SetDirectory(string? path)
        {
            if(path == null) { return; }
            SetPath(path);
        }
        public void AddFolderIfNotExist(String folderName)
        {
            Directory.CreateDirectory($"{Path}\\{folderName}");
        }
        public void SetPath(String path)
        {
            Path = path;
        }
        public bool Exist(String fileName)
        {
            return Path == null ? false : new FileInfo($"{Path}\\{fileName}").Exists;
        }
        public void WriteText(String fileName, object data)
        {
            File.WriteAllText($"{Path}\\{fileName}", data.ToString());
        }
        public void AppendText(String fileName, object data)
        {
            File.AppendAllText($"{Path}\\{fileName}", data.ToString());
        }
        public string ReadText(String fileName)
        {
            if (!Exist(fileName))
            {
                return "";
            }
            return File.ReadAllText($"{Path}\\{fileName}");
        }
        public string LastWriteTime(String fileName)
        {
            FileInfo info = new FileInfo($"{Path}\\{fileName}");
            var t = info.LastWriteTime;
            return Time.ToString(t);
        }
    }
}
