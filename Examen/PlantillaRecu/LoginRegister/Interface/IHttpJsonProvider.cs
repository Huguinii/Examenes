﻿using LoginRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegister.Interface
{
    public interface IHttpJsonProvider<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(string api_url);
        Task<T?> GetByIdAsync(string path, int id);
        Task<IEnumerable<T>> GetByNameAsync(string path, string name);
        Task<T?> PostAsync(string path, T data);
        Task<T?> PutAsync(string path, T data);
        Task Authenticate(string path, HttpClient httpClient, HttpResponseMessage request);
        Task<T?> LoginPostAsync(string path, LoginDTO data);
        Task<T?> RegisterPostAsync(string path, UserRegistroDTO data);
        Task<T?> Delete(string path, Guid id);
    }

}
