using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FileManagerNew
{
    public class wordMethod
    {

        public static Document opendoc(string filepath,bool vis=true)
        {
        
        var    wordApp = new Microsoft.Office.Interop.Word.Application();
            if (vis==true)
            {
                wordApp.Visible = true;
            }
            else
            {
                wordApp.Visible = false;
            }
            

           foreach (Document dd in wordApp.Documents)
            {
               if( dd.Path== filepath)
                {
                    return dd;
                }

            }
            Document myAO;
            myAO = wordApp.Documents.Open(filepath);
            return myAO;



     
     


        }


        public static string gettext(Document doc,int i,int j)
        {
            return doc.Tables[1].Cell(i, j).Range.Text.Replace("\a", "").Replace("\r", "");
        }


        public static void SearchReplace(string path, string oldStr, string newStr)
        {
            object missing = Type.Missing;
             
           var  wordApp = new Microsoft.Office.Interop.Word.Application();
             wordApp.Visible=false;
             wordApp.DisplayAlerts =WdAlertLevel.wdAlertsNone ;
             Document myAO;
             myAO = wordApp.Documents.Open(path);
            //if(myAO==null)
            //{
            //                  object oMissing = System.Reflection.Missing.Value;
            //Microsoft.Office.Interop.Word.Application winObj = (Microsoft.Office.Interop.Word.Application)Marshal.GetActiveObject("Word.Application");
            //var searchdoc = from Document kk in winObj.Documents
            //            where path.Contains(kk.Name)
            //            select kk;

            //if(searchdoc.Count()>0)
            //{
            //    myAO = searchdoc.First();
            //}
            //}

             SearchReplace(wordApp, myAO, oldStr, newStr);
             wordApp.Quit();



        }

        public static void SearchReplace(Microsoft.Office.Interop.Word.Document wordDoc, string oldStr, string newStr)
        {
            var wordApp = wordDoc.Application;
            SearchReplace(wordApp, wordDoc, oldStr, newStr);
        }


        public static void Search( Microsoft.Office.Interop.Word.Document wordDoc, string oldStr)
        {
            var wordApp = wordDoc.Application;
            wordApp.Selection.Find.ClearFormatting();
            wordApp.Selection.Find.Text = oldStr;
            object missing = Type.Missing;
            wordApp.Selection.Find.Execute(
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing);
        }


        public static void SearchReplace( Microsoft.Office.Interop.Word.Application wordApp, Microsoft.Office.Interop.Word.Document wordDoc, string oldStr, string newStr)
        {
            #region 文字区域
            object replaceAll = WdReplace.wdReplaceAll;
        
            wordApp.Selection.Find.ClearFormatting();
            wordApp.Selection.Find.Text = oldStr;

            wordApp.Selection.Find.Replacement.ClearFormatting();
            wordApp.Selection.Find.Replacement.Text = newStr;
            object missing = Type.Missing;
            wordApp.Selection.Find.Execute(
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing);
            #endregion

            #region 文本框
            StoryRanges sr = wordDoc.StoryRanges;
            foreach (Range r in sr)
            {
                Range r1 = r;
                if (WdStoryType.wdTextFrameStory == r.StoryType)
                {
                    do
                    {
                        r1.Find.ClearFormatting();
                        r1.Find.Text = oldStr;

                        r1.Find.Replacement.ClearFormatting();
                        r1.Find.Replacement.Text = newStr;

                        r1.Find.Execute(
                            ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref replaceAll, ref missing, ref missing, ref missing, ref missing);

                        r1 = r1.NextStoryRange;
                    } while (r1 != null);
                }
            }
            #endregion




            //替换页眉

            foreach (Microsoft.Office.Interop.Word.Section section in wordDoc.Sections)
            {

                Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;


                headerRange.Find.ClearFormatting();
                headerRange.Find.Replacement.ClearFormatting();
                headerRange.Find.Text = oldStr;
                headerRange.Find.Replacement.Text = newStr;
                headerRange.Find.Execute(
                            ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref replaceAll, ref missing, ref missing, ref missing, ref missing);

            }   









            try
            {
                wordDoc.Save();
            }
            catch(Exception e)
            {
                throw e;
            }
           


        }


        public static bool WordToPDF(string sourcePath, string targetPath)
        {
            bool result = false;
            Microsoft.Office.Interop.Word.WdExportFormat exportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;
            object paramMissing = Type.Missing;
            Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;
                Microsoft.Office.Interop.Word.WdExportFormat paramExportFormat = exportFormat;
                bool paramOpenAfterExport = false;
                Microsoft.Office.Interop.Word.WdExportOptimizeFor paramExportOptimizeFor = Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Microsoft.Office.Interop.Word.WdExportRange paramExportRange = Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Microsoft.Office.Interop.Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Microsoft.Office.Interop.Word.WdExportCreateBookmarks paramCreateBookmarks = Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;
                wordDocument = wordApplication.Documents.Open(
                ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                ref paramMissing, ref paramMissing, ref paramMissing,
                ref paramMissing, ref paramMissing, ref paramMissing,
                ref paramMissing, ref paramMissing, ref paramMissing,
                ref paramMissing, ref paramMissing, ref paramMissing,
                ref paramMissing);
                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                    paramExportFormat, paramOpenAfterExport,
                    paramExportOptimizeFor, paramExportRange, paramStartPage,
                    paramEndPage, paramExportItem, paramIncludeDocProps,
                    paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                    paramBitmapMissingFonts, paramUseISO19005_1,
                    ref paramMissing);

                result = true;

                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch
            {
                result = false;

                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

    }
}
