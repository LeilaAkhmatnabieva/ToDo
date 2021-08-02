using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    class FileIOService
    {
        private readonly string PATH;
        public FileIOService(String path)
        {
            PATH = path;
        }
        public BindingList<TodoModel> LoadData()
        {
            var fileExist = File.Exists(PATH);
            if(!fileExist)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<TodoModel>();
            }
            using(var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
            }
        }

        public void SaveData(object todoDataList)//BindingList<TodoModel> todoDataList
        {
            using (StreamWriter writer = File.CreateText(PATH))//чтобы вызвать метод desporse
            {
                string output = JsonConvert.SerializeObject(todoDataList);
                writer.Write(output);
            }
        }
    }
}
