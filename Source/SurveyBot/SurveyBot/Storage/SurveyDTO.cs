using Microsoft.WindowsAzure.Storage.Table;
using System;
using WindowsAzure.Table.Attributes;

namespace SurveyBot
{
    public sealed class SurveyDTO : TableEntity
    {
        [PartitionKey]
        public string Partition = "Survey";

        [RowKey]
        public string Id;

        [Property("email")]
        public string Email;
    }

    public static class Extensions
    {
        public static SurveyDTO ConvertToDTO(this Survey survey)
        {
            SurveyDTO dto = new SurveyDTO();
            dto.Id = Guid.NewGuid().ToString();
            dto.Email = survey.Email;
            // map each property you want to store in database
            return dto;
        }
    }
}