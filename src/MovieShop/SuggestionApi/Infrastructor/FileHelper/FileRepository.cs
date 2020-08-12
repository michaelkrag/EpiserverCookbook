using SuggestionApi.Infrastructor.FileHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace SuggestionApi.Infrastructor.FileHelper
{
    public class FileRepository<TModel> where TModel : new()
    {
        private readonly string _fileName;
        private static FileInfoRepository fileInfoRepository = new FileInfoRepository();
        private readonly IEnumerable<ModelInfo> _fileInfo;

        public FileRepository(string fileName)
        {
            _fileName = fileName;
            _fileInfo = fileInfoRepository.Get<TModel>();
        }

        public IEnumerable<TModel> GetAll()
        {
            if (File.Exists(_fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(_fileName, FileMode.Open)))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var model = new TModel();
                        var index = _fileInfo.Where(x => x.Position).FirstOrDefault();
                        if (index != null)
                        {
                            index.PropertyInfos.SetValue(model, reader.BaseStream.Position);
                        }

                        foreach (var modelInfo in _fileInfo.Where(x => x.Position == false))
                        {
                            modelInfo.PropertyInfos.SetValue(model, modelInfo.Func(reader));
                        }

                        yield return model;
                    }
                }
            }
        }

        public TModel Append(TModel model)
        {
            using (var writer = new BinaryWriter(File.Open(_fileName, FileMode.Append)))
            {
                var position = writer.BaseStream.Position;
                foreach (var modelInfo in _fileInfo.Where(x => x.Position == false))
                {
                    var obj = modelInfo.PropertyInfos.GetValue(model);
                    if (obj is int intValue)
                    {
                        writer.Write(intValue);
                    }
                    else if (obj is string stringValue)
                    {
                        writer.Write(stringValue);
                    }
                    else
                    {
                        throw new Exception("");
                    }
                }
                var pos = _fileInfo.Where(x => x.Position).FirstOrDefault();
                if (pos != null)
                {
                    pos.PropertyInfos.SetValue(model, position);
                }
            }
            return model;
        }

        public bool Update(int value, long position, Expression<Func<TModel, int>> property)
        {
            var expression = (MemberExpression)property.Body;
            var name = expression.Member.Name;

            var fileInfo = _fileInfo.Where(x => x.PropertyInfos.Name == name).First();

            return Update(value, position, fileInfo.Offset);
        }

        public bool Update(int value, long position, int offset)
        {
            if (File.Exists(_fileName))
            {
                using (var writer = new BinaryWriter(File.Open(_fileName, FileMode.Open)))
                {
                    writer.BaseStream.Position = position + offset;
                    writer.Write(value);
                }
            }
            return true;
        }
    }
}