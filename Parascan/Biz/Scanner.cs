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
        string dbName = "";
        string dir = "";
        string projName = "";


        public Scanner(string dir, string projName)
        {
            this.dir = dir;
            this.projName = projName;
            this.dbName = projName + "_scan_" + DateTime.Now.Ticks + ".db";
            
        }

        void INIT()
        {
            DAccess d = new DAccess(dbName);
            
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
            else
            {
                d.CreateSchema();
            }
           
        }
        public void SCAN(string dir, string projName)
        {
            if(Directory.Exists(dir))
            {
                INIT();
                DAccess d = new DAccess(dbName);
                d.InsertMeta(DateTime.Now.ToString(), dir, projName);

                FileUtil fu = new FileUtil();
                int scanid = this.GetScanId(dir);
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

                d.CreateViews();



            }
            else
            {
                throw new Exception(dir + " does not exist");
            }
            
     
        }

        public int GetScanId(string scandirectory)
        {
            DAccess d = new DAccess(dbName);
            return d.GetMetaId(scandirectory);
        }

        public int GetFileId(int scanid, string path)
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
