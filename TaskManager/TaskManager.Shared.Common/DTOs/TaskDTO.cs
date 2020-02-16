using Newtonsoft.Json;
using System;

namespace TaskManager.Shared.Common.DTOs
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SectionId { get; set; }
        public int TableId { get; set; }

        [JsonProperty]
        public DateTime? EndDate { get; set; }
    }
}
