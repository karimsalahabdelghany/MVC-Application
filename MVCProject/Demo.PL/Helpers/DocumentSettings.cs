using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Demo.PL.Helpers
{
    public static class DocumentSettings
    {
        //upload
        
        public static string UploadFile(IFormFile file , string FolderName)
        {
            //1.Get Located Folder Path
            //F:\visual stadio projects\MVC Project\MVC Project\Demo.PL\wwwroot\Files\Images\
            //Directory.GetCurrentDirectory()+"\\wwwroot\\Files\\"+FolderName;
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files",FolderName);
            //2.Get File Name and Make it Unique
            string filename =$"{ file.FileName}";
            //3.Get File Path[Folder Path + FileName]
            string FilePath = Path.Combine(FolderPath,filename);
            //4.Save File As Streams
            using var fs = new FileStream(FilePath,FileMode.Create);
            file.CopyTo(fs);
            //5.Return File Name
            return filename;

        }
        //Delete
        public static void DeleteFile( string filename,string folderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", filename);

            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
