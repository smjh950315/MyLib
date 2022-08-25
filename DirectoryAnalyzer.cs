using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class DirectoryAnalyzer
    {
        public bool LogWithFileSystemInfo { get; set; }
        public string Path { get; private set; }
        public List<string> FileList { get; private set; }
        public List<FileSystemInfo> FileSystemEntries { get; private set; }
        public DirectoryInfo Directory { get; private set; }
        public DirectoryAnalyzer(string path)
        {
            Path = path;
            FileList = new List<string>();
            Directory = new DirectoryInfo(Path);
            FileSystemEntries = new List<FileSystemInfo>();
            LogWithFileSystemInfo = false;
        }
        public void ScanFileEntries()
        {
            Directory = new DirectoryInfo(Path);
            var dir = Directory.GetFileSystemInfos().ToList();
            foreach (FileSystemInfo fsi in dir)
            {
                Iterator(fsi);
            }
        }
        public void Iterator(FileSystemInfo fsi)
        {
            if (fsi.Attributes.Equals(FileAttributes.Directory))
            {
                var dirInfo = new DirectoryInfo(fsi.FullName+"\\");
                var dir = dirInfo.GetFileSystemInfos();
                if (dir != null)
                {
                    foreach (FileSystemInfo fsi2 in dir)
                    {
                        Iterator(fsi2);
                    }
                }
                if (LogWithFileSystemInfo)
                {
                    FileSystemEntries.Add(fsi);
                }                
                FileList.Add(fsi.FullName);                
            }
            else
            {
                if (LogWithFileSystemInfo)
                {
                    FileSystemEntries.Add(fsi);
                }
                FileList.Add(fsi.FullName);
            }
        }

        public void WriteFileList(string path)
        {
            foreach(string f in FileList)
            {
                File.AppendAllText(path, f+"\n");
            }
        }
    }

}
