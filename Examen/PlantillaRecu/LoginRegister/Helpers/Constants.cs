﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegister.Helpers
{
    public static class Constants
    {
        public const string JSON_FILTER = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

        public const string BASE_URL = "https://localhost:7778/api/";
        public const string COMENTARIO_URL = "Comentario";
        public const string USUARIO_URL = "Usuario";
        public const string USUARIO_NAME_URL = "Usuario/name";
        public const string LOGIN_PATH = "users/login";
        public const string REGISTER_PATH = "users/register";
        public const string PATH_IMAGE_NOT_FOUND = "/Resources/Not_found.png";

    }
}
