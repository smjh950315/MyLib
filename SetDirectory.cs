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
        public void AddFolderIfNotExist(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }            
        }
        public void SetPath(string path)
        {
            Path = path;
        }
        public bool Exist(string fileName)
        {
            return Path == null ? false : new FileInfo($"{Path}/{fileName}").Exists;
        }
        public void WriteText(string fileName, object data)
        {
            File.WriteAllText($"{Path}/{fileName}", data.ToString());
        }
        public void AppendText(string fileName, object data)
        {
            File.AppendAllText($"{Path}/{fileName}", data.ToString());
        }
        public string ReadText(string fileName)
        {
            return File.ReadAllText($"{Path}/{fileName}");
        }
        public string LastWriteTime(string fileName)
        {
            FileInfo info = new FileInfo($"{Path}/{fileName}");
            var t = info.LastWriteTime;
            return Time.ToString(t);
        }
    }
}
