using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSDosNote.Domain.Interfaces
{
    public interface INoteRepository
    {
        void AddNote(string content);
        void ClearNotes();
        void BackupNotes();
        string GetNotes();
        void OpenNewNotebook();
        string GetNotesFolder();
    }
}
