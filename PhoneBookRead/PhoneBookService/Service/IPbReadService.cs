using PhoneBookReadService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookReadService.Service
{
    public interface IPbReadService
    {
        IEnumerable<PhoneBookDTO> GetPhonebookByName(string name);
        void UpdatePhonebook(PhoneBookDTO dto);
        void DeletePhoneBook(int id);
        void SavePhonebook(PhoneBookDTO phoneBook);
    }
}
