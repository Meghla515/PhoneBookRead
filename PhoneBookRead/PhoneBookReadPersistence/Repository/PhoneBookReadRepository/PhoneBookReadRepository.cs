using Microsoft.Extensions.Configuration;
using PhoneBookReadPersistence.Model;
using PhoneBookReadPersistence.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookReadPersistence.Repository.PhoneBookReadRepository
{
    public class PhoneBookReadRepository : Repository<PhoneBook>, IPhoneBookReadRepository
    {
        public PhoneBookReadRepository()
        {
            CreateTableIfNotExist();
        }
    }
}
