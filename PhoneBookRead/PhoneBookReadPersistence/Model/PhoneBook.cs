using Cassandra.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookReadPersistence.Model
{
    [Table("phonebook")]
    public class PhoneBook
    {
        [PartitionKey]
        public string username { get; set; }
        [Column("id")]
        public int id { get; set; }
        [Column("phonenumber")]
        public string phonenumber { get; set; }
    }
}
