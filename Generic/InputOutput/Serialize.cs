using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Generic.InputOutput
{
    public static class Serialize
    {
        public static void Save<DataType>(String filename, DataType obj)
        {
            var fileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            Save(fileStream,obj);
        }

        public static void Save<DataType>(Stream file, DataType obj)
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(file, obj);
            file.Close();
        }

        public static DataType Load<DataType>(Stream file)
        {
            DataType result;
            var binaryFormatter = new BinaryFormatter();
            try
            {
                result = (DataType)binaryFormatter.Deserialize(file);
            }
            finally
            {
                file.Close();
            }
            return result;
        }

        public static DataType Load<DataType>(String fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return Load<DataType>(fileStream);
        }
    }
}
