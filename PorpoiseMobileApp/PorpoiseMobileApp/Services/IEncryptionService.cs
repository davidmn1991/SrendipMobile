﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PorpoiseMobileApp.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
