// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace StsServerIdentity.Models.DeviceViewModels
{
    public class DeviceAuthorizationInputModel : ConsentViewModels.ConsentViewModel
    {
        public string UserCode { get; set; }
    }
}