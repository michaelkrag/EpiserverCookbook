using System;

namespace SuggestionApi.Infrastructor.FileHelper.Attributes
{
    public class FilePositionAttribute : Attribute
    {
        public readonly int Ordre;

        public FilePositionAttribute(int ordre)
        {
            Ordre = ordre;
        }
    }
}