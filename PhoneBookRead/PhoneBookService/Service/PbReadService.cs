using PhoneBookReadPersistence.Repository.PhoneBookReadRepository;
using PhoneBookReadService.DataTransfer.Mapper;
using PhoneBookReadService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBookReadService.Service
{
    public class PbReadService : IPbReadService
    {
        IPhoneBookReadRepository repo;

        public PbReadService(IPhoneBookReadRepository repo)
        {
            this.repo = repo;
        }
        public void DeletePhoneBook(int id)
        {
            var phoneBook = repo.Get(string.Format("select * from phonebook where id = {0}", id));
            if (phoneBook != null)
            {
                repo.Delete(phoneBook);
            }
        }

        public IEnumerable<PhoneBookDTO> GetPhonebookByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var result = repo.GetAll();
                return result.Select(x => x.ToDTO()).ToList();
            }
            else
            {
                var result = repo.GetAll(string.Format(@"select * from phonebook where username = '{0}'", name));
                return result.Select(x => x.ToDTO()).ToList();
            }
        }

        public void SavePhonebook(PhoneBookDTO phoneBookDTO)
        {
            var phonebook = phoneBookDTO.ToEntity();
            repo.Insert(phonebook);
        }

        public void UpdatePhonebook(PhoneBookDTO dto)
        {
            var phonebook = dto.ToEntity();
            repo.Update(phonebook);
        }
    }
}
