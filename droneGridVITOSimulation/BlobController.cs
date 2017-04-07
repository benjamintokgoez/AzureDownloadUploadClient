using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace droneGridVITOSimulation
{
    class BlobController
    {

        

        public LinkedList<CloudBlockBlob> getBlobList(CloudBlobContainer container)
        {
            

            LinkedList<CloudBlockBlob> tempBlobList = new LinkedList<CloudBlockBlob>();
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {

                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    tempBlobList.AddLast(blob);

                }


            }
            return tempBlobList;
        }
        public void downloadBlockBlob(CloudBlobContainer container, string blockblobname) {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blockblobname);
            using (var fileStream = System.IO.File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + blockblobname))
            {
                blockBlob.DownloadToStream(fileStream);
            }
            Console.WriteLine(blockBlob.Name + " downloaded");

        }

        public void uploadBlockBlobManipulated(CloudBlobContainer container, string blockblobnname) {
            //manipulateFile
            string newPrefix = "new-";
            //
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(newPrefix + blockblobnname);
            using (var fileStream = System.IO.File.OpenRead(System.AppDomain.CurrentDomain.BaseDirectory + "\\" +blockblobnname))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            Console.WriteLine(blockBlob.Name + " uploaded");

        }

    }
}
