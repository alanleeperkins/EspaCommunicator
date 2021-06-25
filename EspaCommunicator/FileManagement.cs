using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EspaLib;

namespace EspaCommunicator
{
    public class FileManagement
    {
        /// <summary>
        /// key = filepath
        /// value = object of any kind of file data class
        /// </summary>
        private Dictionary<string, object> LoadedCommunicationDataLogFiles = new Dictionary<string, object>();

        public FileManagement()
        {

        }

        /// <summary>
        /// remove all files
        /// </summary>
        public void Clear()
        {
            if (LoadedCommunicationDataLogFiles == null) return;

            LoadedCommunicationDataLogFiles.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool IsFileInList(string filepath)
        {
            if (LoadedCommunicationDataLogFiles == null) return false;
            return LoadedCommunicationDataLogFiles.ContainsKey(filepath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filedata"></param>
        public bool AddFile(string filepath, object filedata, bool updateexistingfile=true)
        {
            if (LoadedCommunicationDataLogFiles == null) return false;
            if ((IsFileInList(filepath) == true))
            {
                if (updateexistingfile)
                {
                    return UpdateFile(filepath, filedata);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                LoadedCommunicationDataLogFiles.Add(filepath, filedata);
            }

            return IsFileInList(filepath);
        }

        /// <summary>
        /// updates the data of an already loaded file
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filedata"></param>
        /// <returns></returns>
        public bool UpdateFile(string filepath, object filedata)
        {
            if (LoadedCommunicationDataLogFiles == null) return false;
            if (IsFileInList(filepath) == false) return false;

            LoadedCommunicationDataLogFiles[filepath] = filedata;
           
            return IsFileInList(filepath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public void RemoveFile(string filepath)
        {
            if (IsFileInList(filepath) == false) return;

            if (LoadedCommunicationDataLogFiles.Remove(filepath) == false)
            {
                Console.WriteLine("ERROR: cannot remove dictionary content! ({0})", filepath);
            }
        }
    }
}
