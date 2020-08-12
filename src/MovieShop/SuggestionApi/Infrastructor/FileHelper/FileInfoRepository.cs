using SuggestionApi.Infrastructor.FileHelper.Attributes;
using SuggestionApi.Infrastructor.FileHelper.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SuggestionApi.Infrastructor.FileHelper
{
    public class FileInfoRepository
    {
        private static ConcurrentDictionary<Type, IEnumerable<ModelInfo>> _fileInfoDictionary = new ConcurrentDictionary<Type, IEnumerable<ModelInfo>>();

        public IEnumerable<ModelInfo> Get<TModel>()
        {
            return _fileInfoDictionary.GetOrAdd(typeof(TModel), x => Create(x));
        }

        private static IEnumerable<ModelInfo> Create(Type type)
        {
            var members = type.GetProperties()
                              .Select(x => new { pi = x, attribute = x.GetCustomAttributes(typeof(FilePositionAttribute), true).FirstOrDefault() })
                              .Where(x => x.attribute is FilePositionAttribute)
                              .Select(x => new { pi = x.pi, ordre = ((FilePositionAttribute)x.attribute).Ordre })
                              .OrderBy(x => x.ordre)
                              .Select(x => new ModelInfo() { PropertyInfos = x.pi, Func = GetFunc(x.pi.PropertyType), Position = false }).ToList();

            var position = type.GetProperty("Position");
            if (position != null)
            {
                members.Add(new ModelInfo() { Func = x => x.BaseStream.Position, PropertyInfos = position, Position = true });
            }

            var offset = 0;
            foreach (var mem in members)
            {
                mem.Offset = offset;
                if (mem.PropertyInfos.PropertyType != typeof(string))
                {
                    offset += System.Runtime.InteropServices.Marshal.SizeOf(mem.PropertyInfos.PropertyType);
                }
            }

            return members;
        }

        private static Func<BinaryReader, object> GetFunc(Type type)
        {
            if (type == typeof(int))
            {
                return x => x.ReadInt32();
            }
            if (type == typeof(string))
            {
                return x => x.ReadString();
            }
            throw new Exception("");
        }
    }
}