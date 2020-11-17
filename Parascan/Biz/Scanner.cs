using Parascan.Data;
using Parascan.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parascan.Biz
{
    public class Scanner
    {
        const string dbName = "paraschema.db";

        public static void INIT()
        {
            DAccess d = new DAccess(dbName);

            if (!File.Exists(dbName))
            {
                d.CreateSchema();
            }
           
        }
        public static void SCAN(string dir, string projName)
        {
            if(Directory.Exists(dir))
            {
                DAccess d = new DAccess(dbName);
                d.InsertMeta(DateTime.Now.ToString(), dir, projName);

                FileUtil fu = new FileUtil();
                int scanid = Scanner.GetScanId(dir);
                foreach (var f in fu.GetFiles(dir))
                {
                    d.InsertFile(f, scanid);
                }

                List<PFile> files = d.GetPFiles(scanid);

                //TODO: can multi-thread the items below
                
                //1. Hash files
                foreach(var f in files)
                {
                    d.InsertHash(f.FileId, Hasher.GetMD5Hash(File.ReadAllText(f.FilePath)));
                }

                //2. Get linecounts
                foreach (var f in files)
                {
                    d.InsertLineCounts(f.FileId, File.ReadAllLines(f.FilePath).Length);
                }

                //3. Get extensions
                foreach (var f in files)
                {
                    string[] sa = f.FilePath.Split('.');
                    d.InsertExtensions(f.FileId, sa[sa.Length - 1]);
                }

                //4. Get filesizes
                foreach (var f in files)
                {
                    d.InsertFileSize(f.FileId, File.ReadAllBytes(f.FilePath).Length);
                }

                //5. Get filenames
                foreach (var f in files)
                {
                    string[] sa = f.FilePath.Split('\\');
                    d.InsertFileNames(f.FileId, sa[sa.Length - 1]);

                }



            }
            else
            {
                throw new Exception(dir + " does not exist");
            }
            
     
        }

        public static int GetScanId(string scandirectory)
        {
            DAccess d = new DAccess(dbName);
            return d.GetMetaId(scandirectory);
        }

        public static int GetFileId(int scanid, string path)
        {
            try
            {
                DAccess d = new DAccess(dbName);
                return d.GetFileId(scanid, path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
