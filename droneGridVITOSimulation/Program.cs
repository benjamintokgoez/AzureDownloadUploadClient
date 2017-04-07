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
    class Program
    {
        

        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
             CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("blobcontainer");

            container.CreateIfNotExists();

            BlobController bc = new BlobController();

            foreach (CloudBlockBlob blockblob in bc.getBlobList(container)) {

                Console.WriteLine("Block blob of length {0}: {1}", blockblob.Properties.Length, blockblob.Name + " exists");

                 bc.downloadBlockBlob(container, blockblob.Name);
                
                 bc.uploadBlockBlobManipulated(container,  blockblob.Name);
            }

            while (true) ;
        }


      
    }
}
