using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parascan.Data
{
    public class DAccess
    {
        string dbName = "";
        public DAccess(string dbName)
        {
            this.dbName = dbName;
        }

        public void CreateSchema()
        {
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"CREATE TABLE meta("
                        + " scanid INTEGER PRIMARY KEY AUTOINCREMENT,"
                        + " datestring TEXT NOT NULL,"
                        + " scandirectory TEXT NOT NULL,"
                        + " codename TEXT NOT NULL);";

                    command.ExecuteNonQuery();

                    //Create table files
                    command.CommandText = @"CREATE TABLE files("
                        + " fileid INTEGER PRIMARY KEY AUTOINCREMENT,"
                        + " scanid INTEGER NOT NULL,"
                        + " path TEXT NOT NULL);";
                       
                    command.ExecuteNonQuery();

                    //Create table hashes
                    command.CommandText = @"CREATE TABLE hashes("
                        + " fileid INTEGER NOT NULL,"
                        + " hash TEXT NOT NULL);";

                    command.ExecuteNonQuery();

                    //Create table extensions
                    command.CommandText = @"CREATE TABLE extensions("
                        + " fileid INTEGER NOT NULL,"
                        + " extension TEXT NOT NULL);";

                    command.ExecuteNonQuery();

                    //Create table linecounts
                    command.CommandText = @"CREATE TABLE linecounts("
                        + " fileid INTEGER NOT NULL,"
                        + " linecount INTEGER NOT NULL);";

                    command.ExecuteNonQuery();

                    //Create table filesizes
                    command.CommandText = @"CREATE TABLE filesizes("
                        + " fileid INTEGER NOT NULL,"
                        + " filesize INTEGER NOT NULL);";

                    command.ExecuteNonQuery();


                    //Create table fileNames
                    command.CommandText = @"CREATE TABLE filenames("
                        + " fileid INTEGER NOT NULL,"
                        + " filename TEXT NOT NULL);";

                    command.ExecuteNonQuery();



                }
                catch (Exception e)
                {
                    MessageBox.Show("Problem creating schema: " + e.Message, "Error", MessageBoxButtons.OK);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public void InsertMeta(string datestring, string scandirectory, string codename)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"INSERT INTO meta (datestring, scandirectory, codename)"
                    + " VALUES ($ds, $td, $cd);";
                    command.Parameters.AddWithValue("$ds", datestring);
                    command.Parameters.AddWithValue("$td", scandirectory);
                    command.Parameters.AddWithValue("$cd", codename);

                    command.ExecuteNonQuery();

                      
                }
                catch (Exception e)
                {
                    //MessageBox.Show("Problem inserting meta: " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("Problem inserting meta: " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public void InsertFile(string filepath, int scanid)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"INSERT INTO files (scanid, path)"
                    + " VALUES ($si, $fp);";
                    command.Parameters.AddWithValue("$si", scanid);
                    command.Parameters.AddWithValue("$fp", filepath);
                   
                    command.ExecuteNonQuery();


                }
                catch (Exception e)
                {
                   // MessageBox.Show("Problem inserting file: " + filepath + " " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("error inserting file: " + filepath + " " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        public void InsertHash(int fileid, string filehash)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"INSERT INTO hashes (fileid, hash)"
                    + " VALUES ($fi, $h);";
                    command.Parameters.AddWithValue("$fi", fileid);
                    command.Parameters.AddWithValue("$h", filehash);

                    command.ExecuteNonQuery();


                }
                catch (Exception e)
                {
                    // MessageBox.Show("Problem inserting hash: " + filepath + " " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("error inserting hash " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }//end using
        }

        public void InsertExtensions(int fileid, string extension)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"INSERT INTO extensions (fileid, extension)"
                    + " VALUES ($fi, $ex);";
                    command.Parameters.AddWithValue("$fi", fileid);
                    command.Parameters.AddWithValue("$ex", extension);

                    command.ExecuteNonQuery();


                }
                catch (Exception e)
                {
                    // MessageBox.Show("Problem inserting hash: " + filepath + " " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("error inserting extension " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }//end using
        }

        public void InsertLineCounts(int fileid, int linecount)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"INSERT INTO linecounts (fileid, linecount)"
                    + " VALUES ($fi, $lc);";
                    command.Parameters.AddWithValue("$fi", fileid);
                    command.Parameters.AddWithValue("$lc", linecount);

                    command.ExecuteNonQuery();


                }
                catch (Exception e)
                {
                    // MessageBox.Show("Problem inserting hash: " + filepath + " " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("error inserting linecount " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }//end using
        }

        public void InsertFileSize(int fileid, int filesize)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"INSERT INTO filesizes (fileid, filesize)"
                    + " VALUES ($fi, $fs);";
                    command.Parameters.AddWithValue("$fi", fileid);
                    command.Parameters.AddWithValue("$fs", filesize);

                    command.ExecuteNonQuery();


                }
                catch (Exception e)
                {
                    // MessageBox.Show("Problem inserting hash: " + filepath + " " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("error inserting filesize " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }//end using
        }

        public void InsertFileNames(int fileid, string filename)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"INSERT INTO filenames (fileid, filename)"
                    + " VALUES ($fi, $fn);";
                    command.Parameters.AddWithValue("$fi", fileid);
                    command.Parameters.AddWithValue("$fn", filename);

                    command.ExecuteNonQuery();


                }
                catch (Exception e)
                {
                    // MessageBox.Show("Problem inserting hash: " + filepath + " " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("error inserting filename " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }//end using
        }

        public int GetMetaId(string scandirectory)
        {
            int scanid = 0;
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"SELECT scanid FROM meta WHERE scandirectory = $sd";
                    command.Parameters.AddWithValue("$sd", scandirectory);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scanid = reader.GetInt32(0);
                                                     
                        }
                    }


                }
                catch (Exception e)
                {
                   // MessageBox.Show("Problem inserting meta: " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("Problem inserting meta: " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }//end using
            return scanid;
        }
        public int GetFileId(int scanid, string filepath)
        {
            int fileid = 0;
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"SELECT fileid FROM files WHERE path = $fp AND scanid = $sd";
                    command.Parameters.AddWithValue("$fp", filepath);
                    command.Parameters.AddWithValue("$sd", scanid);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fileid = reader.GetInt32(0);

                        }
                    }


                }
                catch (Exception e)
                {
                    // MessageBox.Show("Problem inserting meta: " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("Problem getting fileid: " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }//end using
            return fileid;
        }
        public List<PFile> GetPFiles(int scanid)
        {
            List<PFile> files = new List<PFile>();
            using (var connection = new SqliteConnection("Data Source=" + dbName))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    //Create table meta
                    command.CommandText = @"SELECT fileid, path FROM files WHERE scanid = $sd";
                    command.Parameters.AddWithValue("$sd", scanid);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PFile pf = new PFile();
                            pf.FileId = reader.GetInt32(0);
                            pf.FilePath = reader.GetString(1);
                            files.Add(pf);

                        }
                    }


                }
                catch (Exception e)
                {
                    // MessageBox.Show("Problem inserting meta: " + e.Message, "Error", MessageBoxButtons.OK);
                    throw new Exception("Problem getting pfiles: " + e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }//end using

            return files;
        }

    }
}
