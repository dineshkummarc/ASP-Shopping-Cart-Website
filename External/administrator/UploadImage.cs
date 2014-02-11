using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace External.administrator
{
    public enum UploadResult
    {
        Successful,
        InvalidExtension,
        NoImageFound
    }

    public class UploadImage
    {
        /// <summary>
        /// Uploads and Image to the Server
        /// Level: External
        /// </summary>
        /// <param name="myUploadControl">The Upload Control Object</param>
        /// <returns>Tuple where String is the Path and Upload Result is the Enum</returns>
        public Tuple<string, UploadResult> Upload(FileUpload myUploadControl)
        {
            try
            {
                bool Correct = false;
                string myPath = HttpContext.Current.Server.MapPath("~/uploads/");
                string myExtension = Path.GetExtension(myUploadControl.FileName).ToLower();

                if (myUploadControl.HasFile)
                {
                    string[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };

                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (myExtension == allowedExtensions[i])
                        {
                            Correct = true;
                            break;
                        }
                    }
                }
                else
                {
                    return new Tuple<string, UploadResult>(null, UploadResult.NoImageFound);
                }

                if (Correct)
                {
                    Guid myGuid = Guid.NewGuid();

                    string CombinedPath = Path.Combine(myPath, myGuid.ToString() + myExtension);
                    string DatabasePath = Path.Combine("~\\uploads\\", myGuid.ToString() + myExtension);

                    myUploadControl.PostedFile.SaveAs(CombinedPath);

                    return new Tuple<string, UploadResult>(DatabasePath, UploadResult.Successful);
                }
                else
                {
                    return new Tuple<string, UploadResult>(null, UploadResult.InvalidExtension);
                }
            }
            catch (Exception Exception)
            {
                throw Exception;
            }
        }
    }
}