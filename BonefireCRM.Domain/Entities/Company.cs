﻿namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a company in the CRM system. 
    /// A company can have multiple contacts and deals.
    /// </summary>
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Industry { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Contact> Contacts { get; set; } = [];

        public ICollection<Deal> Deals { get; set; } = [];
    }
}