using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBookReadService.DataTransfer.Model;
using PhoneBookReadService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookReadAPI.Controllers
{
    [ApiController]
    [Route("/phonebookread")]
    public class PhoneBookReadController : ControllerBase
    {
        private readonly IPbReadService readservice;

        public PhoneBookReadController(IPbReadService service)
        {
            readservice = service;
        }

        [AllowAnonymous]
        [Route("get-entries-by-name"), HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PhoneBookDTO>), 200)]
        public IActionResult GetPhoneBookByName(string name)
        {

            var data = readservice.GetPhonebookByName(name);
            return Ok(data);
        }

        [AllowAnonymous]
        [Route("save-entry"), HttpPost]
        [ProducesResponseType(typeof(PhoneBookDTO), 200)]
        public IActionResult SaveEntry(PhoneBookDTO dto)
        {

            readservice.SavePhonebook(dto);

            return Ok();
        }
    }
}
