using PhoneBookReadPersistence.Repository.PhoneBookReadRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookReadService.Service.Message
{
    public class ServiceFactory : IDisposable
    {
        public IPhoneBookReadRepository pbRepo { get; set; }
        public IPbReadService pbService { get; set; }

        public ServiceFactory()
        {
            pbRepo = new PhoneBookReadRepository();
            pbService = new PbReadService(pbRepo);
        }

        public void Dispose()
        {
            if (pbRepo != null)
            {
                pbRepo.Dispose();
            }
        }
    }
}
