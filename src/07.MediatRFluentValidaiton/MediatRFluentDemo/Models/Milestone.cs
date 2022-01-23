using MediatRFluentDemo.Models.Common;
using System;

namespace MediatRFluentDemo.Models
{
    /// <summary>
    /// An milestone with Id, TItle, Content, AchivedOn, AdditionalDetails
    /// </summary>
    public class Milestone : AuditableEntity
    {
        /// <summary>
        /// The id of the milestone
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Title of the milestone
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Content of the milestone
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The Achived date of the milestone
        /// </summary>
        public DateTime AchivedOn { get; set; }

        /// <summary>
        /// Additiotnal information about the milestone
        /// </summary>
        public string AdditionalDetails { get; set; }
    }
}
