using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace practicheskaya2
{
    public class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }

    public class DataSerializer
    {
        public void Serialize<T>(List<T> data, string filePath)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, jsonData);
        }

        public List<T> Deserialize<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(jsonData);
            }
            else
            {
                return new List<T>();
            }
        }
    }

    public class DailyPlannerViewModel
    {
        private List<Note> notes;
        private DataSerializer serializer;
        private string filePath = "notes.json";

        public DailyPlannerViewModel()
        {
            serializer = new DataSerializer();
            LoadNotes();
        }

        public List<Note> Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        private void LoadNotes()
        {
            notes = serializer.Deserialize<Note>(filePath);
        }

        public void SaveNotes()
        {
            serializer.Serialize(notes, filePath);
        }

        public void AddOrUpdateNote(Note note)
        {
            int index = notes.FindIndex(n => n.Date == note.Date);
            if (index != -1)
            {
                notes[index] = note;
            }
            else
            {
                notes.Add(note);
            }
            SaveNotes();
        }

        public void DeleteNote(Note note)
        {
            notes.Remove(note);
            SaveNotes();
        }
    }
}
