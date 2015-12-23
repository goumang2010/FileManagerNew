using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManagerNew
{
   public static class  fileLists
    {


        public static void  WalkTree(this List<FileInfo> fiList,string folderpath, bool recur = true)
        {
         // var dd=  fiList.ToList();
            Action<string> WalkDirectoryTree = null;
          WalkDirectoryTree = delegate (string path)
              {
                //fileInfo Allfile = new fileInfo();
                DirectoryInfo dir = new DirectoryInfo(path);
                  FileInfo[] files = null;


                  try
                  {

                      files = dir.GetFiles();


                  }
                //catch (UnauthorizedAccessException e)
                catch (Exception e)
                  {
                      throw e;
                  }

                  if (files != null)
                  {

                      fiList.AddRange(files);

                  }
                  // Now find all the subdirectories under this directory.
                  if (recur)
                  {
                      var subDirs = dir.GetDirectories();
                      // display = display + "\r\n";

                      foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                      {
                          WalkDirectoryTree(dirInfo.FullName);

                         

                      }
                  }



              };
          

          

            WalkDirectoryTree(folderpath);


        



        }



        public static void copyto(this IEnumerable<FileInfo> fi, string newfoleder)
        {

            if (newfoleder.Last() != '\\')
            {
                newfoleder = newfoleder + "\\";
            }
            var count = fi.Count();
            for (int i = 0; i < count; i++)
            {
                string newfullname = newfoleder + fi.ElementAt(i).Name;

                File.Copy(fi.ElementAt(i).FullName, newfullname,true);
                //   fileFullName[i] = newfullname;
                // filePath[i] = newfoleder;
            }

         

        }






        public static void moveto(this IEnumerable<FileInfo> fi,string newfolder)
        {
         
            var count = fi.Count();
            if (newfolder.Last() != '\\')
            {
                newfolder = newfolder + "\\";
            }
            if (!Directory.Exists(newfolder))
            {
                Directory.CreateDirectory(newfolder);

            }
            for (int i = 0; i < count; i++)
            {
                string newfullname = newfolder + fi.ElementAt(i).Name;
                if (File.Exists(newfullname))
                {
                    File.Delete(newfullname);
                }
                try
                {
                    File.Move(fi.ElementAt(i).FullName, newfullname);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }



        }

        public static IEnumerable<FileInfo> moveto(this List<FileInfo> fi,string oldpath, string newpath)
        {

            var count = fi.Count;
            for (int i = 0; i < count; i++)
            {

                string newfullname = fi[i].FullName.Replace(oldpath, newpath);
                string newfolder = newfullname.Replace(fi[i].Name, "");
                if (!Directory.Exists(newfolder))
                {
                    Directory.CreateDirectory(newfolder);

                }
                try
                {
                    if (File.Exists(newfullname))
                    {
                        File.Delete(newfullname);
                    }

                    File.Move(fi[i].FullName, newfullname);
                    
                }
                catch
                {
                    continue;
                }
               
                yield return new FileInfo(newfullname);
            }



        }

        public static IEnumerable<FileInfo> namefilter(this IEnumerable<FileInfo> fi, string pass, string deny)
        {
            return fi.Where(delegate (FileInfo p)
            {
                string tempstr = p.Name.Replace(p.Extension, "");
                if (tempstr.Contains(pass) && !tempstr.Contains(deny))
                {
                    return true;
                }
                return false;
            });


        }
        public static IEnumerable<FileInfo> namefilter(this IEnumerable<FileInfo> fi, string pass)
        {
            return fi.Where(delegate (FileInfo p)
            {
                string tempstr = p.Name.Replace(p.Extension, "");
                if (tempstr.Contains(pass) )
                {
                    return true;
                }
                return false;
            });


        }

        public static IEnumerable<FileInfo> extfilter(this IEnumerable<FileInfo> fi, string pass, string deny)
        {
            return fi.Where(delegate (FileInfo p)
            {
                string tempstr =p.Extension.ToLower();
                if (tempstr.Contains(pass.ToLower()) && !tempstr.Contains(deny.ToLower()))
                {
                    return true;
                }
            
                return false;
            });
        }

        public static IEnumerable<FileInfo> extfilter(this IEnumerable<FileInfo> fi, string pass)
        {
            return fi.Where(p => p.Extension.ToLower().Contains(pass.ToLower()));
        }
    }
}
