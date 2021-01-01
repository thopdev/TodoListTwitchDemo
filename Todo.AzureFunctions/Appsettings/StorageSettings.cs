using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.AzureFunctions.Appsettings
{
    public class StorageSettings
    {
        public const string JsonKey = "Storage";
        public string Account { get; set; }
    }
}
