using MSDosNote.Domain.Constants;
using MSDosNote.Domain.Exceptions;
using MSDosNote.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSDosNote.App.Consolos
{
    public class Application
    {
        INoteRepository _repository;

        public Application(INoteRepository repository)
        {
            _repository = repository;
        }

        public void Run(string[] args)
        {
            AddToPath();
            if (args != null && args.Length > 0)
            {
                string argument = args[0];
                string path = _repository.GetNotes();

                switch (argument)
                {
                    case CustomArgument.OpenNotesFile:
                        Process.Start(path);
                        break;
                    case CustomArgument.OpenNotesFileInBrowser:
                        Process.Start("chrome.exe", path);
                        break;
                    case CustomArgument.BackupNotesFile:
                        _repository.BackupNotes();
                        break;
                    case CustomArgument.ClearNotesFile:
                        _repository.ClearNotes();
                        break;
                    case CustomArgument.OpenNewNotebook:
                        _repository.OpenNewNotebook();
                        break;
                    case CustomArgument.Help:
                        ShowHelpContent();
                        break;
                    default:
                        if (argument.StartsWith("-"))
                        {
                            throw new NoSuchCommandFoundException(argument);
                        }

                        string noteContent = args.Aggregate((a, b) => a + " " + b);
                        _repository.AddNote(noteContent);
                        break;
                }

            }
        }


        private void ShowHelpContent()
        {
            CustomArgument customArgument = new CustomArgument();
            foreach (var prop in customArgument.GetType().GetFields())
            {
                Console.WriteLine("{1}\t{0}", prop.Name, prop.GetValue(customArgument));
            }
        }

        private void AddToPath()
        {
            const string name = "PATH";
            string pathvar = System.Environment.GetEnvironmentVariable(name);

            string currentPath = Environment.CurrentDirectory;

            if (!pathvar.Contains(currentPath))
            {
                var value = pathvar + @";" + currentPath;
                var target = EnvironmentVariableTarget.Machine;
                System.Environment.SetEnvironmentVariable(name, value, target);
            }

        }

    }
}
