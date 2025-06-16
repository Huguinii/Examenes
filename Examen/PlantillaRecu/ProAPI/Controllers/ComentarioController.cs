using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Controllers.RestAPI.Controllers;
using RestAPI.Models.DTOs.ComentarioDTO;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        [Authorize(Roles = "DAM_ORD_DOS")]
    public class ComentarioController : BaseController<ComentarioEntity, ComentarioDTO, CreateComentarioDTO>
        {
            public ComentarioController(IComentarioRepository comentarioController,
                IMapper mapper, ILogger<ComentarioController> logger)
                : base(comentarioController, mapper, logger)
            {

            }
        }
}
