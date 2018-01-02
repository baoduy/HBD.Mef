using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace AzureNoteEntities
{
    public class Note
    {
        [JsonProperty(PropertyName = "id")]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [AllowHtml]
        public string Contents { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}