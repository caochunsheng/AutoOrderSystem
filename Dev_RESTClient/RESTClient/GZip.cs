using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Diagnostics;

namespace RESTClient
{
    public class GZip
    {
        public static Boolean ZipFolder(String folder, String zipFile)
        {
            if (!Directory.Exists(folder))
            {
                Debug.WriteLine("Cannot find directory '{0}'", folder);
                return false;
            }

            try
            {
                ICSharpCode.SharpZipLib.Zip.FastZip z = new ICSharpCode.SharpZipLib.Zip.FastZip();
                z.CreateEmptyDirectories = true;
                z.CreateZip(zipFile, folder, true, "");

                if (File.Exists(zipFile))
                    return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception during processing {0}", ex);
            }
            return false;
        }

        public static Boolean ZipFile(String filePath, String zipFile)
        {
            if (!File.Exists(filePath))
            {
                Debug.WriteLine("Cannot find file '{0}'", filePath);
                return false;
            }

            try
            {
                using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(File.Create(zipFile))) 
                {
		            zipStream.SetLevel(9); //0~9

                    byte[] buffer = new byte[4096];
                    ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(System.IO.Path.GetFileName(filePath));

                    entry.DateTime = DateTime.Now;
                    zipStream.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(filePath)) 
                    {
	                    int sourceBytes = 0;
	                    do {
		                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
		                    zipStream.Write(buffer, 0, sourceBytes);
	                    } while (sourceBytes > 0);
                    }

                    zipStream.Finish();
                    zipStream.Close();
                    zipStream.Dispose();
                }

                if (File.Exists(zipFile))
                    return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception during processing {0}", ex);
            }
            return false;
        }

        public static Boolean UnZip(String zipFile, String folder)
        {
            if (!File.Exists(zipFile))
            {
                Debug.WriteLine("Cannot find file '{0}'", zipFile);
                return false;
            }

            try
            {
                ZipInputStream s = new ZipInputStream(File.OpenRead(zipFile));

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);
                    string serverFolder = folder;

                    Directory.CreateDirectory(serverFolder + directoryName);
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(( serverFolder + theEntry.Name));

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
                s.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception during processing {0}", ex);
            }
            return false;
        }
    }
}
