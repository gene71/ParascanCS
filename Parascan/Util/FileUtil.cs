﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parascan.Util
{
    public class FileUtil
    {
        List<string> files;

        public FileUtil()
        {
            files = new List<string>();
        }

        /// <summary>
        /// GetFiles returns a generic list of file paths
        /// </summary>
        /// <param name="path"></param>
        /// <returns>List</returns>
        /// <exception cref="Exception">Exception</exception>
        public List<string> GetFiles(string path)
        {


            try
            {

                foreach (string f in Directory.GetFiles(path))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(path))
                {
                    GetFiles(d);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error enumerating files " + ex.Message);
            }

            return files;
        }
    }
}
