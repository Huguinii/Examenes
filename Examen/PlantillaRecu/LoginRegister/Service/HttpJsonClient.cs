using LoginRegister.Interface;
using LoginRegister.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using LoginRegister.Helpers;
using Microsoft.Extensions.DependencyInjection;


namespace LoginRegister.Services
{
    internal class HttpJsonService<T> : IHttpJsonProvider<T> where T : class
    {

        LoginDTO loginDTO = App.Current.Services.GetService<LoginDTO>();

        public async Task<IEnumerable<T?>> GetAsync(string path)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");
                    HttpResponseMessage request = await httpClient.GetAsync($"{Constants.BASE_URL}{path}");
                    if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Authenticate(path, httpClient, request);
                        request = await httpClient.GetAsync($"{Constants.BASE_URL}{path}");
                    }
                    string dataRequest = await request.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<T>>(dataRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }

        public async Task<T?> GetByIdAsync(string path, int id)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");

                HttpResponseMessage request = await httpClient.GetAsync($"{Constants.BASE_URL}{path}/{id}");

                if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Authenticate($"{path}/{id}", httpClient, request);
                    request = await httpClient.GetAsync($"{Constants.BASE_URL}{path}/{id}");
                }

                if (!request.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {request.StatusCode}");
                    return default;
                }

                string dataRequest = await request.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public async Task<IEnumerable<T?>> GetByNameAsync(string path, string name)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");
                    HttpResponseMessage request = await httpClient.GetAsync($"{Constants.BASE_URL}{path}/{name}");
                    if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Authenticate(path, httpClient, request);
                        request = await httpClient.GetAsync($"{Constants.BASE_URL}{path}");
                    }
                    string dataRequest = await request.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<T>>(dataRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }


        public async Task Authenticate(string path, HttpClient httpClient, HttpResponseMessage request)
        {
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");

            HttpResponseMessage requestToken = await httpClient.PostAsync($"{Constants.BASE_URL}{Constants.LOGIN_PATH}", httpContent);

            string dataTokenRequest = await requestToken.Content.ReadAsStringAsync();
            UserDTO tokenUser = JsonSerializer.Deserialize<UserDTO>(dataTokenRequest);

            loginDTO.Token = tokenUser?.Result?.Token ?? string.Empty;
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");
        }

        public async Task<T?> PostAsync(string path, T data)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");

                    string jsonContent = JsonSerializer.Serialize(data);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync($"{Constants.BASE_URL}{path}", content);

                    // Reintento si se recibió Unauthorized
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Authenticate(path, httpClient, response);
                        response = await httpClient.PostAsync($"{Constants.BASE_URL}{path}", content);
                    }

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    else
                    {
                        // Loguear error si fue BadRequest o cualquier otro
                        Console.WriteLine($"Error en la solicitud POST: {response.StatusCode}");
                        Console.WriteLine($"Cuerpo del error: {responseBody}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud POST: {ex.Message}");
            }

            return default;
        }


        public async Task<T?> LoginPostAsync(string path, LoginDTO data) 
            {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {

                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");

                    // Serializar el objeto 'data' () a JSON
                    string jsonContent = JsonSerializer.Serialize(data);

                    // Crear el contenido HTTP con el tipo adecuado para enviar JSON
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync($"{Constants.BASE_URL}{path}", content);

                    // Leer el contenido de la respuesta y deserializarlo
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Aquí debes extraer el token y guardarlo
                    if (result is UserDTO userDto && userDto?.Result?.Token != null)
                    {
                        loginDTO.Token = userDto.Result.Token; // guardamos el token para futuras peticiones
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud POST: {ex.Message}");
            }
            return default;
        }

        public async Task<T?> RegisterPostAsync(string path, UserRegistroDTO data)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {

                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");

                    // Serializar el objeto 'data' (UserRegistroDTO) a JSON
                    string jsonContent = JsonSerializer.Serialize(data);

                    // Crear el contenido HTTP con el tipo adecuado para enviar JSON
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync($"{Constants.BASE_URL}{path}", content);

                    // Leer el contenido de la respuesta y deserializarlo
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Error en el registro: {response.StatusCode} - {responseBody}");
                        return default;
                    }
                    return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud POST: {ex.Message}");
            }
            return default;
        }



        public async Task<T?> PutAsync(string path, T data)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Agregar encabezado Authorization si es necesario
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");

                    // Serializar el objeto 'data' (dto) a JSON
                    string jsonContent = JsonSerializer.Serialize(data,
                     new JsonSerializerOptions
                     {
                         DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                         WriteIndented = true  // Hace que el JSON sea más legible (con saltos de línea y espacios)
                     });

                    // Crear el contenido HTTP con el tipo adecuado para enviar JSON
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Realizar la solicitud PATCH
                    HttpResponseMessage request = await httpClient.PutAsync($"{Constants.BASE_URL}{path}", content);

                    if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Authenticate(path, httpClient, request);
                        request = await httpClient.PutAsync($"{Constants.BASE_URL}{path}", content);

                        if (request.IsSuccessStatusCode)
                        {
                            string responseBody = await request.Content.ReadAsStringAsync();
                            return JsonSerializer.Deserialize<T?>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        else
                        {
                            Console.WriteLine("Error en la respuesta: " + request.StatusCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud PATCH: {ex.Message}");
            }
            return default;
        }

        public async Task<T?> Delete(string path, Guid id)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginDTO.Token}");
                    HttpResponseMessage request = await httpClient.DeleteAsync($"{Constants.BASE_URL}{path}{id}");
                    if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await Authenticate(path, httpClient, request);
                        request = await httpClient.DeleteAsync($"{Constants.BASE_URL}{path}{id}");
                    }
                    string dataRequest = await request.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(dataRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }


    }
}




