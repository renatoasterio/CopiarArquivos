using System.Collections.Generic;
using System.IO;

namespace CopiarArquivos
{
    class Program
    {
       static Dictionary<string,string> arquivos = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            string origem = @"C:\data\db";
            string destino = @"C:\data\destino";

            GetDiretories(origem, destino);
            foreach (var item in arquivos)
                File.Copy(item.Key, item.Value, true);
        }


        public static void GetDiretories(string origem, string destino)
        {
            var oFiles = Directory.GetFiles(origem);
            foreach (var item in oFiles)
            {
                FileInfo ofInfo = new FileInfo(item);
                string dFile = destino + @"\" + ofInfo.Name;
                if (!File.Exists(dFile))
                    arquivos.Add(item,dFile);
                else
                {
                    FileInfo dfInfo = new FileInfo(dFile);
                    if (ofInfo.LastWriteTimeUtc > dfInfo.LastWriteTimeUtc)
                        arquivos.Add(item, dFile);
                }
            }

            var diretorios = Directory.GetDirectories(origem);
            foreach (var item in diretorios)
            {
                DirectoryInfo dInfo = new DirectoryInfo(item);
                string novoDiretorio = $@"{destino}\{dInfo.Name}";
                if (!Directory.Exists(novoDiretorio))
                    Directory.CreateDirectory(novoDiretorio);

                GetDiretories(item, novoDiretorio);
            }
        }
    }
}
