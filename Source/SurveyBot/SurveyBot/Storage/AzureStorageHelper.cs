using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using WindowsAzure.Table;

namespace SurveyBot
{
    public class StorageHelper
    {
        private CloudStorageAccount storageAccount = null;
        private CloudTableClient tableClient;
        private TableSet<SurveyDTO> assessmentsTable;

        public StorageHelper()
        {
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // we ensure that we create the Table if not already present
            assessmentsTable = new TableSet<SurveyDTO>(storageAccount.CreateCloudTableClient());
            assessmentsTable.CreateIfNotExists();
        }

        public void StoreSurvey(SurveyDTO survey)
        {
            assessmentsTable.AddOrUpdate(survey);
        }
    }
}