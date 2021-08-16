using EscalasADCWebAPI.Data.Collections;
using EscalasADCWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EscalasADCWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoluntarioController : ControllerBase

    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Voluntario> _voluntarioCollection;
        public VoluntarioController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _voluntarioCollection = _mongoDB.DB.GetCollection<Voluntario>(typeof(Voluntario).Name.ToLower());
        }
        [HttpPost]
        public ActionResult SalvarVoluntario([FromBody] VoluntarioDto dto)
        {
            var voluntario = new Voluntario(dto.Nome, dto.Instrumento);

            _voluntarioCollection.InsertOne(voluntario);
            return StatusCode(201, "Voluntário adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterVoluntarios()
        {
            var voluntarios = _voluntarioCollection.Find(Builders<Voluntario>.Filter.Empty).ToList();

            return Ok(voluntarios);
        }
    }
}