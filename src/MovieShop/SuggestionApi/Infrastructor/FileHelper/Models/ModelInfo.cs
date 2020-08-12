using System;
using System.IO;
using System.Reflection;

namespace SuggestionApi.Infrastructor.FileHelper.Models
{
    public class ModelInfo
    {
        public PropertyInfo PropertyInfos { get; set; }
        public Func<BinaryReader, object> Func { get; set; }
        public bool Position { get; set; }
        public int Offset { get; set; }
    }
}