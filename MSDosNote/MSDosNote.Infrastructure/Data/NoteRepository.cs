using MSDosNote.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSDosNote.Infrastructure.Data
{
    public class NoteRepository : INoteRepository
    {
        private string _filePath;
        private string _fileName;
        private string _fullPath;
        public NoteRepository()
        {
            _filePath = Properties.Settings.Default.FilePath;
            _fileName = Properties.Settings.Default.FileName;
            _fullPath = _filePath + _fileName;
            Directory.CreateDirectory(_filePath);
            if (!File.Exists(_fileName))
            {
                using (File.Open(_fullPath, FileMode.OpenOrCreate))
                {
                    // Do nothing
                }
            }
        }

        public void AddNote(string content)
        {
            using (StreamWriter streamWriter = File.AppendText(_fullPath))
            {
                streamWriter.WriteLine(ProcessContent(content));
            }
        }

        public void BackupNotes()
        {
            string backupFileName = GenerateFileNameWithTimestamp() + ".bak";
            File.Copy(_fullPath, backupFileName);
        }

        public void ClearNotes()
        {
            if (File.Exists(_fullPath))
            {
                File.Delete(_fullPath);
                using (File.Open(_fullPath, FileMode.OpenOrCreate))
                {
                    // Do nothing
                }
            }
        }

        public string GetNotes()
        {
            return _fullPath;
        }

        public void OpenNewNotebook()
        {
            string archivedNoteFile = GenerateFileNameWithTimestamp();
            File.Copy(_fullPath, archivedNoteFile);
            ClearNotes();
        }

        private string GenerateFileNameWithTimestamp()
        {
            string datetimeString = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            return _filePath + datetimeString + "-" + _fileName;
        }

        public string ProcessContent(string content)
        {
            DateTime operationTime = DateTime.Now;
            string prefix = operationTime.ToString("dd/MM/yyyy hh:mm");
            return prefix + "\t" + content;
        }

        public string GetNotesFolder()
        {
            return _filePath;
        }
    }
}
