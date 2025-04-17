using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevJANGO.App.Data;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using DevJANGO.Data.Repository;
using DevJANGO.Business.Models;
using DevJANGO.Data.Migrations;

namespace DevJANGO.App.Controllers
{
    [Route("as-multas")]
    public class MultasController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IMultaRepository _multaRepository;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public MultasController(
            IMultaRepository multaRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _multaRepository = multaRepository;
            _mapper = mapper;
        }
        #endregion

        #region MÉTODO PARA LISTAR
        //[AllowAnonymous]
        [Route("lista")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<MultaViewModel>>(await _multaRepository.ObterTodos()));
        }

        #endregion
 
    }
}
