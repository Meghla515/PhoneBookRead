using Newtonsoft.Json;
using PhoneBookReadService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookReadService.Service.Message
{
    public class MessageConsumer
    {
        public void HandlePhonebookMessage(string result)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<KafkaMessage>(result);

                switch (data.EventName)
                {
                    case "phonebookDeleted":
                        using (var helper = new ServiceFactory())
                        {
                            var service = helper.pbService;
                            var phonebookDTO = JsonConvert.DeserializeObject<PhoneBookDTO>(data.Payload);
                            service.DeletePhoneBook(phonebookDTO?.id ?? 0);
                        }
                        break;
                    case "phonebookCreated":
                        using (var helper = new ServiceFactory())
                        {
                            var service = helper.pbService;
                            var phonebookDTO = JsonConvert.DeserializeObject<PhoneBookDTO>(data.Payload);
                            service.SavePhonebook(phonebookDTO);
                        }
                        break;
                    case "phonebookUpdated":
                        using (var helper = new ServiceFactory())
                        {
                            var service = helper.pbService;
                            var phonebookDTO = JsonConvert.DeserializeObject<PhoneBookDTO>(data.Payload);
                            service.UpdatePhonebook(phonebookDTO);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }
    }
}
