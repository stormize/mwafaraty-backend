﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mwafaraty.Business.Managers.IManagers;
using Mwafaraty.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwafaraty.Controllers
{
    
    [Authorize]
    public class VendorController : BaseController
    {
        IVendorManager _vendorManager;

        public VendorController(IVendorManager vendorManager)
        {
            _vendorManager = vendorManager;
        }
        [HttpPost("filter")]

        public async Task<IActionResult> Filter()
        {
           var body = await _vendorManager.Filter(Language);
            var response = BuildResponse(body);
            return Ok(response);
        }
    }
}
